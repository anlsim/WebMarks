using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinBoard.Models
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Pin> Pins { get; set; }
        public IdentityUser User { get; set; }
    }
}
