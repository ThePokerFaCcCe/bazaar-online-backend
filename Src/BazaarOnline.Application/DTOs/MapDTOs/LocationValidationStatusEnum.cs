namespace BazaarOnline.Application.DTOs.MapDTOs;

public enum LocationValidationStatusEnum
{
    IsValid,
    ProvinceNotFound,
    CityNotFound,
    CoordinatesNotInProvince,
}