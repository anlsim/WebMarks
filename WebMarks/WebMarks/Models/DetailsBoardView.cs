using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinBoard.Models
{
    public class DetailsBoardView
    {
        public int Id { get; set; } // Board ID

        public List<Pin> Pins { get; set; }
        public int PinId { get; set; }

        public DetailsBoardView(int pinId)
        {
            this.PinId = pinId;


        }

    }

   
}
