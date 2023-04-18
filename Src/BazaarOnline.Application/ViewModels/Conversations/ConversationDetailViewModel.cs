using BazaarOnline.Application.ViewModels.Advertisements;

namespace BazaarOnline.Application.ViewModels.Conversations;

public class ConversationDetailViewModel
{
    public Guid Id { get; set; }

    public ConversationDetailDataViewModel Data { get; set; }
        = new ConversationDetailDataViewModel();
}

public class ConversationDetailDataViewModel
{
    public ConversationDetailDataUserViewModel User { get; set; }
        = new ConversationDetailDataUserViewModel();

    public ConversationDetailDataAdvertisementViewModel Advertisement { get; set; }
        = new ConversationDetailDataAdvertisementViewModel();

    public MessageDetailViewModel? LastMessage { get; set; } = null;
}

public class ConversationDetailDataUserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int AnswerHourStart { get; set; } = 0;
    public int AnswerHourEnd { get; set; } = 23;

}

public class ConversationDetailDataAdvertisementViewModel
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = string.Empty;
    public AdvertisementPictureViewModel? Picture { get; set; } = null;
}