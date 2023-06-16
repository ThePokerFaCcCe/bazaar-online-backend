using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class EditMessageDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid ConversationId { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid MessageId { get; set; }

    [MaxLength(512, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Text { get; set; }


    /// <summary>
    /// Index of message that edited - used for frontend and not important in backend
    /// </summary>
    public int? MessageIndexInquiry { get; set; }
}