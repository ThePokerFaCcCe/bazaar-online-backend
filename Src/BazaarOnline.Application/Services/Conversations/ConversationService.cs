using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.Utils.Extentions;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Application.ViewModels.Conversations;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Conversations;
using BazaarOnline.Domain.Entities.UploadCenter;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BazaarOnline.Application.Services.Conversations;

public class ConversationService : IConversationService
{
    private readonly IRepository _repository;

    public ConversationService(IRepository repository)
    {
        _repository = repository;
    }

    public AddConversationResultDTO AddConversation(AddConversationDTO dto, string userId)
    {
        var advertisement = _repository.Get<Advertisement>(dto.AdvertisementId);
        if (advertisement == null || advertisement.IsDeleted || advertisement.UserId == userId)
            return new AddConversationResultDTO { ErrorCode = 404, ErrorMessage = "آگهی یافت نشد" };

        var blockStatus = ValidateBlock(userId, advertisement.UserId);
        if (!blockStatus.IsValid)
            return new AddConversationResultDTO { ErrorCode = 400, ErrorMessage = blockStatus.Message };

        var conv = _repository.GetAll<Conversation>()
            .Include(c => c.DeletedConversations)
            .SingleOrDefault(c =>
                c.CustomerId == userId && c.OwnerId == advertisement.UserId &&
                c.AdvertisementId == dto.AdvertisementId);

        if (conv == null)
        {
            var model = new Conversation
            {
                AdvertisementId = dto.AdvertisementId,
                OwnerId = advertisement.UserId,
                CustomerId = userId,
            };
            _repository.Add(model);
            _repository.Save();

            conv = model;
        }
        else
        {
            var deletedConversation = conv.DeletedConversations.SingleOrDefault(dc => dc.UserId == userId);
            if (deletedConversation != null)
            {
                _repository.Remove(deletedConversation);
                _repository.Save();
            }
        }

        return new AddConversationResultDTO
        {
            ConversationId = conv.Id,
            ReceiverUserId = advertisement.UserId,
        };
    }

    public void DeleteConversation(Guid conversationId, string userId)
    {
        var exists = _repository.GetAll<DeletedConversation>()
            .Any(c => c.ConversationId == conversationId && c.UserId == userId);
        if (exists) return;

        var deletedConversation = new DeletedConversation
        {
            ConversationId = conversationId,
            UserId = userId,
        };
        _repository.Add(deletedConversation);

        var deletedMessages = _CreateDeleteConversationMessageList(conversationId, userId);

        _repository.AddRange(deletedMessages);

        _repository.Save();
    }


    public OperationResultDTO BulkDeleteConversations(IEnumerable<Guid> conversationIds, string userId)
    {
        var conversations = _repository.GetAll<Conversation>()
            .Where(c => conversationIds.Contains(c.Id) && (c.CustomerId == userId || c.OwnerId == userId))
            .Where(c => !c.DeletedConversations.Any(dc => dc.UserId == (c.CustomerId != userId ? c.CustomerId : c.OwnerId)))
            .ToList();

        var foundConversationIds = conversations.Select(c => c.Id);

        var notFoundConversationIds = conversationIds.Except(foundConversationIds).ToList();
        if (notFoundConversationIds.Any())
        {
            var errors = new Dictionary<Guid, string>();
            notFoundConversationIds.ForEach(conv => errors[conv] = "یافت نشد");

            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = errors.Stringify(),
            };
        }

        conversations.ForEach(c =>
        {
            var deletedConversation = new DeletedConversation
            {
                ConversationId = c.Id,
                UserId = userId,
            };
            _repository.Add(deletedConversation);

            var deletedMessages = _CreateDeleteConversationMessageList(c.Id, userId);
            _repository.AddRange(deletedMessages);

        });

