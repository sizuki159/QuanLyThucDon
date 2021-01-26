using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public class ThucAn : MonAn
    {
        public ThucAn() { }
        public ThucAn(string name, int calo, NGUYENLIEU nguyenlieu)
        {
            this.TenMonAn = name;
            this.Kcal = calo;
            this.NguyenLieu = nguyenlieu;
        }
        public ThucAn(ThucAn ta)
        {
            this.TenMonAn = ta.TenMonAn;
            this.Kcal = ta.Kcal;
        }

        public static ThucAn operator +(ThucAn a, ThucAn b)
        {
            string name = a.TenMonAn + "_" + b.TenMonAn;
            int calo = (a.Kcal + b.Kcal) / 2;
            MonAn ketqua = new ThucAn(name, calo, NGUYENLIEU.MONMAN);
            return (ThucAn)ketqua;
        }

        protected override string B2()
        {
            return "B2: Chien , xao";
        }

        public delegate string delegateChonKhauVi(params object[] thamso);
        public event delegateChonKhauVi eventChonKhauVi;

        private string B1_ChonTheoKhauVi(params object[] thamso)
        {
            return "Chon ten mon an can order";
        }

        private string B2_ChonTheoKhauVi(params object[] thamso)
        {
            return eventChonKhauVi?.Invoke(thamso);
        }
        public string luaChonTheoKhauVi(params object[] thamso)
        {
            // B1 chon thuc an
            string kqB1 = this.B1_ChonTheoKhauVi(thamso);
            // B2 chon khau vi
            string kqB2 = this.B2_ChonTheoKhauVi(kqB1);
            // B3 tra ve ket qua B2
            return kqB2;
        }
    }
}
