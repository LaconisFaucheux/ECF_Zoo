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
            int entityId,
            bool needsMini = true) where T:class
        {
            string[] SupportedExtensions = [".jpg", ".jpeg", ".png"];
            var fileExtension = Path.GetExtension(image.FileName);

            if (!SupportedExtensions.Contains(fileExtension))
            {
                throw new InvalidImageContentException("Format d'image non supporté");
            }

            string fileName = $"{DateTime.Now.Ticks}_{entityName}{fileExtension}";
            string slug = Path.Combine("images", folderName, fileName);
            string storagePath = Path.Combine("wwwroot", slug);


            string fileNameMini = $"{DateTime.Now.Ticks}_{entityName}_mini{fileExtension}";            
            string miniSlug = Path.Combine("images", folderName, fileNameMini);            
            string storagePathMini = Path.Combine("wwwroot", miniSlug);

            using (var stream = new FileStream(storagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            if (needsMini)
            {
                ResizeImage(storagePath, storagePathMini, 300, 300);
            }            

            if (typeof(T) == typeof(AnimalImage))
            {
                var animalImage = new AnimalImage
                {
                    Slug = slug,
                    MiniSlug = miniSlug,
                    IdAnimal = entityId
                };
                return animalImage as T;
            }
            else if (typeof(T) == typeof(HabitatImage))
            {
                var habitatImage = new HabitatImage
                {
                    Slug = slug,
                    MiniSlug = miniSlug,
                    IdHabitat = entityId
                };
                return habitatImage as T;
            }
            else if (typeof(T) == typeof(string))
            {
                return slug as T;
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
