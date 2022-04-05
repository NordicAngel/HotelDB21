using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Exceptions;
using RazorPageHotelApp.Interfaces;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        private IConfiguration config;
        private IHotelService hotelService;
        [BindProperty]
        public Models.Hotel Hotel { get; set; }

        public string ErrorMsg { get; set; }

        public CreateModel(IConfiguration config, IHotelService hotelService)
        {
            this.config = config;
            this.hotelService = hotelService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {//TODO Make prettier validation
            try
            {
                await hotelService.CreateHotelAsync(Hotel);
            }
            catch (DatabaseException ex)
            {
                ErrorMsg = ex.Message;
                return Page();
            }
            
            return RedirectToPage("GetAllHotels");
        }
    }
}
