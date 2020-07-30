using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        //The model defines the validation rules, the controller enforces them.
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Location name must be between 3 and 50 characters.")]
        public string Location { get; set; }

    }
}
