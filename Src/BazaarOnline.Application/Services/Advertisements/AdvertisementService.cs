using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Entities.Features;
using BazaarOnline.Domain.Entities.Users;
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

    public bool IsAdvertisementExists(int id)
    {
        return _repository.GetAll<Advertisement>()
            .Any(a => a.Id == id);
    }

    public Advertisement? GetAdvertisement(int id)
    {
        return _repository.Get<Advertisement>(id);
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
                    IsChatEnabled =
                        a.ContactType is AdvertisementContactTypeEnum.ChatOnly or AdvertisementContactTypeEnum.Normal,
                    Features = a.AdvertisementFeatures.Where(af => af.CategoryFeature.IsShownInList)
                        .Select(af => new AdvertisementFeatureDetailViewModel
                        {
                            Id = af.Id,
                            Name = af.CategoryFeature.Feature.Name,
                            Value = af.Value,
                            SortNumber = af.CategoryFeature.SortNumber,
                            Position = af.CategoryFeature.Feature.Position,
                        }),
                }.FillFromObject(a)
            }.FillFromObject(a);
        });
    }

    public AdvertisementDetailViewModel? GetAdvertisementDetail(int id, bool acceptedOnly = false,
        string? userId = null)
    {
        var advertisement = _repository.GetAll<Advertisement>()
            .Include(a => a.Province)
            .Include(a => a.City)
            .Include(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Include(a => a.AdvertisementFeatures)
            .ThenInclude(af => af.CategoryFeature)
            .ThenInclude(cf => cf.Feature)
            .IgnoreQueryFilters()
            .SingleOrDefault(a =>
                a.Id == id && (!acceptedOnly || a.StatusType == AdvertisementStatusTypeEnum.Accepted));

        if (advertisement == null)
            return null;

        var note = string.Empty;
        var isBookmarked = false;
        if (!string.IsNullOrEmpty(userId))
        {
            var foundNote = _repository.GetAll<UserAdvertisementNote>()
                .SingleOrDefault(u => u.UserId == userId && u.AdvertisementId == advertisement.Id);
            if (foundNote != null)
                note = foundNote.Note;

            isBookmarked = _repository.GetAll<UserAdvertisementBookmark>()
                .Any(u => u.UserId == userId && u.AdvertisementId == advertisement.Id);
        }


        var features = advertisement.AdvertisementFeatures
            .Select(af => new AdvertisementFeatureDetailViewModel
            {
                Id = af.Id,
                Name = af.CategoryFeature.Feature.Name,
                Value = af.Value,
                SortNumber = af.CategoryFeature.SortNumber,
                Position = af.CategoryFeature.Feature.Position,
            }).ToList();

        var advertisementCategoryPath = new List<AdvertisementCategoryDetailViewModel>();
        var categories = _repository.GetAll<Category>().Include(c => c.ParentCategory).ToList();
        int? categoryId = advertisement.CategoryId;
        do
        {
            var category = categories.First(c => c.Id == categoryId);
            advertisementCategoryPath.Add(new AdvertisementCategoryDetailViewModel().FillFromObject(category));
            categoryId = category.ParentCategoryId;
        } while (categoryId != null);

        advertisementCategoryPath = advertisementCategoryPath.OrderByDescending(c => c.Id).ToList();

        return new AdvertisementDetailViewModel
        {
            Data = new AdvertisementDetailDataViewModel
            {
                Note = note,
                IsBookmarked = isBookmarked,
                Province = advertisement.Province.Name,
                City = advertisement.City.Name,
                CategoryPath = advertisementCategoryPath,
                Location = new AdvertisementLocationDetailViewModel().FillFromObject(advertisement),

                Pictures = advertisement.Pictures.Select(p =>
                    new AdvertisementPictureViewModel().FillFromObject(p.FileCenter)),

                TopFeatures = features.Where(f => f.Position is FeaturePositionEnum.Top),
                BottomFeatures = features.Where(f => f.Position is FeaturePositionEnum.Bottom),
                MiddleFeatures = features.Where(f => f.Position is FeaturePositionEnum.Middle),
                OtherFeatures = features.Where(f => f.Position is FeaturePositionEnum.Other),
            }.FillFromObject(advertisement)
        }.FillFromObject(advertisement);
    }
}