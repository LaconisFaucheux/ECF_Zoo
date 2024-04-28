﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Duende.IdentityServer.Services;

namespace IdentityServerHost.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ArcadiaUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutModel(SignInManager<ArcadiaUser> signInManager, ILogger<LogoutModel> logger, IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            return await OnPost(returnUrl);
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            string logoutId = Request.Query["logoutId"].ToString();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }

            if (!string.IsNullOrEmpty(logoutId))
            {
                //Récupère l'URL vers laquelle rediriger après déconnexion
                var req = await _interaction.GetLogoutContextAsync(logoutId);
                returnUrl = req.PostLogoutRedirectUri;

                //redirige vers cette URL si elle existe
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            }

            return Page();

            //else
            //{
            //    // This needs to be a redirect so that the browser performs a new
            //    // request and the identity for the user gets updated.
            //    return RedirectToPage();
            //}
        }
    }
}
