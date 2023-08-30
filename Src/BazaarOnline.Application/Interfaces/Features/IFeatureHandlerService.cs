using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.ViewModels.Categories.CategoryFeatures;

namespace BazaarOnline.Application.Interfaces.Features;

public interface IFeatureHandlerService
{
    #region CategoryFeatures

    /// <summary>
    /// Return category & it's parent features
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    IEnumerable<CategoryFeaturesListDetailViewModel> GetFeaturesList(int categoryId);

    /// <summary>
    /// return all features of advertisement's category and their value(if has value)
    /// </summary>
    /// <param name="advertisementId"></param>
    /// <returns></returns>
    IEnumerable<CategoryFeaturesListDetailViewModel> GetAdvertisementFeaturesList(int advertisementId);

    #endregion

    #region AdvertisementFeatures

    OperationResultDTO ValidateAdvertisementFeatures(int categoryId,
        IEnumerable<CreateAdvertisementFeatureDTO> features);

    #endregion
}