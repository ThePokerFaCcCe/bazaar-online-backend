namespace BazaarOnline.Application.DTOs.MapDTOs;

public class LocationValidationResultDTO
{
    public LocationValidationStatusEnum Status { get; set; }
    public string? Message { get; set; }
    public bool IsValid => Status is LocationValidationStatusEnum.IsValid;
}