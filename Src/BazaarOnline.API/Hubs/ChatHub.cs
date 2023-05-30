using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Interfaces.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BazaarOnline.API.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    private readonly IConversationService _conversationService;
    private string UserId => Context.User.Identity.Name;

    public ChatHub(IConversationService conversationService)
    {
        _conversationService = conversationService;
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

        var groupName = UserId;
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessage(string inquiryId, string jsonData)
    {
        try
        {
            var data = JsonConvert.DeserializeObject<SocketOperaionRequestDTO<AddMessageDTO>>(jsonData);
            if (data?.Data == null) throw new ArgumentNullException();

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
                createdMessage.Data.IsSentBySelf = false;
                await Clients.Group(receiverId).ReceiveMessage(string.Empty, Jsonify(createdMessage));
            }
        }
        catch (Exception e)
        {
            var result = new SocketOperationResultDTO
            {
                InquiryId = inquiryId,
                ServerErrorMessage = "Internal server error!",
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
                throw new ArgumentNullException("ConuversationId Is Empty");

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
            response.ServerErrorMessage = "Internal Server Error";
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
                throw new ArgumentNullException("ConuversationId Is Empty");
            if (data?.Data?.MessageId == null || data?.Data.MessageId == Guid.Empty)
                throw new ArgumentNullException("MessageId Is Empty");

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
            response.ServerErrorMessage = "Internal Server Error";
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
                throw new ArgumentNullException();
            var chattingEvent = new SocketEventDTO
            {
                Data = data.Data,
                EventType = SocketEventTypeEnum.SeenConversation,
            };
            var receiverId = _conversationService.GetSecondConversationUser(data.Data.ConversationId, UserId);
            await Clients.Group(receiverId).ReceiveEvent(Jsonify(chattingEvent));
            response.IsSuccess = true;
        }
        catch (Exception e)
        {
            response.ServerErrorMessage = "Internal Server Error";
        }

        await Clients.Caller.ReceiveOperationResult(Jsonify(response));
    }
}