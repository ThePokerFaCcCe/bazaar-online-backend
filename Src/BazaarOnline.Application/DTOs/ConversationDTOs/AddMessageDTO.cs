using BazaarOnline.Domain.Entities.Conversations;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs
{
    public class AddMessageDTO
    {
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public Guid ConversationId { get; set; }

        public Guid? ReplyToId { get; set; }

        [Display(Name = "متن پیام")]
        [MaxLength(512, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
        public string Text { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public MessageAttachmentTypeEnum AttachmentType { get; set; }

        public MessagePictureAttachmentDTO? PictureAttachment { get; set; }

        public MessageVoiceAttachmentDTO? VoiceAttachment { get; set; }

        public MessageLocationAttachmentDTO? LocationAttachment { get; set; }
    }
}