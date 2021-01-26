using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public class ThucUong : MonAn
    {
        public ThucUong() { }
        public ThucUong(string name, int calo, NGUYENLIEU nguyenlieu)
        {
            this.TenMonAn = name;
            this.Kcal = calo;
            this.NguyenLieu = nguyenlieu;
        }
        public ThucUong(ThucUong tu)
        {
            this.TenMonAn = tu.TenMonAn;
            this.Kcal = tu.Kcal;
        }
        public static ThucUong operator +(ThucUong a, ThucUong b)
        {
            string name = a.TenMonAn + "_" + b.TenMonAn;
            int calo = (a.Kcal + b.Kcal) / 2;
            MonAn ketqua = new ThucUong(name, calo, NGUYENLIEU.MONCHAY);
            return (ThucUong)ketqua;
        }
        protected override string B2()
        {
            return "B2: vat hoac ep";
        }

        public delegate string delegateChonViNuocUong(params object[] thamso);
        public event delegateChonViNuocUong eventChonViNuocUong;

        private string B1_ChonViNuocUong(params object[] thamso)
        {
            return "Chon nuoc uong can order";
        }
        private string B2_ChonViNuocUong(params object[] thamso)
        {
            return eventChonViNuocUong?.Invoke(thamso);
        }
        public string chonViNuocUong(params object[] thamso)
        {
            // B1 Chon nuoc uong
            string kqB1 = this.B1_ChonViNuocUong(thamso);
            // B2 Chon vi can che bien
            string kqB2 = this.B2_ChonViNuocUong(kqB1);
            // B3 tra ve kqB2
            return kqB2;
        }
    }
}
