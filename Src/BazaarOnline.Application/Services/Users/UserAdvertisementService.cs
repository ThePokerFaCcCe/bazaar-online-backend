using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;

namespace BazaarOnline.Application.Services.Users;

public class UserAdvertisementService : IUserAdvertisementService
{
    private readonly IRepository _repository;

    public UserAdvertisementService(IRepository repository)
    {
        _repository = repository;
    }

    public void AddAdvertisementToUserHistory(string userId, int advertisementId)
    {
        var historyExists = _repository
            .GetAll<UserAdvertisementHistory>()
            .Any(u => u.UserId == userId && u.AdvertisementId == advertisementId);
        if (historyExists)
            return;

        var history = new UserAdvertisementHistory
        {
            UserId = userId,
            AdvertisementId = advertisementId,
        };
        _repository.Add(history);
        _repository.Save();
    }

    public bool AddNote(CreateAdvertisementNoteDTO dto, string userId, int advertisementId)
    {
        var note = _repository
            .GetAll<UserAdvertisementNote>()
            .SingleOrDefault(u => u.UserId == userId && u.AdvertisementId == advertisementId);

        if (note == null)
        {
            note = new UserAdvertisementNote
            {
                AdvertisementId = advertisementId,
                UserId = userId,
                Note = dto.Note,
            };
            _repository.Add(note);
        }
        else
        {
            note.Note = dto.Note;
            _repository.Update(note);
        }

        _repository.Save();
        return true;
    }

    public AdvertisementBookmarkDTO AddOrRemoveBookmark(AdvertisementBookmarkDTO dto, string userId,
        int advertisementId)
    {
        var bookmark = _repository
            .GetAll<UserAdvertisementBookmark>()
            .SingleOrDefault(u => u.UserId == userId && u.AdvertisementId == advertisementId);

        if (dto.IsBookmarked == false && bookmark != null)
        {
            _repository.Remove(bookmark);
        }
        else if (dto.IsBookmarked && bookmark == null)
        {
            _repository.Add(new UserAdvertisementBookmark
            {
                AdvertisementId = advertisementId,
                UserId = userId,
            });
        }

        _repository.Save();
        return dto;
    }
}