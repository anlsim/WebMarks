using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.ComponentModel.DataAnnotations;

namespace PinBoard.Models
{
    public  class CreatePinViewModel
    {
     
        public int Id { get; set; }

        [Display(Name = "Url")]
        [Required]
        public string Url { get; set; }
        [Required]
        public List<Board> Boards;
        public int BoardId { get; set; }

    }
}
