namespace BazaarOnline.Application.DTOs.UploadCenter;

public class UploadFileResultDTO
{
    public bool Success { get; set; }

    public int Id { get; set; }

    public long SizeKB { get; set; }
}