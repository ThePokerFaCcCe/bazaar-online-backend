using BazaarOnline.Application.DTOs.AdvertisementDTOs;

namespace BazaarOnline.Application.Interfaces.Users;

public interface IUserAdvertisementService
{
    #region History

    void AddAdvertisementToUserHistory(string userId, int advertisementId);

    #endregion

    #region Note

    bool AddNote(CreateAdvertisementNoteDTO dto, string userId, int advertisementId);

    #endregion

    #region Bookmark

    AdvertisementBookmarkDTO AddOrRemoveBookmark(AdvertisementBookmarkDTO dto, string userId, int advertisementId);

    #endregion
}