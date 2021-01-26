using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public class ThucDonHangNgay
    {
        public Dictionary<string, DanhSachMonAn> ThucDon;
        
        public ThucDonHangNgay()
        {
            this.ThucDon = new Dictionary<string, DanhSachMonAn>();
        }

        public ThucDonHangNgay(ThucDonHangNgay td)
        {
            this.ThucDon = new Dictionary<string, DanhSachMonAn>();
            foreach(KeyValuePair<string, DanhSachMonAn> temp in td.ThucDon)
            {
                this.ThucDon.Add(temp.Key, temp.Value);
            }
        }

        public string themThucDon(string ngay, DanhSachMonAn dsMa)
        {
            if(this.ThucDon.ContainsKey(ngay) && this.ThucDon.ContainsValue(dsMa))
            {
                return "Danh sach mon an da ton tai trong thuc don ngay " + ngay;
            }
            this.ThucDon.Add(ngay, dsMa);
            return "Them danh sach mon an vao thuc don thanh cong !";
        }

        public string xoaThucDon(string ngay)
        {
            if (this.ThucDon.ContainsKey(ngay))
            {
                this.ThucDon.Remove(ngay);
                return "Xoa thanh cong thuc don ngay " + ngay;
            }
            return "ngay thu " + ngay + " khong ton tai trong thuc don";
        }

        public delegate string delegateChonNgayHoanVi();
        public event delegateChonNgayHoanVi eventChonNgayHoanVi;
        private string chonNgayDeHoanVi()
        {
            // tra ve dinh dang co format thu2|ngay3
            return eventChonNgayHoanVi?.Invoke();
        }
        public string swapThucDon()
        {
            // B1: chon 2 ngay can hoan vi cho nhau delegate-event
            string kqB1 = this.chonNgayDeHoanVi();
            string ngay1 = kqB1.Split('|')[0];
            string ngay2 = kqB1.Split('|')[1];
            List<MonAn> temp = new List<MonAn>();
            foreach(MonAn ma in this.ThucDon[ngay1].dsMonAn)
            {
                if (ma is ThucAn)
                    temp.Add(new ThucAn((ThucAn)ma));
                else if (ma is ThucUong)
                    temp.Add(new ThucUong((ThucUong)ma));
            }
            this.ThucDon[ngay1].dsMonAn.Clear();
            foreach(MonAn ma in this.ThucDon[ngay2].dsMonAn)
            {
                if (ma is ThucAn)
                    this.ThucDon[ngay1].dsMonAn.Add(new ThucAn((ThucAn)ma));
                else if(ma is ThucUong)
                    this.ThucDon[ngay1].dsMonAn.Add(new ThucUong((ThucUong)ma));
            }
            this.ThucDon[ngay2].dsMonAn.Clear();
            foreach (MonAn ma in temp)
            {
                if (ma is ThucAn)
                    this.ThucDon[ngay2].dsMonAn.Add(new ThucAn((ThucAn)ma));
                else if (ma is ThucUong)
                    this.ThucDon[ngay2].dsMonAn.Add(new ThucUong((ThucUong)ma));
            }
            // B3 Trả về kết quả
            return "Swap 2 ngay thanh cong";
        }

        public override string ToString()
        {
            string res = String.Empty;
            foreach (KeyValuePair<string, DanhSachMonAn> item in this.ThucDon)
            {
                res += String.Format("Ngay thu {0}, co danh sach mon an la:\n", item.Key);
                res += item.Value.ToString();
            }
            return res;
        }

        private int tinhTongKcal1Ngay(string ngay)
        {
            int calo = 0;
            foreach(MonAn ma in this.ThucDon[ngay].dsMonAn)
            {
                calo += ma.Kcal;
            }
            return calo;
        }
        private bool kiemTraKcalHopLe(int calo)
        {
            if (calo < 1500 && calo > 2500)
                return false;
            return true;
        }
        public string kiemTraTieuChiKcal(string ngay)
        {
            // B1 tinh tong Kcal
            int kqB1 = this.tinhTongKcal1Ngay(ngay);
            // B2 kiem tra hop le
            bool kqB2 = this.kiemTraKcalHopLe(kqB1);
            // B3 quyet dinh su dung thuc don hay khong
            string kqB3 = this.quyetDinhChoose(kqB2);
            // Tra ve kqB3
            return kqB3;
        }
        public delegate string delegateChoose(bool kqB2);
        public event delegateChoose eventChoose;
        private string quyetDinhChoose(bool kqB2)
        {
            return eventChoose?.Invoke(kqB2);
        }

        private string B1TheoCheDo(params object[] thamso)
        {
            return "B1: Lua chon theo che do";
        }

        private string B2TheoCheDo(params object[] thamso)
        {
            return eventChonPhuongPhap?.Invoke(thamso);
        }

        private string B3TheoCheDo(params object[] thamso)
        {
            return "B3: Xu ly yeu cau va sap xep mon an";
        }

        private string B4TheoCheDo(params object[] thamso)
        {
            return "B4: In ra bang thuc don";
        }

        public delegate string delegateChonPhuongPhap(params object[] thamso);
        public event delegateChonPhuongPhap eventChonPhuongPhap;

        public string chonThucDonTheoCheDo(params object[] thamso)
        {
            string kqB1 = this.B1TheoCheDo(thamso);
            this.eventChonPhuongPhap += ThucDonHangNgay_eventChonPhuongPhap;
            string kqB2 = this.B2TheoCheDo(kqB1);
            string kqB3 = this.B3TheoCheDo();
            string kqB4 = this.B4TheoCheDo();
            return kqB4;
        }

        private string ThucDonHangNgay_eventChonPhuongPhap(params object[] thamso)
        {
            return "B2: Chon phuong phap che bien";
        }

        public delegate string delegateVeThucDon(params object[] thamso);
        public event delegateVeThucDon eventVeThucDon;
        public string veThucDon(params object[] thamso)
        {
            return eventVeThucDon?.Invoke(thamso);
        }

    }
}
