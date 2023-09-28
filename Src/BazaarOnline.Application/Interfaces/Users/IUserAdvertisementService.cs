using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Application.ViewModels.Users;

namespace BazaarOnline.Application.Interfaces.Users;

public interface IUserAdvertisementService
{
    UserContactViewModel GetUserContactDetail(string userId);

    #region History

    void AddAdvertisementToUserHistory(string userId, int advertisementId);
    IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementsHistory(string userId);
    bool DeleteAdvertisementHistory(string userId,int advertisementId);
    

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