using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CusComputer
    {
        public int Number { get; set; }
        public int AssembledYear { get; set; }
        public string Building { get; set; }
        public int RoomNo { get; set; }

        public CusComputer()
        {

        }

        public CusComputer(int number, int assembled, string building, int roomNo)
        {
            this.Number = number;
            this.AssembledYear = assembled;
            this.Building = building;
            this.RoomNo = roomNo;
        }
    }
}