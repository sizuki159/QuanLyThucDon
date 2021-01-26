using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public class Person : Human
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ThucDonHangNgay MyMenu { get; set; }

        public Person() { }
        public Person(string username, string password, string fullname, ushort age, string phone)
        {
            this.Username = username;
            this.Password = password;
            this.HoTen = fullname;
            this.Age = age;
            this.Phone = phone;
        }
        public Person(Person ps)
        {
            this.Username = ps.Username;
            this.Password = ps.Password;
            this.Age = ps.Age;
            this.HoTen = ps.HoTen;
            this.Phone = ps.Phone;
        }
        public string xuatThucDon()
        {
            string res = String.Format("Thuc don cua {0} - {1} tuoi la:\n", this.HoTen, this.Age);
            res += this.MyMenu.ToString();
            return res;
        }
        public override string ToString()
        {
            return String.Format("Ho va ten {0} , tuoi {1}", this.HoTen, this.Age);
        }
    }
}