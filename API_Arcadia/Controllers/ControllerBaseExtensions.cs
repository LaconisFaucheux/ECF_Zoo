using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Arcadia.Migrations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Net;

namespace API_Arcadia.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static ActionResult CustomErrorResponse<T>(this ControllerBase controller, Exception ex,
            T? entity, ILogger logger, [CallerMemberName] string? action = null)
        {
            if (ex is DbUpdateConcurrencyException)
            {
                return controller.Problem("L'entité ou au moins l'une de ses entités filles n'existe pas en base.", null, (int)HttpStatusCode.NotFound, "Aucune modification enregistrée en base.");
            }
            if (ex is DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                logger.LogWarning("Action {action}, entité de type {type}\n{detail}\n{entity}", 
                    action,
                    entity?.GetType().Name,
                    pb.Detail,
                    JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = true }));

                return controller.Problem(pb.Detail, null, pb.Status, pb.Title);
            }

            else throw ex;
        }

    }
}
