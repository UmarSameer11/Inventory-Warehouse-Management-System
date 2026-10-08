using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Auth;
using WarehouseManagementSystemWeb.Models.Auth;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAuthApiClient _authApi;

        public AccountController(IAuthApiClient authApi)
        {
            _authApi = authApi;
        }

        // ---------------------------------------------------------------- Login

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToLocal(returnUrl);
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Password = string.Empty;
                return View(model);
            }

            var result = await _authApi.LoginAsync(model.Email.Trim(), model.Password, HttpContext.RequestAborted);

            if (!result.Success || result.Data is null)
            {
                // Same text for wrong e-mail / wrong password comes from the API (no user enumeration).
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Login failed.");
                model.Password = string.Empty;
                return View(model);
            }

            await AuthSignIn.SignInAsync(HttpContext, result.Data, model.RememberMe);

            return RedirectToLocal(model.ReturnUrl);
        }

        // ---------------------------------------------------------------- Logout

        /// <summary>Logs out this browser: ends the API session and deletes the cookie.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authApi.LogoutAsync(HttpContext.RequestAborted); // best effort - cookie is removed in any case
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction(nameof(Login));
        }

        /// <summary>Logs out every device of this user.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAll()
        {
            await _authApi.LogoutAllAsync(HttpContext.RequestAborted);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["SuccessMessage"] = "You have been logged out from all devices.";
            return RedirectToAction(nameof(Login));
        }

        // ---------------------------------------------------------------- Profile + devices

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await _authApi.GetProfileAsync(HttpContext.RequestAborted);
            var sessions = await _authApi.GetSessionsAsync(HttpContext.RequestAborted);

            if (!profile.Success || profile.Data is null)
            {
                TempData["ErrorMessage"] = profile.ErrorMessage ?? "Could not load your profile.";
                return RedirectToAction("Index", "Employee");
            }

            return View(new ProfilePageViewModel
            {
                Profile = profile.Data,
                Sessions = sessions.Data ?? new List<SessionModel>()
            });
        }

        /// <summary>Logs out one specific device from the list on the profile page.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RevokeSession(Guid sessionId)
        {
            var result = await _authApi.RevokeSessionAsync(sessionId, HttpContext.RequestAborted);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage ?? "Could not log out that device.";
                return RedirectToAction(nameof(Profile));
            }

            // Revoking the session we are using right now = logging out.
            if (AuthSignIn.GetSessionId(User) == sessionId.ToString())
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["SuccessMessage"] = "You have been logged out.";
                return RedirectToAction(nameof(Login));
            }

            TempData["SuccessMessage"] = "Device logged out.";
            return RedirectToAction(nameof(Profile));
        }

        // ---------------------------------------------------------------- Change password

        [HttpGet]
        public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new ChangePasswordViewModel());
            }

            var result = await _authApi.ChangePasswordAsync(model, HttpContext.RequestAborted);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Password could not be changed.");
                return View(new ChangePasswordViewModel());
            }

            TempData["SuccessMessage"] = "Password changed. Your other devices have been logged out.";
            return RedirectToAction(nameof(Profile));
        }

        // ---------------------------------------------------------------- Register a user (Admin only)

        [HttpGet]
        [Authorize(Roles = AuthConstants.AdminRole)]
        public IActionResult Register() => View(NewRegisterModel());

        [HttpPost]
        [Authorize(Roles = AuthConstants.AdminRole)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RegisterView(model);
            }

            var result = await _authApi.RegisterAsync(model, HttpContext.RequestAborted);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "User could not be created.");
                return RegisterView(model);
            }

            TempData["SuccessMessage"] = $"User {model.Email} created as {model.Role}.";
            return RedirectToAction(nameof(Register));
        }

        // ---------------------------------------------------------------- Access denied

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        // ---------------------------------------------------------------- helpers

        private IActionResult RedirectToLocal(string? returnUrl) =>
            !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? LocalRedirect(returnUrl)
                : RedirectToAction("Index", "Employee");

        private static RegisterUserViewModel NewRegisterModel() => new() { Role = "Employee" };

        private IActionResult RegisterView(RegisterUserViewModel model)
        {
            // never send passwords back to the browser
            model.Password = string.Empty;
            model.ConfirmPassword = string.Empty;
            return View(model);
        }
    }
}
