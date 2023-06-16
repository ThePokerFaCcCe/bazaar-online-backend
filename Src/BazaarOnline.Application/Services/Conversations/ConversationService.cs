using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Conversations;
using BazaarOnline.Domain.Entities.Conversations;
using BazaarOnline.Domain.Entities.UploadCenter;
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
        var conv = _repository.GetAll<Conversation>()
            .SingleOrDefault(c =>
                c.CustomerId == userId && c.OwnerId == dto.UserId && c.AdvertisementId == dto.AdvertisementId);

        if (conv == null)
        {
            var model = new Conversation
            {
                AdvertisementId = dto.AdvertisementId,
                OwnerId = dto.UserId,
                CustomerId = userId,
            };
            _repository.Add(model);
            _repository.Save();

            conv = model;
        }

        return new AddConversationResultDTO
        {
            ConversationId = conv.Id,
        };
    }

    public MessageOperationResultDTO AddMessage(AddMessageDTO dto, string userId)
    {
        dto.TrimStrings();

        var validationErrors = ValidateMessage(dto);
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

        message.Text = "(این پیام حذف شده است)"; // ez pz :))
        message.AttachmentType = MessageAttachmentTypeEnum.NoAttachment;
        message.AttachmentJson = null;

        message.IsDeleted = true;
        _repository.Update(message);
        _repository.Save();

        result.MessageId = message.Id;
        return result;
    }

    public IEnumerable<MessageDetailViewModel> GetConversationMessages(Guid conversationId, string userId)
    {
        var messages = _repository.GetAll<Message>()
            .Include(m => m.ReplyTo)
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreateDate);

        return messages.ToList().Select(m => GetMessageViewModel(m, userId));
    }

    public MessageDetailViewModel GetMessage(Guid messageId, string userId)
    {
        var message = _repository.GetAll<Message>()
            .Include(m => m.ReplyTo)
            .SingleOrDefault(m => m.Id == messageId);
        return GetMessageViewModel(message, userId);
    }

    public string? GetSecondConversationUser(Guid conversationId, string userId)
    {
        var conversation = _repository.Get<Conversation>(conversationId);
        if (conversation == null || (conversation.CustomerId != userId && conversation.OwnerId != userId))
            return null;
        return conversation.CustomerId == userId ? conversation.OwnerId : conversation.CustomerId;
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
            .Include(c => c.Advertisement)
            .ThenInclude(a => a.Pictures)
            .Where(c => c.OwnerId == userId || c.CustomerId == userId);

        return conversations.AsEnumerable().Select(c =>
        {
            var user = c.OwnerId != userId ? c.Owner : c.Customer;

            return new ConversationDetailViewModel
            {
                Data = new ConversationDetailDataViewModel
                {
                    Advertisement = new ConversationDetailAdvertisementViewModel
                    {
                        Data = new ConversationDetailAdvertisementDataViewModel
                        {
                            Picture = new ViewModels.Advertisements.AdvertisementPictureViewModel
                            {
                            }.FillFromObject(c.Advertisement.Pictures.MinBy(p => p.Id), false),
                        }.FillFromObject(c.Advertisement, false),
                    }.FillFromObject(c.Advertisement, false),

                    User = new ConversationDetailUserViewModel
                    {
                        Data = new ConversationDetailUserDataViewModel
                        {
                        }.FillFromObject(user, false),
                    }.FillFromObject(user, false),

                    LastMessage = GetMessageViewModel(c.Messages.MaxBy(m => m.CreateDate), userId),
                }
            }.FillFromObject(c, false);
        }).OrderByDescending(c => c.Data.LastMessage?.Data.CreateDate);
    }

    private string? ValidateMessage(AddMessageDTO dto)
    {
        var errors = new Dictionary<string, object>();

        if (dto.ConversationId == null)
        {
            errors.Add(nameof(dto.ConversationId), "این فیلد اجباری است");
        }

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
            .Any(c => c.Id == conversationId && !c.IsDeleted && (c.OwnerId == userId || c.CustomerId == userId));
    }
}