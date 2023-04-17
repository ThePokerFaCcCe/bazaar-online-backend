namespace BazaarOnline.Domain.Entities.UploadCenter;

public class FileCenter
{
    public int Id { get; set; }

    public string FileName { get; set; }

    public string FileType { get; set; }

    public long SizeKB { get; set; }

    public string? ExtraProperties { get; set; }

    public FileCenterTypeEnum UsageType { get; set; }
}