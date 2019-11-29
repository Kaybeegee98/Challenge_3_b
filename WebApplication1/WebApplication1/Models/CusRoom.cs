using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CusRoom
    {
        public string Building { get; set; }
        public int RoomNo { get; set; }
        public int Capacity { get; set; }

        public CusRoom()
        {
        }

        public CusRoom(string building, int roomNo, int capacity)
        {
            this.Building = building;
            this.RoomNo = roomNo;
            this.Capacity = capacity;

        }
    }
}