        _repository.Save();
        return new OperationResultDTO { IsSuccess = true };
    }

    public IEnumerable<BlocklistViewModel> GetUserBlocklist(string userId)
    {
        return _repository.GetAll<Blocklist>()
            .Include(b => b.BlockedUser)
            .Where(b => b.BlockerId == userId)
            .Select(b => new BlocklistViewModel
            {
                Id = b.BlockedUser.Id,
                Data = new BlocklistDataViewModel
                {
                    BlockDate = b.CreateDate,
                    DisplayName = b.BlockedUser.DisplayName,
                }
            });
    }


    private IQueryable<DeletedMessage> _CreateDeleteConversationMessageList(Guid conversationId, string userId)
    {
        var messageIds = _repository.GetAll<Message>()
            .Include(m => m.DeletedMessages)
            .Where(m => m.ConversationId == conversationId && !m.DeletedMessages.Any(d => d.UserId == userId))
            .Select(m => m.Id);

        var deletedMessages = messageIds.Select(messageId => new DeletedMessage
        {
            ConversationId = conversationId,
            UserId = userId,
            MessageId = messageId
        });
        return deletedMessages;
    }

    public MessageOperationResultDTO AddMessage(AddMessageDTO dto, string userId)
    {
        dto.TrimStrings();

        var validationErrors = ValidateMessage(dto, userId);
        if (!string.IsNullOrEmpty(validationErrors))
        {
            return new MessageOperationResultDTO
            {
                ErrorCode = 400,
                ErrorMessage = validationErrors,
            };
        }

        var message = new Message
        {
            SenderId = userId,
        }.FillFromObject(dto);

        switch (dto.AttachmentType)
        {
            case MessageAttachmentTypeEnum.Picture:
                message.AttachmentJson = JsonConvert.SerializeObject(dto.PictureAttachment);
                break;
            case MessageAttachmentTypeEnum.Voice:
                message.AttachmentJson = JsonConvert.SerializeObject(dto.VoiceAttachment);
                break;
            case MessageAttachmentTypeEnum.Location:
                message.AttachmentJson = JsonConvert.SerializeObject(dto.LocationAttachment);
                break;
            default:
                break;
        }


        var deletedConversations = _repository.GetAll<DeletedConversation>()
            .Where(dc => dc.ConversationId == dto.ConversationId);
        _repository.RemoveRange(deletedConversations);

        _repository.Add(message);
        _repository.Save();

        return new MessageOperationResultDTO
        {
            MessageId = message.Id
        };
    }

    public MessageOperationResultDTO EditMessage(EditMessageDTO dto, string userId)
    {
        dto.TrimStrings();
        var result = new MessageOperationResultDTO();

        var errors = new Dictionary<string, object>();
        if (dto.ConversationId == null)
            errors.Add(nameof(dto.ConversationId), "این فیلد اجباری است");
        if (dto.MessageId == null)
            errors.Add(nameof(dto.MessageId), "این فیلد اجباری است");
        if (dto.Text != null && dto.Text.Length > 512)
            errors.Add(nameof(dto.Text), "متن پیام باید حداکثر 512 کاراکتر باشد");

        if (errors.Any())
        {
            result.ErrorMessage = JsonConvert.SerializeObject(errors);
            result.ErrorCode = 400;
            return result;
        }

        var message = _repository.GetAll<Message>()
            .SingleOrDefault(m => !m.IsDeleted && m.ConversationId == dto.ConversationId
                                               && m.Id == dto.MessageId && m.SenderId == userId);

        if (message == null)
        {
            errors.Add(nameof(dto.MessageId), "پیام یافت نشد");
        }
        else if (message.AttachmentType != MessageAttachmentTypeEnum.NoAttachment)
        {
            errors.Add(nameof(dto.MessageId), "شما نمیتوانید پیامی غیر از متن را ویرایش کنید");
        }

        if (errors.Any())
        {
            result.ErrorMessage = JsonConvert.SerializeObject(errors);
            result.ErrorCode = 400;
            return result;
        }

        message.Text = dto.Text;
        message.UpdateDate = DateTime.Now;
        message.IsEdited = true;
        _repository.Update(message);
        _repository.Save();

        result.MessageId = message.Id;
        return result;
    }

    public MessageOperationResultDTO DeleteMessage(DeleteMessageDTO dto, string userId)
    {
        dto.TrimStrings();
        var result = new MessageOperationResultDTO();

        var errors = new Dictionary<string, object>();
        if (dto.ConversationId == null)
            errors.Add(nameof(dto.ConversationId), "این فیلد اجباری است");
        if (dto.MessageId == null)
            errors.Add(nameof(dto.MessageId), "این فیلد اجباری است");

        if (errors.Any())
        {
            result.ErrorMessage = JsonConvert.SerializeObject(errors);
            result.ErrorCode = 400;
            return result;
        }

        var message = _repository.GetAll<Message>()
            .SingleOrDefault(m => !m.IsDeleted && m.ConversationId == dto.ConversationId
                                               && m.Id == dto.MessageId && m.SenderId == userId);

        if (message == null)
        {
            errors.Add(nameof(dto.MessageId), "پیام یافت نشد");
            result.ErrorMessage = JsonConvert.SerializeObject(errors);
            result.ErrorCode = 400;
            return result;
        }

        int? fileCenterId = null;
        switch (message.AttachmentType)
        {
            case MessageAttachmentTypeEnum.Picture:
                fileCenterId = JsonConvert.DeserializeObject<MessagePictureAttachmentDTO>(message.AttachmentJson)
                    ?.FileId;
                break;
            case MessageAttachmentTypeEnum.Voice:
                fileCenterId = JsonConvert.DeserializeObject<MessageVoiceAttachmentDTO>(message.AttachmentJson)?.FileId;
                break;
        }

        if (fileCenterId != null)
        {
            var fileCenter = _repository.Get<FileCenter>((int)fileCenterId);
            if (fileCenter != null)
            {
                switch (fileCenter.UsageType)
                {
                    case FileCenterTypeEnum.ChatPicture:
                        FileHelper.DeleteFile(Path.Join(PathHelper.ChatImages, fileCenter.FileName));
                        FileHelper.DeleteFile(Path.Join(PathHelper.ChatThumbs, fileCenter.FileName));
                        break;
                    case FileCenterTypeEnum.ChatVoice:
                        FileHelper.DeleteFile(Path.Join(PathHelper.ChatVoice, fileCenter.FileName));
                        break;
                }

                _repository.Remove(fileCenter);
            }
        }

        message.Text = "(این پیام حذف شده است)"; // ez pz :))
        message.AttachmentType = MessageAttachmentTypeEnum.NoAttachment;
        message.AttachmentJson = null;

        message.IsDeleted = true;
        message.UpdateDate = DateTime.Now;
        _repository.Update(message);
        _repository.Save();

        result.MessageId = message.Id;
        return result;
    }

    public ConversationDetailViewModel? GetConversationDetail(Guid conversationId, string userId)
    {
        var conversation = _repository.GetAll<Conversation>()
            .Include(c => c.Owner)
            .Include(c => c.Customer)
            .Include(c => c.Messages)
            .ThenInclude(m => m.DeletedMessages)
            .Include(c => c.Advertisement)
            .ThenInclude(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Where(c => c.Id == conversationId)
            .Where(c => c.OwnerId == userId || c.CustomerId == userId)
            .SingleOrDefault();

        if (conversation == null) return null;

        var otherUserIds = new List<string> { conversation.CustomerId, conversation.OwnerId }
            .Distinct().Where(u => u != userId);
        var blocks = _repository.GetAll<Blocklist>()
            .Where(b => (b.BlockedUserId == userId || b.BlockerId == userId)).ToList()
            .Where(b => otherUserIds.Contains(b.BlockedUserId) || otherUserIds.Contains(b.BlockerId));

        var secondUser = conversation.OwnerId != userId ? conversation.Owner : conversation.Customer;

        return new ConversationDetailViewModel
        {
            Data = new ConversationDetailDataViewModel
            {
                Advertisement = new ConversationDetailAdvertisementViewModel
                {
                    Data = new ConversationDetailAdvertisementDataViewModel
                    {
                        Picture = new AdvertisementPictureViewModel().FillFromObject(conversation.Advertisement.Pictures.MinBy(p => p.Id)?.FileCenter, false),
                    }.FillFromObject(conversation.Advertisement, false),
                }.FillFromObject(conversation.Advertisement, false),

                User = new ConversationDetailUserViewModel
                {
                    Data = new ConversationDetailUserDataViewModel
                    {
                    }.FillFromObject(secondUser, false),
                }.FillFromObject(secondUser, false),

                LastMessage = GetMessageViewModel(conversation.Messages.Where(m => !m.DeletedMessages.Any(d => d.UserId == userId)).MaxBy(m => m.CreateDate), userId),
                IsBlockedByUser = blocks.Any(b => b.BlockedUserId == userId && b.BlockerId == secondUser.Id),
                IsBlockedUserBySelf = blocks.Any(b => b.BlockerId == userId && b.BlockedUserId == secondUser.Id),
            }
        }.FillFromObject(conversation, false);
    }

    public PaginationResultDTO<MessageDetailViewModel> GetConversationMessages(Guid conversationId, string userId,
        PaginationFilterDTO pagination)
    {
        var messages = _repository.GetAll<Message>()
            .Include(m => m.ReplyTo)
            .Include(m => m.DeletedMessages)
            .Where(m => m.ConversationId == conversationId && !m.DeletedMessages.Any(d => d.UserId == userId))
            .OrderByDescending(m => m.CreateDate);

        return new PaginationResultDTO<MessageDetailViewModel>
        {
            AllCount = messages.Count(),
            Content = messages.Paginate(pagination).AsEnumerable()
                .Reverse() // frontend developer request
                .Select(m => GetMessageViewModel(m, userId))
        };
    }

    public bool IsUserDeletedConversation(Guid conversationId, string userId)
    {
        return _repository.GetAll<DeletedConversation>()
            .Any(d => d.ConversationId == conversationId && d.UserId == userId);
    }

    public MessageDetailViewModel GetMessage(Guid messageId, string userId)
    {
        var message = _repository.GetAll<Message>()
            .Include(m => m.ReplyTo)
            .SingleOrDefault(m => m.Id == messageId);
        return GetMessageViewModel(message, userId);
    }

    public string? GetSecondConversationUser(Guid conversationId, string userId, bool checkIsDeletedConversation = false)
    {
        var conversations = _repository.GetAll<Conversation>();
        if (checkIsDeletedConversation)
        {
            conversations = conversations.Include(c => c.DeletedConversations);
        }

        var conversation = conversations.SingleOrDefault(c => c.Id == conversationId);

        if (conversation == null || (conversation.CustomerId != userId && conversation.OwnerId != userId))
            return null;

        var secondConversationUserId = conversation.CustomerId == userId ? conversation.OwnerId : conversation.CustomerId;

        if (checkIsDeletedConversation && conversation.DeletedConversations.Any(dc => dc.UserId == secondConversationUserId))
            return null;

        return secondConversationUserId;
    }

    public IEnumerable<ConversationUserIdViewModel> GetUserIdsHaveConversationWithUser(string userId)
    {
        var conversations = _repository.GetAll<Conversation>()
            .Include(c => c.DeletedConversations)
            .Where(c => c.CustomerId == userId || c.OwnerId == userId)
            .Where(c => !c.DeletedConversations.Any(dc => dc.UserId == (c.CustomerId != userId ? c.CustomerId : c.OwnerId)))
            .Select(c => new { c.Id, c.CustomerId, c.OwnerId });

        return conversations.ToList().Select(c => new ConversationUserIdViewModel
        {
            UserId = c.CustomerId != userId ? c.CustomerId : c.OwnerId,
            ConversationId = c.Id,
        });
    }

    public OperationResultDTO SeenConversation(Guid conversationId, string userId)
    {
        var conversation = _repository.Get<Conversation>(conversationId);
        if (conversation == null || (conversation.CustomerId != userId && conversation.OwnerId != userId))
            return new OperationResultDTO { IsSuccess = false, Message = "Conversation Not Found" };

        var senderId = conversation.CustomerId == userId ? conversation.OwnerId : conversation.CustomerId;
        var messages = _repository.GetAll<Message>()
            .Where(m => m.ConversationId == conversationId && m.SenderId == senderId && m.IsSeen == false)
            .ToList();

        messages.ForEach(m => m.IsSeen = true);
        _repository.UpdateRange(messages);
        _repository.Save();
        return new OperationResultDTO { IsSuccess = true };
    }

    public OperationResultDTO SeenMessage(Guid conversationId, Guid messageId, string userId)
    {
        var conversation = _repository.Get<Conversation>(conversationId);
        if (conversation == null || (conversation.CustomerId != userId && conversation.OwnerId != userId))
            return new OperationResultDTO { IsSuccess = false, Message = "Conversation Not Found" };

        var senderId = conversation.CustomerId == userId ? conversation.OwnerId : conversation.CustomerId;

        var lastMessage = _repository.GetAll<Message>()
            .OrderByDescending(m => m.CreateDate)
            .FirstOrDefault(m => m.ConversationId == conversationId && m.SenderId == senderId && m.IsSeen == false);

        if (lastMessage == null)
            return new OperationResultDTO { IsSuccess = true, Message = "No new messages to seen" };

        var messages = _repository.GetAll<Message>()
            .OrderByDescending(m => m.CreateDate)
            .Where(m => m.ConversationId == conversationId && m.SenderId == senderId &&
                        m.IsSeen == false && m.CreateDate <= lastMessage.CreateDate)
            .Take(30)
            .ToList();

        messages.ForEach(m => m.IsSeen = true);
        _repository.UpdateRange(messages);
        _repository.Save();
        return new OperationResultDTO { IsSuccess = true, Message = $"{messages.Count} Messages SeenConversation" };
    }

    public OperationResultDTO BlockUser(BlockUserDTO dto, string blockerUserId)
    {
        var userExists = _repository.GetAll<User>().Any(u => u.Id == dto.UserId);
        if (!userExists)
            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = "کاربر یافت نشد",
            };

        var exists = _repository.GetAll<Blocklist>()
            .Any(b => b.BlockerId == blockerUserId && b.BlockedUserId == dto.UserId);
        if (exists) return new OperationResultDTO { IsSuccess = true };

        var block = new Blocklist
        {
            BlockedUserId = dto.UserId,
            BlockerId = blockerUserId
        };
        _repository.Add(block);
        _repository.Save();

        return new OperationResultDTO { IsSuccess = true };
    }

    public OperationResultDTO UnblockUser(UnblockUserDTO dto, string blockerUserId)
    {
        var userExists = _repository.GetAll<User>().Any(u => u.Id == dto.UserId);
        if (!userExists)
            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = "کاربر یافت نشد",
            };

        var block = _repository.GetAll<Blocklist>()
            .SingleOrDefault(b => b.BlockerId == blockerUserId && b.BlockedUserId == dto.UserId);
        if (block == null)
            return new OperationResultDTO
            {
                IsSuccess = false,
                Message = "کاربر بلاک نشده است"
            };

        _repository.Remove(block);
        _repository.Save();

        return new OperationResultDTO { IsSuccess = true };
    }

    private MessageDetailViewModel? GetMessageViewModel(Message? message, string userId)
    {
        if (message == null) return null;

        var model = new MessageDetailViewModel
        {
            Data = new MessageDetailDataViewModel
            {
                IsSentBySelf = (message.SenderId == userId),
            }.FillFromObject(message),
        }.FillFromObject(message);

        if (message.ReplyTo != null)
        {
            model.Data.ReplyTo = GetMessageViewModel(message.ReplyTo, userId);
        }

        if (message.AttachmentType == MessageAttachmentTypeEnum.Location)
        {
            var data = JsonConvert.DeserializeObject<MessageLocationAttachmentViewModel>(message.AttachmentJson);
            model.Data.AttachmentLocation = data;
        }
        else if (message.AttachmentType == MessageAttachmentTypeEnum.Picture)
        {
            var data = JsonConvert.DeserializeObject<MessagePictureAttachmentDTO>(message.AttachmentJson);
            var file = _repository.Get<FileCenter>(data.FileId);
            if (file != null)
            {
                model.Data.AttachmentFile = new MessageFileAttachmentViewModel
                {
                    FileName = file.FileName,
                    Url = Path.Join(PathHelper.ChatImages, file.FileName),
                };
            }
        }
        else if (message.AttachmentType == MessageAttachmentTypeEnum.Voice)
        {
            var data = JsonConvert.DeserializeObject<MessageVoiceAttachmentDTO>(message.AttachmentJson);
            var file = _repository.Get<FileCenter>(data.FileId);
            if (file != null)
            {
                model.Data.AttachmentFile = new MessageFileAttachmentViewModel
                {
                    FileName = file.FileName,
                    Url = Path.Join(PathHelper.ChatVoice, file.FileName),
                };
            }
        }

        return model;
    }

    public IEnumerable<ConversationDetailViewModel> GetConversations(string userId)
    {
        var conversations = _repository.GetAll<Conversation>()
            .Include(c => c.Owner)
            .Include(c => c.Customer)
            .Include(c => c.Messages)
            .ThenInclude(m => m.DeletedMessages)
            .Include(c => c.Advertisement)
            .ThenInclude(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Where(c => c.OwnerId == userId || c.CustomerId == userId)
            .Where(c => c.Messages.Any())
            .Where(c => !c.DeletedConversations.Any(dc => dc.UserId == userId))
            .ToList()
            .OrderByDescending(c => c.Messages.MaxBy(m => m.CreateDate)?.CreateDate ?? c.CreateDate);

        var otherUserIds = conversations.Select(c => c.OwnerId).Union(conversations.Select(c => c.CustomerId))
            .Distinct().Where(u => u != userId);
        var blocks = _repository.GetAll<Blocklist>()
            .Where(b => (b.BlockedUserId == userId || b.BlockerId == userId)).ToList()
            .Where(b => otherUserIds.Contains(b.BlockedUserId) || otherUserIds.Contains(b.BlockerId));

        return conversations.Select(c =>
        {
            var secondUser = c.OwnerId != userId ? c.Owner : c.Customer;

            return new ConversationDetailViewModel
            {
                Data = new ConversationDetailDataViewModel
                {
                    Advertisement = new ConversationDetailAdvertisementViewModel
                    {
                        Data = new ConversationDetailAdvertisementDataViewModel
                        {
                            Picture = new AdvertisementPictureViewModel().FillFromObject(c.Advertisement.Pictures.MinBy(p => p.Id)?.FileCenter, false),
                        }.FillFromObject(c.Advertisement, false),
                    }.FillFromObject(c.Advertisement, false),

                    User = new ConversationDetailUserViewModel
                    {
                        Data = new ConversationDetailUserDataViewModel
                        {
                        }.FillFromObject(secondUser, false),
                    }.FillFromObject(secondUser, false),

                    LastMessage = GetMessageViewModel(c.Messages.Where(m => !m.DeletedMessages.Any(d => d.UserId == userId)).MaxBy(m => m.CreateDate), userId),
                    IsBlockedByUser = blocks.Any(b => b.BlockedUserId == userId && b.BlockerId == secondUser.Id),
                    IsBlockedUserBySelf = blocks.Any(b => b.BlockerId == userId && b.BlockedUserId == secondUser.Id),
                }
            }.FillFromObject(c, false);
        });
    }

    private ValidationResultDTO ValidateBlock(string senderId, string receiverId)
    {
        var result = new ValidationResultDTO { IsValid = false };

        var isBlocker = _repository.GetAll<Blocklist>()
            .Any(b => b.BlockerId == senderId && b.BlockedUserId == receiverId);
        if (isBlocker)
        {
            result.Message = "شما این کاربر را بلاک کرده اید";
            return result;
        }

        var isBlockedByUser = _repository.GetAll<Blocklist>()
            .Any(b => b.BlockerId == receiverId && b.BlockedUserId == senderId);
        if (isBlockedByUser)
        {
            result.Message = "این کاربر شما را بلاک کرده است";
            return result;
        }

        result.IsValid = true;
        return result;
    }

    private string? ValidateMessage(AddMessageDTO dto, string userId)
    {
        var errors = new Dictionary<string, object>();

        if (dto.ConversationId == null)
        {
            errors.Add(nameof(dto.ConversationId), "این فیلد اجباری است");
        }
        else
        {
            var conversation = _repository.GetAll<Conversation>()
                .Where(c => !c.DeletedConversations.Any(dc => dc.UserId == userId))
                .SingleOrDefault(c => c.Id == dto.ConversationId && (c.CustomerId == userId || c.OwnerId == userId));
            if (conversation == null)
            {
                errors.Add(nameof(dto.ConversationId), "چت یافت نشد");
                goto Next;
            }

            var secondUserId = conversation.CustomerId == userId ? conversation.OwnerId : conversation.CustomerId;
            var blockStatus = ValidateBlock(userId, secondUserId);
            if (!blockStatus.IsValid)
                errors.Add(nameof(dto.ConversationId), blockStatus.Message);
        }

    Next:
        if (dto.AttachmentType == null)
        {
            errors.Add(nameof(dto.AttachmentType), "این فیلد اجباری است");
        }

        if (dto.ReplyToId != null)
        {
            var isReplyExists = _repository.GetAll<Message>().Any(x => x.Id == dto.ReplyToId && !x.IsDeleted);
            if (!isReplyExists)
            {
                errors.Add(nameof(dto.ReplyToId), "پیغامی که قصد پاسخ به آن را دارید یافت نشد");
            }
        }

        if (dto.Text != null && dto.Text.Length > 512)
        {
            errors.Add(nameof(dto.Text), "متن پیام باید حداکثر 512 کاراکتر باشد");
        }

        if (dto.AttachmentType == MessageAttachmentTypeEnum.Location)
        {
            var locationErrors = new Dictionary<string, string>();
            if (dto.LocationAttachment?.Latitude == null || dto.LocationAttachment.Latitude is < -180d or > 180d)
            {
                locationErrors.Add(nameof(dto.LocationAttachment.Latitude), "باید عددی بین -180 تا 180 باشد");
            }

            if (dto.LocationAttachment?.Longitude == null || dto.LocationAttachment.Longitude is < -180d or > 180d)
            {
                locationErrors.Add(nameof(dto.LocationAttachment.Longitude), "باید عددی بین -180 تا 180 باشد");
            }

            if (locationErrors.Any())
            {
                errors.Add(nameof(dto.LocationAttachment), locationErrors);
            }
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.NoAttachment)
        {
            if (dto.Text == null)
            {
                errors.Add(nameof(dto.Text), "این فیلد اجباری است");
            }
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.Picture)
        {
            var pictureErrors = new Dictionary<string, string>();
            if (dto.PictureAttachment?.FileId == null)
            {
                errors.Add(nameof(AddMessageDTO.PictureAttachment.FileId), "این فیلد اجباری است");
            }
            else
            {
                var isPictureExists = _repository.GetAll<FileCenter>()
                    .Any(f => f.Id == dto.PictureAttachment.FileId && f.UsageType == FileCenterTypeEnum.ChatPicture);

                if (!isPictureExists)
                {
                    errors.Add(nameof(dto.PictureAttachment.FileId), "فایل تصویر یافت نشد");
                }
            }

            if (pictureErrors.Any())
            {
                errors.Add(nameof(dto.PictureAttachment), pictureErrors);
            }
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.Voice)
        {
            var voiceErrors = new Dictionary<string, string>();
            if (dto.VoiceAttachment?.FileId == null)
            {
                errors.Add(nameof(AddMessageDTO.VoiceAttachment.FileId), "این فیلد اجباری است");
            }
            else
            {
                var isVoiceExists = _repository.GetAll<FileCenter>()
                    .Any(f => f.Id == dto.VoiceAttachment.FileId && f.UsageType == FileCenterTypeEnum.ChatVoice);

                if (!isVoiceExists)
                {
                    errors.Add(nameof(dto.VoiceAttachment.FileId), "فایل صوتی یافت نشد");
                }
            }

            if (voiceErrors.Any())
            {
                errors.Add(nameof(dto.VoiceAttachment), voiceErrors);
            }
        }
        else
        {
            errors.Add(nameof(dto.AttachmentType), "مقدار ورودی معتبر نیست");
        }

        if (errors.Any())
        {
            return JsonConvert.SerializeObject(errors);
        }

        return null;
    }

    public bool IsConversationExists(Guid conversationId, string userId)
    {
        return _repository.GetAll<Conversation>()
            .Any(c => c.Id == conversationId && !c.IsDeleted &&
                      (c.OwnerId == userId || c.CustomerId == userId) &&
                      !c.DeletedConversations.Any(dc => dc.UserId == userId));
    }

    public bool HasConversationAnyMessages(Guid conversationId, string userId)
    {
        return _repository.GetAll<Message>()
            .Any(m => m.ConversationId == conversationId);
    }
}