using Clean.Application.Abstractions;
using QRCoder;
using SkiaSharp;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQrWithLogo(string qrText, string logoPath, int size = 1500)
    {
        // Generate QR code data (matrix of pixels)
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.H);

        // Create SkiaSharp surface
        using var surface = SKSurface.Create(new SKImageInfo(size, size));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        // Get module count (QR grid size)
        int moduleCount = qrCodeData.ModuleMatrix.Count;

        // Calculate pixels per module
        float pixelsPerModule = (float)size / moduleCount;

        using var paint = new SKPaint
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Fill,
            IsAntialias = false
        };

        // Draw QR code modules
        for (int y = 0; y < moduleCount; y++)
        {
            for (int x = 0; x < moduleCount; x++)
            {
                if (qrCodeData.ModuleMatrix[y][x])
                {
                    var rect = new SKRect(
                        x * pixelsPerModule,
                        y * pixelsPerModule,
                        (x + 1) * pixelsPerModule,
                        (y + 1) * pixelsPerModule
                    );
                    canvas.DrawRect(rect, paint);
                }
            }
        }

        // Draw logo in the center
        using var logoBitmap = SKBitmap.Decode(logoPath);
        int logoSize = size / 4;
        var resizedLogo = logoBitmap.Resize(new SKImageInfo(logoSize, logoSize), SKFilterQuality.High);

        if (resizedLogo != null)
        {
            float x = (size - logoSize) / 2f;
            float y = (size - logoSize) / 2f;
            canvas.DrawBitmap(resizedLogo, x, y);
        }

        // Export final PNG
        canvas.Flush();
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        return data.ToArray();
    }
    
    public byte[] GenerateStudentQr(int studentId, string logoPath, int size = 1500)
    {
        string url = $"https://api.yourwebsite.tj/students/{studentId}";
        return GenerateQrWithLogo(url, logoPath, size);
    }

}
