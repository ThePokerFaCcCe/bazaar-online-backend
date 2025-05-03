using BazaarOnline.Application.DTOs;
using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.DTOs.PaginationDTO;
using BazaarOnline.Application.DTOs.SitemapDTOs;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Domain.Entities.Advertisements;

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

    OperationResultDTO UpdateAdvertisement(int id, UpdateAdvertisementDTO dto);

    OperationResultDTO UpdateAdvertisementPictures(int id, UpdateAdvertisementPictureDTO dto);

    bool IsAdvertisementExists(int id);
    bool IsAdvertisementExists(int advertisementId, string userId);
    Advertisement? GetAdvertisement(int id);

    PaginationResultGenericListDTO<AdvertisementSelfListDetailViewModel> GetSelfAdvertisementList(string userId,
        PaginationFilterDTO pagination);

    OperationResultDTO UpdateAdvertisementStatus(int id, AdvertisementUpdateStatusDTO dto);

    PaginationResultGenericListDTO<AdvertisementListDetailViewModel> GetAdvertisementList(AdvertisemenFilterDTO filterDto, PaginationFilterDTO pagination);
    List<SitemapAdvertisementDTO> GetAdvertisementSitemap();

    AdvertisementDetailViewModel? GetAdvertisementDetail(int id, bool acceptedOnly = false, string? userId = null);

    IEnumerable<AdvertisementSearchSuggestViewModel> SearchSuggestAdvertisement(AdvertisementSearchDTO searchDto);
}