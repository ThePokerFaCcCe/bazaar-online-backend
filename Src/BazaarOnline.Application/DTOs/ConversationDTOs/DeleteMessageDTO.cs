using System.ComponentModel.DataAnnotations;
using BazaarOnline.Application.ViewModels.Conversations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class DeleteMessageDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid ConversationId { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid MessageId { get; set; }


    /// <summary>
    /// The message object sent for ReceiveEvent signal
    /// </summary>
    public MessageDetailViewModel? DeletedMessage { get; set; }
}