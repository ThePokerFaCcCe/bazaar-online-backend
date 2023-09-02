using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Advertisements;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.Utils.Extentions;
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
        if (dto.Latitude == null || dto.Longitude == null)
        {
            dto.Latitude = null;
            dto.Longitude = null;
            dto.ShowExactCoordinates = false;
        }

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

        var advertisementFeatures = dto.Features.Where(f=>f.Value!=null).Select(feature => new AdvertisementFeature
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

    public OperationResultDTO UpdateAdvertisementPictures(int id, UpdateAdvertisementPictureDTO dto)
    {
        var advertisement = _repository.GetAll<Advertisement>()
            .Include(a => a.Pictures)
            .ThenInclude(a => a.FileCenter)
            .SingleOrDefault(a => a.Id == id);

        if (advertisement == null)
            return new OperationResultDTO { Message = "آگهی یافت نشد", IsSuccess = false };

        if (dto.Status is UpdateAdvertisementPictureStatus.InsertPictures)
        {
            var newPictures = dto.Pictures.Except(advertisement.Pictures.Select(ap => ap.FileCenterId));
            var advertisementPictures = newPictures.Select(fileId => new AdvertisementPicture
            {
                Advertisement = advertisement,
                FileCenterId = fileId,
            });

            _repository.AddRange(advertisementPictures);
        }
        else if (dto.Status is UpdateAdvertisementPictureStatus.DeletePictures)
        {
            var deletedPictures = advertisement.Pictures.Where(ap => dto.Pictures.Contains(ap.FileCenterId));
            var deletedFiles = deletedPictures.Select(ap => ap.FileCenter).ToList();

            _repository.RemoveRange(deletedPictures);
            _repository.RemoveRange(deletedFiles);

            deletedFiles.ForEach(f =>
            {
                FileHelper.DeleteFile(Path.Join(PathHelper.AdvertisementImages, f.FileName));
                FileHelper.DeleteFile(Path.Join(PathHelper.AdvertisementThumbs, f.FileName));
            });
        }

        advertisement.UpdateDate = DateTime.Now;
        _repository.Update(advertisement);
        _repository.Save();
        return new OperationResultDTO { IsSuccess = true };
    }

    public bool IsAdvertisementExists(int id)
    {
        return _repository.GetAll<Advertisement>()
            .Any(a => a.Id == id);
    }

    public bool IsAdvertisementExists(int advertisementId, string userId)
    {
        return _repository.GetAll<Advertisement>()
            .Any(a => a.Id == advertisementId && a.UserId == userId);
    }

    public Advertisement? GetAdvertisement(int id)
    {
        return _repository.Get<Advertisement>(id);
    }

    public PaginationResultDTO<AdvertisementSelfListDetailViewModel> GetSelfAdvertisementList(string userId,
        PaginationFilterDTO pagination)
    {
        var advertisements = _repository.GetAll<Advertisement>()
            .Include(a => a.City)
            .Include(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Include(a => a.AdvertisementFeatures)
            .ThenInclude(af => af.CategoryFeature)
            .ThenInclude(cf => cf.Feature)
            .Where(a => a.UserId == userId);


        return new PaginationResultDTO<AdvertisementSelfListDetailViewModel>
        {
            AllCount = advertisements.Count(),

            Content = advertisements.Paginate(pagination).ToList().Select(a =>
            {
                var firstPic = a.Pictures.MinBy(p => p.Id);
                AdvertisementPictureViewModel? picture = null;
                if (firstPic != null)
                    picture = new AdvertisementPictureViewModel().FillFromObject(firstPic.FileCenter);

                return new AdvertisementSelfListDetailViewModel
                {
                    Data = new AdvertisementSelfListDetailDataViewModel()
                    {
                        Picture = picture,
                        LocationText = a.City.Name,
                        TimeText = a.UpdateDate.PassedFromNowString(),
                        IsChatEnabled =
                            a.ContactType is AdvertisementContactTypeEnum.ChatOnly
                                or AdvertisementContactTypeEnum.Normal,
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
            })
        };
    }


    public OperationResultDTO UpdateAdvertisement(int id, UpdateAdvertisementDTO dto)
    {
        var advertisement = _repository.GetAll<Advertisement>()
            .Include(a => a.AdvertisementFeatures)
            .Include(a => a.Pictures)
            .SingleOrDefault(a => a.Id == id);

        if (advertisement == null)
            return new OperationResultDTO { Message = "آگهی یافت نشد", IsSuccess = false };

        dto.TrimStrings();
        if (dto.Latitude == null || dto.Longitude == null)
        {
            dto.Latitude = null;
            dto.Longitude = null;
            dto.ShowExactCoordinates = false;
        }

        advertisement.FillFromObject(dto);
        advertisement.UpdateDate = DateTime.Now;
        _repository.Update(advertisement);

        var advertisementFeatures = dto.Features.Select(feature => new AdvertisementFeature
        {
            Value = feature.Value,
            CategoryFeatureId = feature.Id,
            Advertisement = advertisement,
        });

        _repository.RemoveRange(advertisement.AdvertisementFeatures);
        _repository.AddRange(advertisementFeatures);

        var advertisementPictures = dto.Pictures.Select(fileCenterId => new AdvertisementPicture
        {
            Advertisement = advertisement,
            FileCenterId = fileCenterId,
        });

        _repository.RemoveRange(advertisement.Pictures);
        _repository.AddRange(advertisementPictures);

        _repository.Save();
        
        return new OperationResultDTO { IsSuccess = true };
    }


    public OperationResultDTO UpdateAdvertisementStatus(int id, AdvertisementUpdateStatusDTO dto)
    {
        dto.TrimStrings();

        var advertisement = _repository.Get<Advertisement>(id);
        if (advertisement == null)
            return new OperationResultDTO { Message = "آگهی یافت نشد", IsSuccess = false };

        advertisement.FillFromObject(dto);
        advertisement.UpdateDate = DateTime.Now;
        _repository.Update(advertisement);
        _repository.Save();

        return new OperationResultDTO { IsSuccess = true };
    }

    public PaginationResultDTO<AdvertisementListDetailViewModel> GetAdvertisementList(AdvertisemenFilterDTO filterDto,
        PaginationFilterDTO pagination)
    {
        var advertisements = _repository.GetAll<Advertisement>()
            .Include(a => a.City)
            .Include(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Include(a => a.AdvertisementFeatures)
            .ThenInclude(af => af.CategoryFeature)
            .ThenInclude(cf => cf.Feature)
            .Where(a => a.StatusType == AdvertisementStatusTypeEnum.Accepted)
            .Filter(filterDto);

        if (filterDto.HasPicture)
            advertisements = advertisements.Where(a => a.Pictures.Any());

        if (filterDto.Category != null)
        {
            var categoriesList = _repository.GetAll<Category>()
                .Include(c => c.ChildCategories)
                .ToList();

            var filterCategoryIdList = new List<int?>() { filterDto.Category };

            Action<Category> addChildrenAction = null;
            addChildrenAction = c =>
            {
                if (c.ParentCategory != null && filterCategoryIdList.Contains(c.ParentCategoryId))
                {
                    filterCategoryIdList.Add(c.Id);
                    c.ChildCategories.ToList().ForEach(c => addChildrenAction(c));

                }
            };
            categoriesList.ForEach(c => addChildrenAction(c));

            foreach (var category in categoriesList.Where(c => !filterCategoryIdList.Contains(c.Id)))
            {
                advertisements = advertisements.Where(a => a.CategoryId != category.Id);
            }


        }

        advertisements = advertisements.OrderByDescending(a => a.CreateDate);

        return new PaginationResultDTO<AdvertisementListDetailViewModel>
        {
            AllCount = advertisements.Count(),

            Content = advertisements.Paginate(pagination).ToList().Select(a =>
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
                            a.ContactType is AdvertisementContactTypeEnum.ChatOnly
                                or AdvertisementContactTypeEnum.Normal,
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
            })
        };
    }

    public AdvertisementDetailViewModel? GetAdvertisementDetail(int id, bool acceptedOnly = false,
        string? userId = null)
    {
        var advertisement = _repository.GetAll<Advertisement>()
            .Include(a => a.Province)
            .Include(a => a.City)
            .Include(a => a.User)
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

        var location = new AdvertisementLocationDetailViewModel();
        if (advertisement.HasCoordinates)
        {
            if (advertisement.ShowExactCoordinates)
            {
                location.FillFromObject(advertisement);
            }
            else
            {
                location.Latitude = Math.Round((double)advertisement.Latitude, 5);
                location.Longitude = Math.Round((double)advertisement.Longitude, 5);
                location.ShowExactCoordinates = false;
            }
        }


        return new AdvertisementDetailViewModel
        {
            Data = new AdvertisementDetailDataViewModel
            {
                Note = note,
                IsBookmarked = isBookmarked,
                Province = new AdvertisementProvinceDetailViewModel().FillFromObject(advertisement.Province),
                City = new AdvertisementCityDetailViewModel().FillFromObject(advertisement.City),
                Owner = new AdvertisementUserViewModel
                {
                    IsSelfAdvertisement = userId==advertisement.UserId,
                }.FillFromObject(advertisement.User),
                
                CategoryPath = advertisementCategoryPath,
                Location = location,

                Pictures = advertisement.Pictures.Select(p =>
                    new AdvertisementPictureViewModel().FillFromObject(p.FileCenter)),

                TopFeatures = features.Where(f => f.Position is FeaturePositionEnum.Top),
                BottomFeatures = features.Where(f => f.Position is FeaturePositionEnum.Bottom),
                MiddleFeatures = features.Where(f => f.Position is FeaturePositionEnum.Middle),
                OtherFeatures = features.Where(f => f.Position is FeaturePositionEnum.Other),
            }.FillFromObject(advertisement)
        }.FillFromObject(advertisement);
    }

    public IEnumerable<AdvertisementSearchSuggestViewModel> SearchSuggestAdvertisement(AdvertisementSearchDTO searchDto)
    {
        searchDto.TrimStrings();
        var queryWords = searchDto.Query.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToLower());
        var queryLastWord = queryWords.Last();
        var queryExceptLastWord = queryWords.Take(queryWords.Count() - 1);
        var searchText = string.Join(' ', queryExceptLastWord) + " ";

        var advertisementsQuery = _repository.GetAll<Advertisement>()
            .Include(a => a.Category)
            .AsQueryable();
        foreach (var word in queryWords)
        {
            advertisementsQuery = advertisementsQuery.Where(a => a.Title.ToLower().Contains(word));
        }

        var advertisements = advertisementsQuery
            .Select(a => new { a.Id, a.Title, a.CategoryId, CategoryTitle = a.Category.Title }).ToList();

        var groupedCategories = advertisements
            .GroupBy(a => a.CategoryId)
            .OrderByDescending(c => c.Count())
            .Take(5);

        var suggests = new List<AdvertisementSearchSuggestViewModel>();


        foreach (var category in groupedCategories)
        {
            var wordsList = category.OrderByDescending(a => a.Id).Take(500)
                .Where(a => !queryExceptLastWord.Except(a.Title.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Any())
                .SelectMany(a => a.Title
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.ToLower().Trim()));

            var similarWords = wordsList
                .GroupBy(s => s)
                .OrderByDescending(s => s.Count())
                .Where(s => !queryExceptLastWord.Any(q => s.Key == q) && s.Key.Contains(queryLastWord))
                .Take(3);

            suggests.AddRange(
                similarWords.Select(s => new AdvertisementSearchSuggestViewModel
                {
                    AdvertisementsCount = category.Count(),
                    CategoryId = category.Key,
                    CategoryTitle = category.First().CategoryTitle,
                    SearchSuggest = (searchText + s.Key).Trim(),
                })
            );
        }

        return suggests;
    }
}