using System.ComponentModel.DataAnnotations;
using BazaarOnline.Application.ViewModels.Conversations;
using BazaarOnline.Domain.Entities.Conversations;

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

    /// <summary>
    /// The Edited message object sent for ReceiveEvent signal
    /// </summary>
    public MessageDetailViewModel? EditedMessage { get; set; }
}