namespace Clean.Application.Abstractions;

public interface IQrCodeService
{
    public byte[] GenerateQrWithLogo(string qrText, string logoPath, int size = 250);
}