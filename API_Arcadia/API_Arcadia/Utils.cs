using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using API_Arcadia.Models;

namespace API_Arcadia
{
    public static class Utils
    {
        public static void ResizeImage(string inputPath, string outputPath, int width, int height)
        {
            //sans préservation du ratio
            //using (Image image = Image.Load(inputPath))
            //{
            //    image.Mutate(x => x
            //        .Resize(width, height));

            //    image.Save(outputPath);
            //}

            //avec préservation du ratio
            using (Image image = Image.Load(inputPath))
            {
                // Calculer les dimensions proportionnelles pour conserver le ratio de l'image
                float aspectRatio = (float)image.Width / image.Height;
                int newWidth = width;
                int newHeight = (int)(newWidth / aspectRatio);

                // Si la hauteur calculée dépasse la hauteur cible, recalculer la largeur pour conserver le ratio
                if (newHeight > height)
                {
                    newHeight = height;
                    newWidth = (int)(newHeight * aspectRatio);
                }

                image.Mutate(x => x
                    .Resize(newWidth, newHeight));

                image.Save(outputPath);
            }
        }



        //TODO T = Animal ou Habitat, U = HabitatImage ou AnimalImage? A creuser!
        //public async static U UploadImage<T, U>(IFormFile image, T entity)
        //{
        //    var t = typeof(T).Name;
        //    var fileExtension = Path.GetExtension(image.FileName);
        //    string storagePath = string.Empty;
        //    string storagePathMini = string.Empty;
        //    string fileName = $"{DateTime.Now.Ticks}_{animal.Name}{fileExtension}";
        //    string fileNameMini = $"{DateTime.Now.Ticks}_{animal.Name}_mini{fileExtension}";

        //    switch (t.ToLower())
        //    {
        //        case "animal":
        //            storagePath = Path.Combine("Assets\\Images\\Animals", fileName);
        //            storagePathMini = Path.Combine("Assets\\Images\\Animals", fileNameMini);
        //            string fileName = $"{DateTime.Now.Ticks}_{animal.Name}{fileExtension}";
        //            string fileNameMini = $"{DateTime.Now.Ticks}_{animal.Name}_mini{fileExtension}";

        //            break;
        //    }

            


        //    using (var stream = new FileStream(storagePath, FileMode.Create))
        //    {
        //        await image.CopyToAsync(stream);
        //    }

        //    Utils.ResizeImage(storagePath, storagePathMini, 50, 50);
        //}

        public static void DeleteImage()
        {

        }
    }
}
