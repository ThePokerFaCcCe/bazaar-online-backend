using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Domain.Entities.Advertisements;
using Humanizer;

public class AdvertisementSelfListDetailViewModel
{
    public int Id { get; set; }
    public AdvertisementSelfListDetailDataViewModel Data { get; set; }
}

public class AdvertisementSelfListDetailDataViewModel : AdvertisementListDetailDataViewModel
{
    public AdvertisementStatusTypeEnum StatusType { get; set; }
    public string StatusTypeName => StatusType.GetDisplayName();
    public string StatusReason { get; set; } = string.Empty;
}