using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public enum NGUYENLIEU
    {
        MONCHAY,
        MONMAN
    }
    public abstract class MonAn
    {        
        public string TenMonAn { get; set; }
        public int Kcal { get; set; }
        public NGUYENLIEU NguyenLieu { get; set; }

        public static int Count = 0;

        public MonAn()
        {
            System.Threading.Interlocked.Increment(ref Count);
        }
        public MonAn(string name, int calo, NGUYENLIEU nguyenlieu)
        {
            this.TenMonAn = name;
            this.Kcal = calo;
            this.NguyenLieu = nguyenlieu;
            System.Threading.Interlocked.Increment(ref Count);
        }
        public MonAn(MonAn ma)
        {
            this.TenMonAn = ma.TenMonAn;
            this.Kcal = ma.Kcal;
            this.NguyenLieu = ma.NguyenLieu;
            System.Threading.Interlocked.Increment(ref Count);
        }

        protected string B1()
        {
            return "B1: chon nguyen lieu";
        }
        protected virtual string B2()
        {
            return "B2: phuong phap che bien";
        }
        protected string B3()
        {
            return "B3: in ra ten mon an";
        }
        public string cheBienMonAn()
        {
            return this.B1() + "\n" + this.B2() + "\n" + this.B3();
        }

        public delegate string delegateNguyenLieu(params object[] thamso);
        public event delegateNguyenLieu eventNguyenLieu;

        private string B1_ChonNguyenLieu(params object[] thamso)
        {
            return eventNguyenLieu?.Invoke(thamso);
        }
        private string B2_ChonNguyenLieu(params object[] thamso)
        {
            return "Luu thong tin nguyen lieu da chon";
        }
        private string B3_ChonNguyenLieu(params object[] thamso)
        {
            return "Chon dau bep nau mon an";
        }
        public string chonNguyenLieuCheBien(params object[] thamso)
        {
            // B1 chon nguyen lieu
            string kqB1 = this.B1_ChonNguyenLieu(thamso);
            // B2 luu thong tin nguyen lieu da chon
            string kqB2 = this.B2_ChonNguyenLieu(kqB1);
            // B3 chon dau bep nau an
            string kqB3 = this.B3_ChonNguyenLieu(kqB2);
            // B4 tra ve kqB1
            return ((MonAn)(thamso[0])).TenMonAn + " co nguyen lieu: " + kqB1;
        }
    }
}