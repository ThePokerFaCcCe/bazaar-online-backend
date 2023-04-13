using BazaarOnline.Application.DTOs.ConversationDTOs;
using BazaarOnline.Application.Interfaces.Conversations;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Conversations;
using BazaarOnline.Domain.Interfaces;

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
            .SingleOrDefault(c => c.CustomerId == userId && c.OwnerId == dto.UserId && c.AdvertisementId == dto.AdvertisementId);

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

    public void AddMessage(AddMessageDTO dto, string userId)
    {
        dto.TrimStrings();

        var message = new Message
        {
            SenderId = userId,
            
        }.FillFromObject(dto);
    }
}
