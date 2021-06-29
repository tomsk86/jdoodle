using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace jdoodle.VIewModel
{
    public class SolveViewModel
    {
        [Required]
        public string Task { get; set; }

        public List<SelectListItem> Tasks { get; set; }

        public string Description { get; set; }

        [Required]
        public string SolutionCode { get; set; }
    }
}