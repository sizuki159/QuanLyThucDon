using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThucDon
{
    class Program
    {
        static void Main(string[] args)
        {
            // Khoi tao cac mon an cho he thong
            MonAn hutieu = new ThucAn("Hu tieu", 300, NGUYENLIEU.MONMAN);
            MonAn mi = new ThucAn("Mi", 400, NGUYENLIEU.MONCHAY);
            MonAn comtam = new ThucAn("Com tam", 500, NGUYENLIEU.MONMAN);
            MonAn bokho = new ThucAn("Bo kho", 450, NGUYENLIEU.MONMAN);
            MonAn nuoccam = new ThucUong("Nuoc cam", 100, NGUYENLIEU.MONCHAY);
            MonAn nuocmia = new ThucUong("Nuoc mia", 150, NGUYENLIEU.MONCHAY);
            MonAn milo_milk = new ThucUong("Sua milo", 200, NGUYENLIEU.MONCHAY);

            // Mo phong operator
            MonAn hutieu_mi = (ThucAn)hutieu + (ThucAn)mi;
            MonAn nuocmia_cam = (ThucUong)nuoccam + (ThucUong)nuocmia;

            // Chon nguyen lieu dau vao
            comtam.eventNguyenLieu += Comtam_eventNguyenLieu;
            string nguyenLieuComTam = comtam.chonNguyenLieuCheBien(comtam);
            Console.WriteLine(nguyenLieuComTam);
            nuocmia_cam.eventNguyenLieu += Nuocmia_cam_eventNguyenLieu;
            string nguyenLieuNuocCamMia = nuocmia_cam.chonNguyenLieuCheBien(nuocmia_cam);
            Console.WriteLine(nguyenLieuNuocCamMia);

            // Chon khau vi thuc an va thuc uong
            Console.WriteLine("---------------------------------------------------");
            ((ThucAn)(hutieu)).eventChonKhauVi += Program_eventChonKhauVi;
            string khauViHuTieu = ((ThucAn)(hutieu)).luaChonTheoKhauVi();
            Console.WriteLine(khauViHuTieu);
            ((ThucUong)(milo_milk)).eventChonViNuocUong += Program_eventChonViNuocUong;
            string khauViMilo = ((ThucUong)(milo_milk)).chonViNuocUong();
            Console.WriteLine(khauViMilo);

            //Mo phong virtual-override , 3 buoc che bien thuc an
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("- Che bien mon an chinh");
            Console.WriteLine(hutieu.cheBienMonAn());
            Console.WriteLine();
            Console.WriteLine("- Che bien thuc uong");
            Console.WriteLine(nuoccam.cheBienMonAn());
            Console.WriteLine();

            // Su dung bien static de dem mon an
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("He thong dang co {0} mon an (thuc an va thuc uong)", MonAn.Count);

            // Khoi tao cac danh sach mon an List<MonAn>
            DanhSachMonAn ds1 = new DanhSachMonAn();
            ds1.themMonAn(hutieu);
            ds1.themMonAn(mi);
            ds1.themMonAn(nuoccam);

            DanhSachMonAn ds2 = new DanhSachMonAn();
            ds2.themMonAn(comtam);
            ds2.themMonAn(nuocmia);
            ds2.themMonAn(milo_milk);

            DanhSachMonAn ds3 = new DanhSachMonAn();
            ds3.themMonAn(nuocmia);
            ds3.themMonAn(hutieu);
            ds3.themMonAn(nuocmia_cam);

            Console.WriteLine("---------------------------------------------------");

            // Thuc don Dictionary<string date, List<MonAn>>;
            ThucDonHangNgay td = new ThucDonHangNgay();
            td.themThucDon("thu2", ds1);
            td.themThucDon("thu3", ds2);
            td.themThucDon("thu4", ds3);

            // Gan thuc don cho nguoi dung
            Person banA = new Person("nguyenvana" , "a123456", "Nguyen Van A", 19, "09098221129");
            banA.MyMenu = new ThucDonHangNgay(td);

            string xuatThucDon = banA.xuatThucDon();
            Console.WriteLine(xuatThucDon);
            Console.WriteLine("---------------------------------------------------");
            // Them mon an
            string ketQuaThemMonAn = banA.MyMenu.ThucDon["thu2"].themMonAn(bokho);
            Console.WriteLine(ketQuaThemMonAn);
            Console.WriteLine("---------------------------------------------------");
            // Xoa mon an , su dung event delegate de nhan quyet dinh cua nguoi dung
            banA.MyMenu.ThucDon["thu2"].eventXoaMonAn += Program_eventXoaMonAn;
            string xoaMonAn = banA.MyMenu.ThucDon["thu2"].xoaMonAn(hutieu);
            Console.WriteLine(xoaMonAn);

            Console.WriteLine("---------------------------------------------------");
            // Hoan vi thuc don giua 2 ngay , su dung event delegate de nhan vao 2 ngay cua nguoi dung
            banA.MyMenu.eventChonNgayHoanVi += MyMenu_eventChonNgayHoanVi;
            banA.MyMenu.swapThucDon();
            Console.WriteLine("Thuc don sau khi swap la");
            xuatThucDon = banA.xuatThucDon();
            Console.WriteLine(xuatThucDon);

            banA.MyMenu.eventChoose += MyMenu_eventChoose;
            string quyetDinhSuDungThucDon = banA.MyMenu.kiemTraTieuChiKcal("thu2");
            Console.WriteLine(quyetDinhSuDungThucDon);

            // Giao cho cong ty B ve ra thuc don
            banA.MyMenu.eventVeThucDon += MyMenu_eventVeThucDon;
            string veThucDon = banA.MyMenu.veThucDon(banA);
            Console.WriteLine(veThucDon);

            Console.ReadKey();
        }

        private static string Nuocmia_cam_eventNguyenLieu(params object[] thamso)
        {
            // Nguyen lieu cua nuoc mia cam
            return "Mia, cam, duong, chanh muoi";
        }

        private static string Comtam_eventNguyenLieu(params object[] thamso)
        {
            // Nguyen lieu cua com tam
            return "Gao, thit , rau,.....";
        }

        private static string Program_eventChonViNuocUong(params object[] thamso)
        {
            // Khau vi cua sua milo
            return "Sua milo sieu ngot";
        }

        private static string Program_eventChonKhauVi(params object[] thamso)
        {
            // Khau vi cua mon hu tieu
            return "Hu tieu sieu cay";
        }

        private static string MyMenu_eventVeThucDon(params object[] thamso)
        {
            // bang thiet ke va bang ve cua cong ty B
            return "Cong ty B thiet ke va ve thuc don thanh cong";
        }

        private static string MyMenu_eventChoose(bool kqB2)
        {
            // Quyet dinh su dung thuc don cua nguoi dung
            return "Quyet dinh su dung thuc don";
        }

        private static string MyMenu_eventChonNgayHoanVi()
        {
            return "thu2|thu3"; // swap 2 ngay thu2 va thu3
        }

        private static string Program_eventXoaMonAn(params object[] thamso)
        {
            return "yes"; // nguoi dung quyet dinh xoa
        }
    }
}