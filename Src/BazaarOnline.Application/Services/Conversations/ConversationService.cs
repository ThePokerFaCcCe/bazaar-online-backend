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

    public AddMessageResultDTO AddMessage(AddMessageDTO dto, string userId)
    {
        dto.TrimStrings();


        var message = new Message
        {
            SenderId = userId,
        }.FillFromObject(dto);

        throw new Exception();
    }

    private string? ValidateMessage(AddMessageDTO dto)
    {
        var errors = new Dictionary<string, object>();

        if (dto.ConversationId == null)
            errors.Add(nameof(dto.ConversationId), "این فیلد اجباری است");

        if (dto.AttachmentType == null)
            errors.Add(nameof(dto.AttachmentType), "این فیلد اجباری است");

        if (dto.ReplyToId != null)
        {
            var isReplyExists = _repository.GetAll<Message>().Any(x => x.Id == dto.ReplyToId && !x.IsDeleted);
            if (!isReplyExists)
                errors.Add(nameof(dto.ReplyToId), "پیغامی که قصد پاسخ به آن را دارید یافت نشد");
        }

        if (dto.Text != null && dto.Text.Length > 512)
        {
            errors.Add(nameof(dto.Text), "متن پیام باید حداکثر 512 کاراکتر باشد");
        }

        if (dto.AttachmentType == MessageAttachmentTypeEnum.Location)
        {
            var locationErrors = new Dictionary<string, string>();
            if (dto.LocationAttachment?.Latitude == null || dto.LocationAttachment.Latitude is < -180d or > 180d)
                locationErrors.Add(nameof(dto.LocationAttachment.Latitude), "باید عددی بین -180 تا 180 باشد");

            if (dto.LocationAttachment?.Longitude == null || dto.LocationAttachment.Longitude is < -180d or > 180d)
                locationErrors.Add(nameof(dto.LocationAttachment.Longitude), "باید عددی بین -180 تا 180 باشد");

            if (locationErrors.Any())
                errors.Add(nameof(dto.LocationAttachment), locationErrors);
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.NoAttachment)
        {
            if (dto.Text == null)
                errors.Add(nameof(dto.Text), "این فیلد اجباری است");
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.Picture)
        {
            if (dto.PictureAttachment == null)
                errors.Add(nameof(dto.PictureAttachment), "این فیلد اجباری است");
        }
        else if (dto.AttachmentType == MessageAttachmentTypeEnum.Voice)
        {
            if (dto.VoiceAttachment == null)
                errors.Add(nameof(dto.VoiceAttachment), "این فیلد اجباری است");
        }
        else
        {
            errors.Add(nameof(dto.AttachmentType), "مقدار ورودی معتبر نیست");
        }

        return null;
    }
}