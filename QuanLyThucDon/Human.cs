using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public abstract class Human
    {
        public string HoTen { get; set; }
        public ushort Age { get; set; }
        public string Phone { get; set; }

        public Human() { }
        public Human(string ten, ushort tuoi, string sdt)
        {
            this.HoTen = ten;
            this.Age = tuoi;
            this.Phone = sdt;
        }
        public Human(Human hm)
        {
            this.HoTen = hm.HoTen;
            this.Age = hm.Age;
            this.Phone = hm.Phone;
        }
    }
}
