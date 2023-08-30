using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Interfaces.Features;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;
using BazaarOnline.Application.ViewModels.Features;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Entities.Features;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BazaarOnline.Application.Services.Features;

public class FeatureHandlerService : IFeatureHandlerService
{
    private readonly IRepository _repository;

    public FeatureHandlerService(IRepository repository)
    {
        _repository = repository;
    }

    private IQueryable<CategoryFeature> GetCategoryAndParentsFeatures(int categoryId)
    {
        var categoryIds = new List<int>();

        var categories = _repository.GetAll<Category>()
            .Select(c => new { c.Id, c.ParentCategoryId })
            .ToList();

        int? searchId = categoryId;
        do
        {
            categoryIds.Add((int)searchId);
            searchId = categories.SingleOrDefault(c => c.Id == searchId)?.ParentCategoryId;
        } while (searchId != null);


        return _repository.GetAll<CategoryFeature>()
            .Include(cf => cf.Feature)
            .ThenInclude(f => f.SelectType)
            .Include(cf => cf.Feature)
            .ThenInclude(f => f.StringType)
            .Include(cf => cf.Feature)
            .ThenInclude(f => f.IntegerType)
            //-----------------------------------------------------------------------------------------//
            .Where(cf => categoryIds.Contains(cf.CategoryId))
            .OrderBy(cf => cf.SortNumber);
    }

    public IEnumerable<CategoryFeaturesListDetailViewModel> GetFeaturesList(int categoryId)
    {
        var categoryFeatures = GetCategoryAndParentsFeatures(categoryId);

        return categoryFeatures.ToList().Select(cf =>
            {
                var model = new CategoryFeaturesListDetailViewModel
                {
                    Data = new CategoryFeaturesListFeatureDetailViewModel().FillFromObject(cf.Feature, false),
                }.FillFromObject(cf);
                model.Data.IsRequired = cf.IsRequired;

                object? typeObjectViewModel = cf.Feature.Type switch
                {
                    FeatureTypeEnum.Integer => new FeatureIntegerTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    FeatureTypeEnum.Select => new FeatureSelectTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    FeatureTypeEnum.String => new FeatureStringTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    _ => null
                };
                model.Data.FillFromObject(typeObjectViewModel);

                return model;
            }
        );
    }

    public IEnumerable<CategoryFeaturesListDetailViewModel> GetAdvertisementFeaturesList(int advertisementId)
    {
        var advertisement = _repository.GetAll<Advertisement>()
            .Include(a => a.AdvertisementFeatures)
            .SingleOrDefault(a => a.Id == advertisementId);
        if (advertisement == null) return Enumerable.Empty<CategoryFeaturesListDetailViewModel>();

        var categoryFeatures = GetCategoryAndParentsFeatures(advertisement.CategoryId);

        return categoryFeatures.ToList().Select(cf =>
            {
                var model = new CategoryFeaturesListDetailViewModel
                {
                    Data = new CategoryFeaturesListFeatureDetailViewModel().FillFromObject(cf.Feature, false),
                }.FillFromObject(cf);
                model.Data.IsRequired = cf.IsRequired;

                var featureValue =
                    advertisement.AdvertisementFeatures.SingleOrDefault(af => af.CategoryFeatureId == cf.Id);

                model.Data.Value = featureValue?.Value ?? string.Empty;

                object? typeObjectViewModel = cf.Feature.Type switch
                {
                    FeatureTypeEnum.Integer => new FeatureIntegerTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    FeatureTypeEnum.Select => new FeatureSelectTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    FeatureTypeEnum.String => new FeatureStringTypeViewModel().FillFromObject(cf.Feature.TypeObject),
                    _ => null
                };
                model.Data.FillFromObject(typeObjectViewModel);

                return model;
            }
        );
    }

    public OperationResultDTO ValidateAdvertisementFeatures(int categoryId,
        IEnumerable<CreateAdvertisementFeatureDTO> features)
    {
        // TODO - Refactor
        var categoryFeatures = GetCategoryAndParentsFeatures(categoryId).ToList();

        if (!categoryFeatures.Any())
            return new OperationResultDTO { IsSuccess = true };
        var errors = new Dictionary<int, string>();

        foreach (var feature in features)
        {
            var categoryFeature = categoryFeatures.FirstOrDefault(cf => cf.Id == feature.Id);

            if (categoryFeature == null)
            {
                errors.Add(feature.Id, $"این ویژگی مجاز نیست");
                continue;
            }

            if (categoryFeature.Feature.Type == FeatureTypeEnum.Integer)
            {
                var intType = categoryFeature.Feature.IntegerType;

                if (!long.TryParse(feature.Value, out long value))
                    errors.Add(feature.Id, "لطفا عدد وارد کنید");
                else if (value < intType.Minimum)
                    errors.Add(feature.Id, $"عدد بزرگتر از {intType.Minimum} وارد کنید");
                else if (value > intType.Maximum)
                    errors.Add(feature.Id, $"عدد کوچکتر از {intType.Maximum} وارد کنید");
            }
            else if (categoryFeature.Feature.Type == FeatureTypeEnum.String)
            {
                var stringType = categoryFeature.Feature.StringType;

                var value = feature.Value.Trim();
                if (string.IsNullOrEmpty(value))
                    errors.Add(feature.Id, "لطفا متن معتبر وارد کنید");
                else if (value.Length < stringType.MinLength)
                    errors.Add(feature.Id, $"متن بیشتر از {stringType.MinLength} کاراکتر وارد کنید");
                else if (value.Length > stringType.MaxLength)
                    errors.Add(feature.Id, $"متن کمتر از {stringType.MaxLength} کاراکتر وارد کنید");
            }
            else if (categoryFeature.Feature.Type == FeatureTypeEnum.Select)
            {
                var options = categoryFeature.Feature.SelectType.OptionsList;

                var value = feature.Value.Trim();
                if (!options.Contains(value))
                    errors.Add(feature.Id, $"مقدار انتخاب شده معتبر نیست");
            }
        }

        var enteredFeatureIds = features.Select(f => f.Id);

        var requiredFeatureIds = categoryFeatures
            .Where(cf => cf.IsRequired)
            .Select(cf => cf.Id);

        var notEnteredRequiredFeatures = requiredFeatureIds.Except(enteredFeatureIds).ToList();
        notEnteredRequiredFeatures.ForEach(id => errors.Add(id, $"این فیلد اجباری است"));

        if (errors.Any())
        {
            return new OperationResultDTO()
            {
                IsSuccess = false,
                Message = JsonConvert.SerializeObject(errors),
            };
        }

        return new OperationResultDTO { IsSuccess = true };
    }
}