namespace BazaarOnline.Application.DTOs.UploadCenter;

public class UploadFileResultDTO
{
    public int Id { get; set; }=0;

    public long SizeKB { get; set; } = 0;

    public string? ExtraProperties { get; set; }= null;

}