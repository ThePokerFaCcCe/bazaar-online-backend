using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Conversations;

namespace BazaarOnline.Application.ViewModels.Conversations;

public class MessageDetailViewModel
{
    public Guid Id { get; set; }

    public MessageDetailDataViewModel Data { get; set; }
        = new MessageDetailDataViewModel();
}

public class MessageDetailDataViewModel
{
    public string Text { get; set; } = string.Empty;

    public string? AttachmentJson { get; set; } = null;

    public MessageAttachmentTypeEnum AttachmentType { get; set; } = MessageAttachmentTypeEnum.NoAttachment;

    public string AttachmentTypeName => AttachmentType.GetDisplayName();

    public Guid? ReplyToId { get; set; } = null;

    public DateTime CreateDate { get; set; } = DateTime.MinValue;

    public bool IsSentBySelf { get; set; }

    public bool IsSeen { get; set; } = false;
}