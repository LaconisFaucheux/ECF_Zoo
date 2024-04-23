using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;

namespace API_Arcadia
{
    public static class Utils
    {
        public static void ResizeImage(string inputPath, string outputPath, int width, int height)
        {
            using (Image image = Image.Load(inputPath))
            {
                image.Mutate(x => x
                    .Resize(width, height));

                image.Save(outputPath);
            }
        }
    }
}
