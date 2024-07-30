using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using API_Arcadia.Models;
using System.Text.RegularExpressions;

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


        public static async Task<T?> UploadImage<T>(
            IFormFile image, 
            string folderName, 
            string entityName, 
            int entityId ) where T:class
        {
            string[] SupportedExtensions = [".jpg", ".jpeg", ".png"];
            var fileExtension = Path.GetExtension(image.FileName);

            if (!SupportedExtensions.Contains(fileExtension))
            {
                throw new InvalidImageContentException("Format d'image non supporté");
            }

            string fileName = $"{DateTime.Now.Ticks}_{entityName}{fileExtension}";
            string fileNameMini = $"{DateTime.Now.Ticks}_{entityName}_mini{fileExtension}";
            string storagePath = Path.Combine("wwwroot", "Images", folderName, fileName);
            string storagePathMini = Path.Combine("wwwroot", "Images", folderName, fileNameMini);
            using (var stream = new FileStream(storagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            Utils.ResizeImage(storagePath, storagePathMini, 300, 300);

            if (typeof(T) == typeof(AnimalImage))
            {
                var animalImage = new AnimalImage
                {
                    Slug = storagePath,
                    MiniSlug = storagePathMini,
                    IdAnimal = entityId
                };
                return animalImage as T;
            }
            else if (typeof(T) == typeof(HabitatImage))
            {
                var habitatImage = new HabitatImage
                {
                    Slug = storagePath,
                    MiniSlug = storagePathMini,
                    IdHabitat = entityId
                };
                return habitatImage as T;
            }
            else
            {
                return null;
            }
        }

        public static bool CheckEmailValidity(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
