using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            // Chemin relatif au dossier d'images dans le projet API
            //string relativeImagePath = Path.Combine("Assets", "Images", "Animals", imageName);

            // Chemin d'accès physique au dossier d'images sur le serveur
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), imageName);

            // Vérifier si le fichier image existe
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(); // L'image n'existe pas, retourner une réponse 404 Not Found
            }

            // Lire l'image en tant que tableau de bytes
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Retourner l'image en tant que réponse HTTP avec le bon type de contenu
            return File(imageBytes, "image/jpeg"); // Vous pouvez ajuster le type de contenu en fonction du type de l'image
        }
    }
}
