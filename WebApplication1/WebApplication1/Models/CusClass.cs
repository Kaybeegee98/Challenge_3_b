using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CusClass
    {
        public string ClassCode { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public int RoomNo { get; set; }

        public CusClass()
        {

        }

        public CusClass(string classcode, string name, string building, int roomNo)
        {
            ClassCode = classcode;
            Name = name;
            Building = building;
            RoomNo = roomNo;
        }
    }
}