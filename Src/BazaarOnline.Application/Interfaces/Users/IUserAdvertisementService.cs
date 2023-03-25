using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.ViewModels.Advertisements;

namespace BazaarOnline.Application.Interfaces.Users;

public interface IUserAdvertisementService
{
    #region History

    void AddAdvertisementToUserHistory(string userId, int advertisementId);
    IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementsHistory(string userId);

    #endregion

    #region Note

    bool AddNote(CreateAdvertisementNoteDTO dto, string userId, int advertisementId);
    bool RemoveNote(string userId, int advertisementId);
    IEnumerable<AdvertisementNoteListDetailViewModel> GetUserNotes(string userId);

    #endregion

    #region Bookmark

    AdvertisementBookmarkDTO AddOrRemoveBookmark(AdvertisementBookmarkDTO dto, string userId, int advertisementId);
    IEnumerable<AdvertisementListDetailViewModel> GetUserBookmarks(string userId);

    #endregion
}