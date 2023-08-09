using BazaarOnline.Application.Utils.Attributes;

namespace BazaarOnline.Application.ViewModels.Conversations;

public class BlocklistViewModel
{
    public string Id { get; set; }
    public BlocklistDataViewModel Data { get; set; }
}

public class BlocklistDataViewModel
{
    public string DisplayName { get; set; }

    [FillerProperty(PropertyName = "CreateDate")]
    public DateTime BlockDate { get; set; }
}