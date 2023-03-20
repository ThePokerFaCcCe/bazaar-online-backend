using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.ViewModels.Advertisements;

namespace BazaarOnline.Application.Interfaces.Advertisements;

public interface IAdvertisementService
{
    /// <summary>
    /// Create an advertisement and return it's Id when succeed
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="userId"></param>
    /// <returns>Created ad Id on success or -1 on failure</returns>
    int CreateAdvertisement(CreateAdvertisementDTO dto, string userId);

    bool IsAdvertisementExists(int id);

    IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementList(AdvertisemenFilterDTO filterDto);

    AdvertisementDetailViewModel? GetAdvertisementDetail(int id, bool acceptedOnly = false, string? userId = null);
}