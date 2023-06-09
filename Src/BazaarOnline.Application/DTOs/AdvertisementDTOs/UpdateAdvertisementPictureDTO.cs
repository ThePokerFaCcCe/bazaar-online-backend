namespace BazaarOnline.Application.DTOs.AdvertisementDTOs;

public class UpdateAdvertisementPictureDTO
{
    public List<int> Pictures { get; set; }
    public UpdateAdvertisementPictureStatus Status { get; set; }
}

public enum UpdateAdvertisementPictureStatus
{
    DeletePictures,
    InsertPictures,
}