using Azure;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using Microsoft.Identity.Client;
using System.Drawing.Printing;
using QLRapChieuPhim.Models.LienKetModels;
using Microsoft.AspNetCore.Http;

namespace QLRapChieuPhim.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 100;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Phims
                .AsNoTracking()
                .OrderByDescending(x => x.NgayKhoiChieu)
                .ToList();
            PagedList<Phim> lst = new PagedList<Phim>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult GiaoDich()
        {
            var username = HttpContext.Session.GetString("username");
            var khachhangid = db.KhachHangs.FirstOrDefault(x => x.Username == username);
            var giaodich = from b in db.ChiTietChieuPhims
                           join d in db.Phims on b.MaPhim equals d.MaPhim
                           join c in db.Ves on b.MaVe equals c.MaVe
                           where c.MaKh == khachhangid.MaKh
                           select new GiaoDichModel { NgayBanVe = c.NgayBanVe, MaVe = c.MaVe, MaPhim = b.MaPhim, TenPhim = d.TenPhim};
            var danhSachGiaoDich = giaodich.ToList();
            List<GiaoDichModel> list = new List<GiaoDichModel>(danhSachGiaoDich);
            return View(list);
        }

        public IActionResult TheLoaiPhim(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstSP = db.Phims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<Phim> lst = new PagedList<Phim>(lstSP, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult ChiTietPhim(string maPhim)
        {
            var chitietphim = db.Phims.SingleOrDefault(x => x.MaPhim == maPhim);
            if (chitietphim == null)
            {
                return RedirectToAction("Phim");
            }
            else
            {
                return View(chitietphim);
            }
        }

        public IActionResult MuaVes(int? page)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanPham = db.Phims.AsNoTracking().Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today).OrderByDescending(x => x.NgayKhoiChieu);

            PagedList<Phim> lst = new PagedList<Phim>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult PhimTheoGioChieu(int maGio, int? page)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            var kq = from a in db.GioChieus
                     join b in db.PhimGioChieus on a.MaGioChieu equals b.MaGioChieu
                     join c in db.Phims on b.MaPhim equals c.MaPhim
                     where a.MaGioChieu == maGio
                     select c;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 8;
            var listPhim = kq.Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today).OrderByDescending(x => x.NgayKhoiChieu).ToList();
            PagedList<Phim> lst = new PagedList<Phim>(listPhim, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemVe")]
        [HttpGet]
        public IActionResult ThemVe(String maPhim, string gioChieu)
        {
            List<LoaiVe> loaiVe = db.LoaiVes.ToList();
            var name = db.Phims.FirstOrDefault(x => x.MaPhim == maPhim);
            var suatChieu = db.PhimGioChieus.Where(x => x.MaPhim == maPhim).ToList();
            var phimVaGiochieu = new PhimVaGioChieuModel
                                                { TenPhim = name.TenPhim,
                                                  AnhDaiDien = name.AnhDaiDien,
                                                  MaPhim = maPhim,
                                                  SuatChieu = gioChieu,
                                                  lv = loaiVe
                                                };
            //var phim = db.Phims.Find(maPhim);

            return View(phimVaGiochieu);
        }

        public IActionResult ChonGhe(string maPhim, string gioChieu)
        {
            List<Ghe> lstGhe = db.Ghes.ToList();
            var name = db.Phims.FirstOrDefault(x => x.MaPhim == maPhim);
            var suatChieu = db.PhimGioChieus.Where(x => x.MaPhim == maPhim).ToList();
            var phimVaGiochieu = new PhimVaGioChieuModel
                                                        {
                                                            TenPhim = name.TenPhim,
                                                            AnhDaiDien = name.AnhDaiDien,
                                                            MaPhim = maPhim,
                                                            SuatChieu = gioChieu,
                                                            ghe = lstGhe
            };
            return View(phimVaGiochieu);
        }

        public IActionResult ThemChiTietChieuPhim(PhimVaGioChieuModel phimvagc, string selectedSeats, string phimDaChon)
        {
            var chitietchieuphim = new ChiTietChieuPhim();
            chitietchieuphim.MaPhim = phimDaChon;
            chitietchieuphim.MaGhe = selectedSeats;

            db.ChiTietChieuPhims.Add(chitietchieuphim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult _LayoutPhimDangChieu(int ? page)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Phims
                .AsNoTracking()
                .Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today)
                .OrderByDescending(x => x.NgayKhoiChieu)
                .ToList();

            PagedList<Phim> lst = new PagedList<Phim>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult _LayoutPhimSapChieu(int? page)
        {
            var today = DateTime.Today;
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Phims
                .AsNoTracking()
                .Where(x => x.NgayKhoiChieu > today)
                .OrderByDescending(x => x.NgayKhoiChieu)
                .ToList();
            PagedList<Phim> lst = new PagedList<Phim>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult DienVien(int? page)
        {
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 8;
            var listSanPham = db.DienViens.AsNoTracking().OrderBy(x => x.TenDv);
            PagedList<DienVien> lst = new PagedList<DienVien>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult ChiTietDienVien(string maDienVien)
        {
            var dienVien = db.DienViens.SingleOrDefault(x => x.MaDv == maDienVien);
            var anhDienVien = db.AnhDaiDiens.Where(x => x.MaDv == maDienVien).ToList();
            ViewBag.anhDienVien = anhDienVien;
            if (dienVien == null)
            {
                return RedirectToAction("DienVien");
            }
            else
            {
                return View(dienVien);
            }
        }

        public IActionResult DienVienTheoQuocGia(string MaNuoc, int? page)
        {
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            int pageSize = 3;
            var listDienVien = db.DienViens.AsNoTracking().Where(x => x.MaNuoc == MaNuoc).ToList();
            PagedList<DienVien> lst = new PagedList<DienVien>(listDienVien, pageNumber, pageSize);
            ViewBag.MaNuoc = MaNuoc;
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}