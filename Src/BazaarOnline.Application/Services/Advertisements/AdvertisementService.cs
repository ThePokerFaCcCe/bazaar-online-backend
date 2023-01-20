using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Interfaces;

namespace BazaarOnline.Application.Services.Advertisements;

public class AdvertisementService : IAdvertisementService
{
    private readonly IRepository _repository;

    public AdvertisementService(IRepository repository)
    {
        _repository = repository;
    }

    public int CreateAdvertisement(CreateAdvertisementDTO dto, string userId)
    {
        dto.TrimStrings();

        var advertisement = new Advertisement
        {
            UserId = userId,
            StatusType = AdvertisementStatusTypeEnum.Pending,
        }.FillFromObject(dto);

        _repository.Add(advertisement);

        var advertisementPictures = dto.Pictures.Select(formFile =>
        {
            var fileName = FileHelper.SaveImageWithThumb(
                formFile,
                PathHelper.AdvertisementImages,
                PathHelper.AdvertisementThumbs
            );

            return new AdvertisementPicture
            {
                Advertisement = advertisement,
                PictureName = fileName,
            };
        });

        _repository.AddRange(advertisementPictures);

        _repository.Save();
        return advertisement.Id;
    }
}