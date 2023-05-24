using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Conversations;
using Humanizer;

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

    public MessageLocationAttachmentViewModel? AttachmentLocation { get; set; } = null;

    public MessageFileAttachmentViewModel? AttachmentFile { get; set; } = null;

    public MessageAttachmentTypeEnum AttachmentType { get; set; } = MessageAttachmentTypeEnum.NoAttachment;

    public string AttachmentTypeName => AttachmentType.GetDisplayName().Camelize(); // front developer request

    public Guid? ReplyToId { get; set; } = null;

    public MessageDetailViewModel? ReplyTo { get; set; } = null;

    public Guid ConversationId { get; set; } = Guid.Empty;

    public DateTime CreateDate { get; set; } = DateTime.MinValue;

    public DateTime UpdateDate { get; set; } = DateTime.MinValue;

    public bool IsSentBySelf { get; set; }

    public bool IsSeen { get; set; } = false;

    public bool IsEdited { get; set; } = false;

    public bool IsDeleted { get; set; } = false;
}

public class MessageLocationAttachmentViewModel
{
    public double Longitude { get; set; } = double.NaN;
    public double Latitude { get; set; } = double.NaN;
}

public class MessageFileAttachmentViewModel
{
    public string FileName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}