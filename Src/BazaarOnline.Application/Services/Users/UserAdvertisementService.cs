using BazaarOnline.Application.DTOs.AdvertisementDTOs;
using BazaarOnline.Application.Filters;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Utils;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Application.ViewModels.Users;
using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BazaarOnline.Application.Services.Users;

public class UserAdvertisementService : IUserAdvertisementService
{
    private readonly IRepository _repository;

    public UserAdvertisementService(IRepository repository)
    {
        _repository = repository;
    }

    public UserContactViewModel GetUserContactDetail(string userId)
    {
        var user = _repository.Get<User>(userId);
        return new UserContactViewModel
        {
            Success = true,
            //Email = user?.Email ?? string.Empty,
            //PhoneNumber = user?.PhoneNumber ?? string.Empty,
        }.FillFromObject(user);
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

    public IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementsHistory(string userId)
    {
        var advertisementIds = _repository.GetAll<UserAdvertisementHistory>()
            .Where(ua => ua.UserId == userId && !ua.IsDeleted)
            .OrderByDescending(ua => ua.CreateDate)
            .Select(ua => ua.AdvertisementId);


        return GetAdvertisementsListDetail(advertisementIds);
    }

    private IEnumerable<AdvertisementListDetailViewModel> GetAdvertisementsListDetail(IEnumerable<int> ids)
    {
        var advertisements = _repository.GetAll<Advertisement>()
            .Include(a => a.City)
            .Include(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Where(a => ids.Contains(a.Id) && a.StatusType == AdvertisementStatusTypeEnum.Accepted);
        var idsList = ids.ToList();

        return advertisements.ToList().Select(a =>
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
                        a.ContactType is AdvertisementContactTypeEnum.ChatOnly or AdvertisementContactTypeEnum.Normal,
                }.FillFromObject(a)
            }.FillFromObject(a);
        }).OrderBy(a => idsList.IndexOf(a.Id)); // order them with same order at ids list
    }

    public bool AddNote(CreateAdvertisementNoteDTO dto, string userId, int advertisementId)
    {
        dto.TrimStrings();
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

    public bool RemoveNote(string userId, int advertisementId)
    {
        var note = _repository
            .GetAll<UserAdvertisementNote>()
            .SingleOrDefault(u => u.UserId == userId && u.AdvertisementId == advertisementId);

        if (note != null)
        {
            _repository.Remove(note);
            _repository.Save();
        }

        return true;
    }

    public IEnumerable<AdvertisementNoteListDetailViewModel> GetUserNotes(string userId)
    {
        var advertisements = _repository.GetAll<UserAdvertisementNote>()
            .Include(ua => ua.Advertisement)
            .ThenInclude(a => a.City)
            .Include(ua => ua.Advertisement)
            .ThenInclude(a => a.Pictures)
            .ThenInclude(p => p.FileCenter)
            .Where(ua => ua.UserId == userId && ua.Advertisement.StatusType == AdvertisementStatusTypeEnum.Accepted)
            .OrderByDescending(ua => ua.CreateDate);

        return advertisements.ToList().Select(ua =>
        {
            var advertisement = ua.Advertisement;
            var firstPic = advertisement.Pictures.MinBy(p => p.Id);
            AdvertisementPictureViewModel? picture = null;
            if (firstPic != null)
                picture = new AdvertisementPictureViewModel().FillFromObject(firstPic.FileCenter);

            var cleanedNoteText = ua.Note.Split(new[] { ' ', '\n' }).Where(s => !string.IsNullOrEmpty(s));
            var note = string.Join(' ', cleanedNoteText);

            return new AdvertisementNoteListDetailViewModel()
            {
                Data = new AdvertisementNoteListDetailDataViewModel()
                {
                    Note = note,
                    Picture = picture,
                    LocationText = advertisement.City.Name,
                    TimeText = advertisement.UpdateDate.PassedFromNowString(),
                    IsChatEnabled =
                        advertisement.ContactType is AdvertisementContactTypeEnum.ChatOnly
                            or AdvertisementContactTypeEnum.Normal,
                }.FillFromObject(advertisement)
            }.FillFromObject(advertisement);
        });
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

    public IEnumerable<AdvertisementListDetailViewModel> GetUserBookmarks(string userId)
    {
        var advertisementIds = _repository.GetAll<UserAdvertisementBookmark>()
            .Where(ua => ua.UserId == userId)
            .OrderByDescending(ua => ua.CreateDate)
            .Select(ua => ua.AdvertisementId);


        return GetAdvertisementsListDetail(advertisementIds);
    }
}