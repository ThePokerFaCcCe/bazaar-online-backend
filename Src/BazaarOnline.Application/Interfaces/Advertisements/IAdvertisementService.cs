using BazaarOnline.Application.DTOs.AdvertisementDTOs;

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
}