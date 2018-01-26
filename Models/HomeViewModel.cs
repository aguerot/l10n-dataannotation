using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace l10n_dataannotation.Models
{
    public class HomeViewModel: BaseViewModel
    {
        [Display(Name = "TITLE")]
        [Required]
        public string Title { get; set; }
    }

    public class BaseViewModel
    {
        [Display(Name = "COMMON")]
        [Required]
        public string Common { get; set; }
    }
}
