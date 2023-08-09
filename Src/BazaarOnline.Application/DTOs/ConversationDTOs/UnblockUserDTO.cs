using System.ComponentModel.DataAnnotations;

namespace BazaarOnline.Application.DTOs.ConversationDTOs;

public class UnblockUserDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public string UserId { get; set; }
}
public class UnblockUserRequestDTO
{
    [Required(ErrorMessage = "این فیلد اجباری است")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "این فیلد اجباری است")]
    public UnblockUserRequestTypeEnum UnblockType { get; set; }


}

public enum UnblockUserRequestTypeEnum
{
    ByUserId=1,
    ByConversationId=2
}