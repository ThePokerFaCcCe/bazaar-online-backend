using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Interfaces.Hubs;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BazaarOnline.API.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    private readonly IConversationService _conversationService;
    private readonly IUserService _userService;
    private string UserId => Context.User.Identity.Name;

    public ChatHub(IConversationService conversationService, IUserService userService)
    {
        _conversationService = conversationService;
        _userService = userService;
    }

    private string Jsonify(object data) => JsonConvert.SerializeObject(
        data,
        new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        await Groups.AddToGroupAsync(Context.ConnectionId, UserId);

        try
        {
            var lastSeenResult = _userService.UpdateUserLastSeenToNow(UserId);
            if (true) // maybe user service will be refactored in future
            {
                var receivers = _conversationService.GetUserIdsHaveConversationWithUser(UserId).ToList();
                foreach (var receiver in receivers)
                {
                    var seenEvent = new SocketEventDTO
                    {
                        EventType = SocketEventTypeEnum.UserOnline,
                        Data = new SocketUserLastSeenEventDataDTO
                        {
                            ConversationId = receiver.ConversationId,
                            UserId = lastSeenResult.UserId,
                            LastSeen = lastSeenResult.LastSeen,
                        }
                    };
                    await Clients.Group(receiver.UserId).ReceiveEvent(Jsonify(seenEvent));
                }
            }
        }
        catch (Exception e)
        {
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var lastSeenResult = _userService.UpdateUserLastSeenToNow(UserId);
            if (true) // maybe user service will be refactored in future
            {

                var receivers = _conversationService.GetUserIdsHaveConversationWithUser(UserId).ToList();
                foreach (var receiver in receivers)
                {
                    var seenEvent = new SocketEventDTO
                    {
                        EventType = SocketEventTypeEnum.UserOffline,
                        Data = new SocketUserLastSeenEventDataDTO
                        {
                            ConversationId = receiver.ConversationId,
                            UserId = lastSeenResult.UserId,
                            LastSeen = lastSeenResult.LastSeen,
                        }
                    };
                    await Clients.Group(receiver.UserId).ReceiveEvent(Jsonify(seenEvent));
                }

            }
        }
        catch (Exception e)
        {
        }
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, UserId);

        await base.OnDisconnectedAsync(exception);
    }


    public async Task SendMessage(string inquiryId, string jsonData)
    {
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<AddMessageDTO>>(jsonData);
            if (data?.Data == null)
            {
                throw new ArgumentNullException();
            }

            var validation = _conversationService.AddMessage(data.Data, UserId);

            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                Data = validation,
                IsSuccess = validation.IsSuccess,
                OperationType = SocketOperationTypeEnum.SendMessage,
            };

            await Clients.Caller.ReceiveOperationResult(Jsonify(result));

            if (validation.IsSuccess)
            {
                var createdMessage = _conversationService.GetMessage((Guid)validation.MessageId, UserId);
                await Clients.Caller.ReceiveMessage(inquiryId, Jsonify(createdMessage));

                var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);

                var isConversationDeleted =
                    _conversationService.IsUserDeletedConversation(data.Data.ConversationId, receiverId);
                var isConversationHasAnyMessages =
                    _conversationService.HasConversationAnyMessages(data.Data.ConversationId, receiverId);

                if (isConversationDeleted || !isConversationHasAnyMessages)
                {
                    var addConvEvent = new SocketEventDTO
                    {
                        EventType = SocketEventTypeEnum.NewConversation,
                        Data = _conversationService.GetConversationDetail(data.Data.ConversationId, receiverId),
                    };

                    await Clients.Group(receiverId).ReceiveEvent(Jsonify(addConvEvent));
                }

                createdMessage.Data.IsSentBySelf = false;
                await Clients.Group(receiverId).ReceiveMessage(string.Empty, Jsonify(createdMessage));
            }
        }
        catch (Exception e)
        {
            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                ServerErrorMessage = "Internal server error!" + e.Message,
                IsSuccess = false,
                OperationType = SocketOperationTypeEnum.SendMessage,
            };
            await Clients.Caller.ReceiveOperationResult(Jsonify(result));
        }
    }

    public async Task EditMessage(string inquiryId, string jsonData)
    {
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<EditMessageDTO>>(jsonData);
            if (data?.Data == null)
            {
                throw new ArgumentNullException();
            }

            var validation = _conversationService.EditMessage(data.Data, UserId);

            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                Data = validation,
                IsSuccess = validation.IsSuccess,
                OperationType = SocketOperationTypeEnum.EditMessage,
            };


            if (validation.IsSuccess)
            {
                var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);

                var editedMessage = _conversationService.GetMessage((Guid)validation.MessageId, UserId);
                var replies = _conversationService.GetMessageReplies((Guid)validation.MessageId, UserId).ToList();

                replies.Add(editedMessage);
                foreach (var m in replies)
                {
                    data.Data.EditedMessage = m;
                    data.Data.MessageId = m.Id;
                    var editEvent = new SocketEventDTO
                    {
                        Data = data,
                        EventType = SocketEventTypeEnum.EditMessage,
                    };

                    await Clients.Group(UserId).ReceiveEvent(Jsonify(editEvent));

                    data.Data.EditedMessage.Data.IsSentBySelf = (!data.Data.EditedMessage.Data.IsSentBySelf);
                    await Clients.Group(receiverId).ReceiveEvent(Jsonify(editEvent));
                }
            }

            await Clients.Caller.ReceiveOperationResult(Jsonify(result));
        }
        catch (Exception e)
        {
            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                ServerErrorMessage = "Internal server error!" + e.Message,
                IsSuccess = false,
                OperationType = SocketOperationTypeEnum.SendMessage,
            };
            await Clients.Caller.ReceiveOperationResult(Jsonify(result));
        }
    }

    public async Task DeleteMessage(string inquiryId, string jsonData)
    {
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<DeleteMessageDTO>>(jsonData);
            //if (data?.Data == null) throw new ArgumentNullException();

            var validation = _conversationService.DeleteMessage(data.Data, UserId);

            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                Data = validation,
                IsSuccess = validation.IsSuccess,
                OperationType = SocketOperationTypeEnum.DeleteMessage,
            };

            if (validation.IsSuccess)
            {
                var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);

                var deletedMessage = _conversationService.GetMessage((Guid)validation.MessageId, UserId);
                data.Data.DeletedMessage = deletedMessage;

                var deleteEvent = new SocketEventDTO
                {
                    Data = data,
                    EventType = SocketEventTypeEnum.DeleteMessage,
                };

                await Clients.Group(UserId).ReceiveEvent(Jsonify(deleteEvent));

                data.Data.DeletedMessage.Data.IsSentBySelf = (!data.Data.DeletedMessage.Data.IsSentBySelf);
                await Clients.Group(receiverId).ReceiveEvent(Jsonify(deleteEvent));

                // send edit message event 
                var replies = _conversationService.GetMessageReplies((Guid)validation.MessageId, UserId).ToList();

                var editData = new SocketOperaionRequestDTO<EditMessageDTO>
                {
                    Data = new EditMessageDTO
                    {
                        EditedMessage = null,
                    }.FillFromObject(data.Data)
                }.FillFromObject(data);

                foreach (var m in replies)
                {
                    editData.Data.EditedMessage = m;
                    editData.Data.MessageId = m.Id;
                    var editEvent = new SocketEventDTO
                    {
                        Data = editData,
                        EventType = SocketEventTypeEnum.EditMessage,
                    };

                    await Clients.Group(UserId).ReceiveEvent(Jsonify(editEvent));

                    editData.Data.EditedMessage.Data.IsSentBySelf = (!editData.Data.EditedMessage.Data.IsSentBySelf);
                    await Clients.Group(receiverId).ReceiveEvent(Jsonify(editEvent));
                }
            }

            await Clients.Caller.ReceiveOperationResult(Jsonify(result));
        }
        catch (Exception e)
        {
            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                ServerErrorMessage = "Internal server error!" + e.Message,
                IsSuccess = false,
                OperationType = SocketOperationTypeEnum.SendMessage,
            };
            await Clients.Caller.ReceiveOperationResult(Jsonify(result));
        }
    }

    public async Task SeenConversation(string inquiryId, string jsonData)
    {
        var response = new SocketOperationResultDTO
        {
            InquiryId = inquiryId,
            IsSuccess = false,
            OperationType = SocketOperationTypeEnum.SeenConversation,
        };
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<SeenConversationDTO>>(jsonData);
            if (data?.Data?.ConversationId == null || data?.Data.ConversationId == Guid.Empty)
            {
                throw new ArgumentNullException("ConversationId Is Empty");
            }

            var seenResult = _conversationService.SeenConversation(data.Data.ConversationId, UserId);
            if (seenResult.IsSuccess)
            {
                var seenEvent = new SocketEventDTO
                {
                    Data = data,
                    EventType = SocketEventTypeEnum.SeenConversation,
                };

                var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);
                await Clients.Group(receiverId).ReceiveEvent(Jsonify(seenEvent));
                response.IsSuccess = true;
            }
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal server error!" + e.Message;
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }

    public async Task SeenMessage(string inquiryId, string jsonData)
    {
        var response = new SocketOperationResultDTO
        {
            InquiryId = inquiryId,
            IsSuccess = false,
            OperationType = SocketOperationTypeEnum.SeenMessage,
        };
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<SeenMessageDTO>>(jsonData);
            if (data?.Data?.ConversationId == null || data?.Data.ConversationId == Guid.Empty)
            {
                throw new ArgumentNullException("ConuversationId Is Empty");
            }

            if (data?.Data?.MessageId == null || data?.Data.MessageId == Guid.Empty)
            {
                throw new ArgumentNullException("MessageId Is Empty");
            }

            var seenResult = _conversationService.SeenConversation(data.Data.ConversationId, UserId);
            if (seenResult.IsSuccess)
            {
                var seenEvent = new SocketEventDTO
                {
                    Data = data,
                    EventType = SocketEventTypeEnum.SeenMessage,
                };

                var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);
                await Clients.Group(receiverId).ReceiveEvent(Jsonify(seenEvent));
                response.IsSuccess = true;
            }
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal server error!" + e.Message;
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }

    public async Task ImOnline(string inquiryId)
    {
        var response = new SocketOperationResultDTO
        {
            InquiryId = inquiryId,
            IsSuccess = false,
            OperationType = SocketOperationTypeEnum.ImOnline,
        };
        try
        {

            var lastSeenResult = _userService.UpdateUserLastSeenToNow(UserId);
            if (true) // maybe user service will be refactored in future
            {

                var receivers = _conversationService.GetUserIdsHaveConversationWithUser(UserId).ToList();
                foreach (var receiver in receivers)
                {
                    var seenEvent = new SocketEventDTO
                    {
                        EventType = SocketEventTypeEnum.UserOnline,
                        Data = new SocketUserLastSeenEventDataDTO
                        {
                            ConversationId = receiver.ConversationId,
                            UserId = lastSeenResult.UserId,
                            LastSeen = lastSeenResult.LastSeen,
                        }
                    };
                    await Clients.Group(receiver.UserId).ReceiveEvent(Jsonify(seenEvent));
                }

                response.IsSuccess = true;
            }
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal server error!" + e.Message;
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }

    public async Task ImOffline(string inquiryId)
    {
        var response = new SocketOperationResultDTO
        {
            InquiryId = inquiryId,
            IsSuccess = false,
            OperationType = SocketOperationTypeEnum.ImOffline,
        };
        try
        {

            var lastSeenResult = _userService.UpdateUserLastSeenToNow(UserId);
            if (true) // maybe user service will be refactored in future
            {

                var receivers = _conversationService.GetUserIdsHaveConversationWithUser(UserId).ToList();
                foreach (var receiver in receivers)
                {
                    var seenEvent = new SocketEventDTO
                    {
                        EventType = SocketEventTypeEnum.UserOffline,
                        Data = new SocketUserLastSeenEventDataDTO
                        {
                            ConversationId = receiver.ConversationId,
                            UserId = lastSeenResult.UserId,
                            LastSeen = lastSeenResult.LastSeen,
                        }
                    };
                    await Clients.Group(receiver.UserId).ReceiveEvent(Jsonify(seenEvent));
                }

                response.IsSuccess = true;
            }
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal server error!" + e.Message;
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }

    public async Task ChattingStatus(string inquiryId, string jsonData)
    {
        var response = new SocketOperationResultDTO
        {
            InquiryId = inquiryId,
            IsSuccess = false,
            OperationType = SocketOperationTypeEnum.ChattingStatus,
        };
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<ChattingStatusDTO>>(jsonData);
            if (data?.Data?.ConversationId == null || data.Data.Status == null || data.Data.Timeout == null)
            {
                throw new ArgumentNullException();
            }

            var chattingEvent = new SocketEventDTO
            {
                Data = data.Data,
                EventType = SocketEventTypeEnum.Chatting,
            };
            var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId,
                checkIsDeletedConversation: true);

            if (receiverId != null)
            {
                await Clients.Group(receiverId).ReceiveEvent(Jsonify(chattingEvent));
            }

            response.IsSuccess = true;
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal server error!" + e.Message;
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }
}