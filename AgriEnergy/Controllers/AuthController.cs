using Firebase.Auth;
using AgriEnergy.Logger;
using AgriEnergy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Common;
using System.Diagnostics;

namespace AgriEnergy.Controllers
{
    public class AuthController : Controller
    {
        private ILog iLog;
        private readonly AgriEnergyContext _context;

        FirebaseAuthProvider auth;

        public AuthController(AgriEnergyContext context)
        {
            _context = context;
            iLog = Log.GetInstance();
            // Replace YOUR_FIREBASE_API_KEY with your real key in user secrets or environment variables
            auth = new FirebaseAuthProvider(new FirebaseConfig("YOUR_FIREBASE_API_KEY")
            );
        }

        public IActionResult Registration()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("_UserToken")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult SignIn()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("_UserToken")))
            {

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AuthModel authModel)
        {
            try
            {
                var fbAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(authModel.Email, authModel.Password);

                if (fbAuthLink?.User == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create user in Firebase.");
                    return View(authModel);
                }

                string firebaseUid = fbAuthLink.User.LocalId;
                string token = fbAuthLink.FirebaseToken;

                if (token == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve a valid token.");
                    return View(authModel);
                }


                HttpContext.Session.SetString("_UserToken", token);
                HttpContext.Session.SetString("Role", "FARMER");

                var newUser = new AgriEnergy.Models.User
                {
                    FirebaseUid = firebaseUid,
                    Role = "FARMER",
                    Name = authModel.Name,
                    Dob = authModel.Dob,
                    Bio = authModel.Bio
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetInt32("UserId", newUser.UserId); // NEW LINE
                // Redirect after successful registration
                return RedirectToAction("Index", "Home");
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(string.Empty, "Registration error: " + firebaseEx.error.message);
                return View(authModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                return View(authModel);
            }
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(AuthModel authModel)
        {
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(authModel.Email, authModel.Password);
                string token = fbAuthLink.FirebaseToken;
                string uuid = fbAuthLink.User.LocalId;
                if (token != null)
                {
                    Models.User user = _context.Users.Where(id => id.FirebaseUid.Equals(uuid)).FirstOrDefault();

                    HttpContext.Session.SetString("Role", user.Role);
                    HttpContext.Session.SetString("_UserToken", token);
                    HttpContext.Session.SetInt32("UserId", user.UserId); // NEW LINE
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid login credentials.");
                    iLog.LogException("User Login Failed - " + authModel.Email);
                    return View(authModel);
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, "Login error: " + firebaseEx.error.message);
                iLog.LogException("User Login Failed - " + authModel.Email);
                return View(authModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, "An unexpected error occurred: " + ex.Message);
                iLog.LogException("User Login Failed - " + authModel.Email);
                return View(authModel);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            HttpContext.Session.Remove("UserId"); // NEW LINE
            HttpContext.Session.Remove("Role");
            return RedirectToAction("SignIn");
        }
    }
}
