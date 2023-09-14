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
    public bool IsBlockedUserBySelf { get; set; } = false;
    public bool IsBlockedByUser { get; set; } = false;

    public ConversationDetailUserViewModel User { get; set; }
        = new ConversationDetailUserViewModel();

    public ConversationDetailAdvertisementViewModel Advertisement { get; set; }
        = new ConversationDetailAdvertisementViewModel();

    public int AllCount { get; set; } = 0;

    public Dictionary<Guid, MessageDetailViewModel> Messages { get; set; } =
        new Dictionary<Guid, MessageDetailViewModel>();
}

public class ConversationDetailUserViewModel
{
    public string Id { get; set; } = string.Empty;

    public ConversationDetailUserDataViewModel Data { get; set; } =
        new ConversationDetailUserDataViewModel();
}

public class ConversationDetailUserDataViewModel
{
    public string DisplayName { get; set; } = string.Empty;
    public DateTime LastSeen { get; set; } = DateTime.MinValue;
    public int AnswerHourStart { get; set; } = 0;
    public int AnswerHourEnd { get; set; } = 23;
}

public class ConversationDetailAdvertisementViewModel
{
    public int Id { get; set; } = 0;

    public ConversationDetailAdvertisementDataViewModel Data { get; set; }
        = new ConversationDetailAdvertisementDataViewModel();
}

public class ConversationDetailAdvertisementDataViewModel
{
    public string Title { get; set; } = string.Empty;
    public AdvertisementPictureViewModel? Picture { get; set; } = null;
}