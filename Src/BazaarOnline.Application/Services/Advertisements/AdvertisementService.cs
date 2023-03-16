using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            StatusType = AdvertisementStatusTypeEnum.Accepted, //////////////////////////////////TODO: Remove line
            //StatusType = AdvertisementStatusTypeEnum.Pending,
        }.FillFromObject(dto);

        _repository.Add(advertisement);

        var advertisementPictures = dto.Pictures.Select(fileId => new AdvertisementPicture
        {
            Advertisement = advertisement,
            FileCenterId = fileId,
        });

        var advertisementFeatures = dto.Features.Select(feature => new AdvertisementFeature
        {
            Value = feature.Value,
            CategoryFeatureId = feature.Id,
            Advertisement = advertisement,
        });

        _repository.AddRange(advertisementPictures);
        _repository.AddRange(advertisementFeatures);

        _repository.Save();
        return advertisement.Id;
    }

    public IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementList(AdvertisemenFilterDTO filterDto)
    {
        var advertisements = _repository.GetAll<Advertisement>()
            .Include(a => a.City)
            .Include(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Include(a => a.AdvertisementFeatures)
            .ThenInclude(af => af.CategoryFeature)
            .ThenInclude(cf => cf.Feature)
            .Where(a => a.StatusType == AdvertisementStatusTypeEnum.Accepted);

        advertisements = advertisements.Filter(filterDto);
        if (filterDto.HasPicture)
            advertisements = advertisements.Where(a => a.Pictures.Any());

        return advertisements.ToList().Select(a =>
        {
            var firstPic = a.Pictures.MinBy(p => p.Id);
            AdvertisementPictureViewModel? picture = null;
            if (firstPic != null)
                picture = new AdvertisementPictureViewModel().FillFromObject(firstPic.FileCenter);

            return new AdvertisementListDetailViewModel
            {
                Data = new AdvertisementListDetailDataViewModel
                {
                    Picture = picture,
                    LocationText = a.City.Name,
                    TimeText = a.UpdateDate.PassedFromNowString(),
                    Features = a.AdvertisementFeatures.Where(af => af.CategoryFeature.IsShownInList)
                    .Select(af => new AdvertisementFeatureDetailViewModel
                    {
                        Id = af.Id,
                        Name = af.CategoryFeature.Feature.Name,
                        Value = af.Value,
                        SortNumber = af.CategoryFeature.SortNumber,
                        IsChatEnabled = a.ContactType is AdvertisementContactTypeEnum.ChatOnly,
                    }.FillFromObject(a)),
                }
            }.FillFromObject(a);
        });
    }
}