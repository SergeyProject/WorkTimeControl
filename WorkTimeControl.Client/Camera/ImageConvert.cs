using System.Drawing;
using System.IO;

namespace WorkTimeControl.Client.Camera
{
   public static class ImageConvert
    {
        // Convert Bitmap to Byte[]
        public static byte[] ConvertToByte(Bitmap bmp)
        {
            MemoryStream memoryStream = new MemoryStream();
            // Конвертируем в массив байтов с сжатием Jpeg
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }


        // Convert Byte[] to Image
        public static Image ByteToImage(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
