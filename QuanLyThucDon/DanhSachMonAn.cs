using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    public class DanhSachMonAn
    {
        public List<MonAn> dsMonAn;

        public DanhSachMonAn()
        {
            this.dsMonAn = new List<MonAn>();
        }
        public DanhSachMonAn(DanhSachMonAn dsMa)
        {
            this.dsMonAn = new List<MonAn>();

            foreach (MonAn temp in dsMa.dsMonAn)
            {
                this.dsMonAn.Add(temp);
            }
        }

        public delegate string delegateXoaMonAn(params object[] thamso);
        public event delegateXoaMonAn eventXoaMonAn;
        public string quyetDinhXoa(params object[] thamso)
        {
            return eventXoaMonAn?.Invoke(thamso);
        }
        public string xoaMonAn(MonAn ma)
        {
            // B1 kiem tra mon an ton tai trong danh sach hay khong
            bool kqB1 = this.kiemTraMonAn(ma);
            // B2 neu khong ton tai thi thong bao ra man hinh
            if (!kqB1)
                return String.Format("Mon an {0} khong ton tai trong danh sach", ma.TenMonAn);
            // B3
            string quyetDinh = this.quyetDinhXoa(kqB1);
            if (quyetDinh == "yes")
                this.dsMonAn.Remove(ma);
            // Tra ve ket qua B3
            return quyetDinh + "! xoa mon an " + ma.TenMonAn + " thanh cong";
        }

        public delegate string delegateThemMonAn(params object[] thamso);
        public event delegateThemMonAn eventThemMonAn;
        private string quyetDinhAddMonAn(params object[] thamso)
        {
            return eventThemMonAn?.Invoke(thamso);
        }
        public string themMonAn(MonAn ma)
        {
            // B1 kiem tra mon an co ton tai trong danh sach khong
            if (!this.kiemTraMonAn(ma))
            {
                this.eventThemMonAn += DanhSachMonAn_eventThemMonAn;
                string quyetdinh = this.quyetDinhAddMonAn(ma);
                // B2 neu khong ton tai thi add
                if(quyetdinh == "them")
                {
                    this.dsMonAn.Add(ma);
                    // B3 tra ve ket qua
                    return String.Format("Them mon {0} thanh cong", ma.TenMonAn);
                }
            }
            // B3 tra ve ket qua
            return String.Format("Mon an {0} da ton tai trong thuc don", ma.TenMonAn);
        }
        private bool kiemTraMonAn(MonAn ma)
        {
            if (this.dsMonAn.Contains(ma))
                return true;
            return false;
        }

        private string DanhSachMonAn_eventThemMonAn(params object[] thamso)
        {
            return "them";
        }

        public override string ToString()
        {
            string res = String.Empty;
            int index = 1;
            foreach (MonAn ma in this.dsMonAn)
            {
                res += String.Format("{0}. {1} : {2} Kcal\n", index++, ma.TenMonAn, ma.Kcal);
            }
            return res;
        }
    }
}
