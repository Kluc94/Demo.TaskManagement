// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Demo.TaskManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.TaskManagement.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Titul před")]
            public string DegreeBefore { get; set; }
            [Display(Name = "Jméno")]
            public string FirstName { get; set; }
            [Display(Name = "Příjmení")]
            public string LastName { get; set; }
            [Display(Name = "Titul za")]
            public string DegreeAfter { get; set; }
            [Display(Name = "Zobrazované jméno")]
            public string FullName { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                DegreeBefore = user.DegreeBefore,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DegreeAfter = user.DegreeAfter,
                FullName = user.FullName,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.DegreeBefore = Input.DegreeBefore;
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.DegreeAfter = Input.DegreeAfter;
            user.FullName = GetFullName();
            
            var updateResult = await _userManager.UpdateAsync(user);
            if(updateResult.Succeeded == false)
            {
                StatusMessage = "Unexpected error when trying to update informations";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private string GetFullName()
        {
            string fullName = string.Empty;

            if (string.IsNullOrEmpty(Input.DegreeBefore) == false)
            {
                fullName = Input.DegreeBefore.Trim();
            }

            if (string.IsNullOrEmpty(Input.FirstName) == false)
            {
                fullName += " " + Input.FirstName.Trim();
            }

            if (string.IsNullOrEmpty(Input.LastName) == false)
            {
                fullName += " " + Input.LastName.Trim();
            }

            if (string.IsNullOrEmpty(Input.DegreeAfter) == false)
            {
                fullName += ", " + Input.DegreeAfter.Trim();
            }

            return fullName;
        }
    }
}
