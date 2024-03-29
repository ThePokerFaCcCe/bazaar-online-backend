﻿using BazaarOnline.Application.ViewModels.Conversations;
using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class EditMessageDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid ConversationId { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid MessageId { get; set; }

    [MaxLength(3999, ErrorMessage = "{0} باید حداکثر {1} کاراکتر باشد")]
    public string Text { get; set; }

    /// <summary>
    /// new message image when message type is picture
    /// </summary>
    public MessagePictureAttachmentDTO? PictureAttachment { get; set; }

    /// <summary>
    /// The Edited message object sent for ReceiveEvent signal
    /// </summary>
    public MessageDetailViewModel? EditedMessage { get; set; }
}