using Azure;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Models.Authentication;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace QLRapChieuPhim.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("admin")]
    [Authentication]
    //[AdminAuthorization]
    public class HomeAdminController : Controller
    {
        IWebHostEnvironment webhost;
        public HomeAdminController(IWebHostEnvironment webhost)
        {
            this.webhost = webhost;
        }
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            if (HttpContext.Request.Headers["Referer"].ToString() != "https://localhost:7017/")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //[Route("NamDinh")]
        //public string Uploade(Phim phim)
        //{
        //    string uniqueFileName = null;
        //    if (phim.FrontImage != null)
        //    {
        //        string uploadFolder = Path.Combine(webhost.WebRootPath, "film/images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + phim.FrontImage.FileName;
        //        string filePath = Path.Combine(uploadFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            phim.FrontImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}

        [Route("khachhang")]
        public IActionResult KhachHang(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstKhachHang = db.KhachHangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<KhachHang> lst = new PagedList<KhachHang>(lstKhachHang, pageNumber, pageSize);
            if (!String.IsNullOrEmpty(searchString))
            {

                searchString = searchString.ToLower();
                var khachhang = db.KhachHangs.Where(x => x.Hoten.ToLower().Contains(searchString));
                PagedList<KhachHang> lst1 = new PagedList<KhachHang>(khachhang, pageNumber, pageSize);
                return View(lst1);
            };
            return View(lst);
        }
        // ThemKhachHang
        [Route("ThemKhachHang")]
        [HttpGet]
        public IActionResult ThemKhachHang()
        {
            return View();
        }
        [Route("ThemKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {

                db.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("KhachHang", "Admin");
            }
            return View(khachHang);
        }
        // SuaKhachHang
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(int maKhachHang)
        {
            var khachhang = db.KhachHangs.Find(maKhachHang);
            return View(khachhang);
        }
        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Update(khachHang);
                db.SaveChanges();
                return RedirectToAction("KhachHang", "Admin");
            }
            return View(khachHang);
        }
        // XoaKhachHang
        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(int maKhachHang)
        {
            TempData["Message"] = "";
            var Ve = db.Ves.Where(x => x.MaKh == maKhachHang).ToList();
            if (Ve.Count() > 0)
            {
                TempData["Message"] = "Không xoá được khách hàng này";
                return RedirectToAction("KhachHang", "Admin");
            }
            db.Remove(db.KhachHangs.Find(maKhachHang));
            db.SaveChanges();
            TempData["Message"] = "Khách hàng đã được xoá";
            return RedirectToAction("KhachHang", "Admin");
        }

        [Route("nhanvien")]
        public IActionResult NhanVien(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNhanVien = db.NhanViens.AsNoTracking().OrderBy(x => x.MaNv);
            PagedList<NhanVien> lst = new PagedList<NhanVien>(lstNhanVien, pageNumber, pageSize);
            if (!String.IsNullOrEmpty(searchString))
            {

                searchString = searchString.ToLower();
                var nhanvien = db.NhanViens.Where(x => x.Hoten.ToLower().Contains(searchString));
                PagedList<NhanVien> lst1 = new PagedList<NhanVien>(nhanvien, pageNumber, pageSize);
                return View(lst1);
            };
            return View(lst);
        }

        [Route("ThemNhanVien")]
        [HttpGet]

        public IActionResult ThemNhanVien()
        {
            return View();
        }

        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(sanPham).State = EntityState.Modified;
                db.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("NhanVien", "Admin");
            }
            return View(nhanVien);
        }

        [Route("SuaNhanVien")]
        [HttpGet]

        public IActionResult SuaNhanVien(string maNhanVien)
        {
            var nhanvien = db.NhanViens.Find(maNhanVien);
            return View(nhanvien);
        }

        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Update(nhanVien);
                db.SaveChanges();
                return RedirectToAction("NhanVien", "Admin");
            }
            return View(nhanVien);
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string maNhanVien)
        {
            TempData["Message"] = ""; 
            var Ve = db.Ves.Where(x => x.MaNv == maNhanVien).ToList();
            if(Ve.Count() > 0)
            {
                TempData["Message"] = "Không xoá được nhân viên này";
                return RedirectToAction("NhanVien", "Admin"); 
            }
            db.Remove(db.NhanViens.Find(maNhanVien));
            db.SaveChanges();
            TempData["Message"] = "Nhân viên đã được xoá";
            return RedirectToAction("NhanVien", "Admin");
        }

        [Route("taikhoan")]
        public IActionResult TaiKhoan(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstTK = db.Users.AsNoTracking().OrderBy(x => x.Username);
            PagedList<User> lst = new PagedList<User>(lstTK, pageNumber, pageSize);
            if (!String.IsNullOrEmpty(searchString))
            {

                searchString = searchString.ToLower();
                var taikhoan = db.Users.Where(x => x.Username.ToLower().Contains(searchString));
                PagedList<User> lst1 = new PagedList<User>(taikhoan, pageNumber, pageSize);
                return View(lst1);
            };
            return View(lst);
        }

        [Route("ThemTaiKhoan")]
        [HttpGet]

        public IActionResult ThemTaiKhoan()
        {
            return View();
        }

        [Route("ThemTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(User taikhoan)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(sanPham).State = EntityState.Modified;
                db.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("TaiKhoan", "Admin");
            }
            return View(taikhoan);
        }

        [Route("SuaTaiKhoan")]
        [HttpGet]

        public IActionResult SuaTaiKhoan(string username)
        {
            var taikhoan = db.Users.Find(username);
            return View(taikhoan);
        }

        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(User taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Users.Update(taikhoan);
                db.SaveChanges();
                return RedirectToAction("TaiKhoan", "Admin");
            }
            return View(taikhoan);
        }

        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(string username)
        {
            TempData["Message"] = "";
            var KhachHang = db.KhachHangs.Where(x => x.Username == username).ToList();
            if (KhachHang.Count() > 0)
            {
                TempData["Message"] = "Không xoá được tài khoản này";
                return RedirectToAction("TaiKhoan", "Admin");
            }
            if(KhachHang.Any())
            {
                db.RemoveRange(KhachHang);
            }
            var NhanVien = db.NhanViens.Where(x => x.Username == username).ToList();
            if (NhanVien.Count() > 0)
            {
                TempData["Message"] = "Không xoá được tài khoản này";
                return RedirectToAction("TaiKhoan", "Admin");
            }
            if (NhanVien.Any())
            {
                db.RemoveRange(NhanVien);
            }
            db.Remove(db.Users.Find(username));
            db.SaveChanges();
            TempData["Message"] = "Tài khoản đã được xoá";
            return RedirectToAction("TaiKhoan", "Admin");
        }

        [Route("phim")]
        public IActionResult Phim(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstPhim = db.Phims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<Phim> lst = new PagedList<Phim>(lstPhim, pageNumber, pageSize);
            if (!String.IsNullOrEmpty(searchString))
            {

                searchString = searchString.ToLower();
                var phim = db.Phims.Where(x => x.TenPhim.ToLower().Contains(searchString));
                PagedList<Phim> lst1 = new PagedList<Phim>(phim, pageNumber, pageSize);
                return View(lst1);
            };
            return View(lst);
        }

        [Route("ThemPhim")]
        [HttpGet]
        public IActionResult ThemPhim()
        {
            ViewBag.MaNuoc = new SelectList(db.QuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLp = new SelectList(db.LoaiPhims.ToList(), "MaLp", "LoaiPhim1");
            ViewBag.MaDp = new SelectList(db.DangPhims.ToList(), "MaDp", "DangPhim1");
            return View();
        }

        [Route("ThemPhim")]
        [HttpPost]
        public IActionResult ThemPhim(Phim phim)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(phim).State = EntityState.Modified;
                //string uniqueFileName = Uploade(phim);
                //if (uniqueFileName == "")
                //{
                //    uniqueFileName = "ao_khoac_da_nam.jpg";
                //}
                //phim.AnhDaiDien = uniqueFileName;
                db.Add(phim);
                db.SaveChanges();
                return RedirectToAction("Phim", "Admin");
            }
            ViewBag.MaNuoc = new SelectList(db.QuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLp = new SelectList(db.LoaiPhims.ToList(), "MaLp", "LoaiPhim1");
            ViewBag.MaDp = new SelectList(db.DangPhims.ToList(), "MaDp", "DangPhim1");
            return View(phim);
        }


        [Route("SuaPhim")]
        [HttpGet]

        public IActionResult SuaPhim(string maPhim)
        {
            ViewBag.MaNuoc = new SelectList(db.QuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLp = new SelectList(db.LoaiPhims.ToList(), "MaLp", "LoaiPhim1");
            ViewBag.MaDp = new SelectList(db.DangPhims.ToList(), "MaDp", "DangPhim1");
            var phim = db.Phims.Find(maPhim);
            return View(phim);
        }

        [Route("SuaPhim")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhim(Phim phim)
        {
            if (ModelState.IsValid)
            {
                db.Phims.Update(phim);
                db.SaveChanges();
                return RedirectToAction("Phim", "Admin");
            }
            ViewBag.MaNuoc = new SelectList(db.QuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLp = new SelectList(db.LoaiPhims.ToList(), "MaLp", "LoaiPhim1");
            ViewBag.MaDp = new SelectList(db.DangPhims.ToList(), "MaDp", "DangPhim1");
            return View(phim);
        }

        [Route("XoaPhim")]
        [HttpGet]
        public IActionResult XoaPhim(string maPhim)
        {
            TempData["Message"] = "";
            var chitietchieuphim = db.ChiTietChieuPhims.Where(x => x.MaPhim == maPhim).ToList();
            if (chitietchieuphim.Count() > 0)
            {
                TempData["Message"] = "Không xoá được phim này";
                return RedirectToAction("Phim", "Admin");
            }
            if (chitietchieuphim.Any())
            {
                db.RemoveRange(chitietchieuphim);
            }
            db.Remove(db.Phims.Find(maPhim));
            db.SaveChanges();
            TempData["Message"] = "Phim đã được xoá";
            return RedirectToAction("Phim", "Admin");
        }

        [Route("chitietchieuphim")]
        public IActionResult ChiTietChieuPhim(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstNhanVien = db.ChiTietChieuPhims.AsNoTracking().OrderByDescending(x => x.MaVe);
            PagedList<ChiTietChieuPhim> lst = new PagedList<ChiTietChieuPhim>(lstNhanVien, pageNumber, pageSize);
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    searchString = searchString.ToLower();
            //    var chitietve = db.ChiTietChieuPhims.Where(x => x.MaVe.ToLower().Contains(searchString));
            //    PagedList<ChiTietChieuPhim> lst1 = new PagedList<ChiTietChieuPhim>(chitietve, pageNumber, pageSize);
            //    return View(lst1);
            //};
            return View(lst);
        }

        [Route("ThemChitietchieuphim")]
        [HttpGet]
        public IActionResult ThemChiTietCP()
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            ViewBag.MaPhong = new SelectList(db.PhongChieus.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaPhim = new SelectList(db.Phims.AsNoTracking().Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today), "MaPhim", "TenPhim");
            ViewBag.GioChieu = new SelectList(db.GioChieus.ToList(), "MaGioChieu", "GioChieu1");
            ViewBag.MaLV = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaGhe = new SelectList(db.Ghes.ToList(), "MaGhe", "MaGhe");
            return View();
        }

        [Route("ThemChitietchieuphim")]
        [HttpPost]
        public IActionResult ThemChiTietCP(ChiTietChieuPhim chitiet)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            if (ModelState.IsValid)
            {
                //db.Entry(phim).State = EntityState.Modified;
                db.Add(chitiet);
                db.SaveChanges();
                return RedirectToAction("ChiTietChieuPhim", "Admin");
            }
            ViewBag.MaPhong = new SelectList(db.PhongChieus.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaPhim = new SelectList(db.Phims.AsNoTracking().Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today), "MaPhim", "TenPhim");
            ViewBag.GioChieu = new SelectList(db.GioChieus.ToList(), "MaGioChieu", "GioChieu1");
            ViewBag.MaLV = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaGhe = new SelectList(db.Ghes.ToList(), "MaGhe", "MaGhe");
            return View(chitiet);
        }

        [Route("SuaChiTiet")]
        [HttpGet]
        public IActionResult SuaChiTiet(int maVe)
        {
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            ViewBag.MaPhong = new SelectList(db.PhongChieus.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaPhim = new SelectList(db.Phims.AsNoTracking().Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today), "MaPhim", "TenPhim");
            ViewBag.GioChieu = new SelectList(db.GioChieus.ToList(), "MaGioChieu", "GioChieu1");
            ViewBag.MaLV = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaGhe = new SelectList(db.Ghes.ToList(), "MaGhe", "MaGhe");
            var ve = db.ChiTietChieuPhims.Find(maVe);
            return View(ve);
        }

        [Route("SuaChiTiet")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaChiTiet(ChiTietChieuPhim ve)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietChieuPhims.Update(ve);
                db.SaveChanges();
                return RedirectToAction("ChiTietChieuPhim", "Admin");
            }
            var today = DateTime.Today;
            var lastWeek = today.AddDays(-7);
            ViewBag.MaPhong = new SelectList(db.PhongChieus.ToList(), "MaPhong", "TenPhong");
            ViewBag.MaPhim = new SelectList(db.Phims.AsNoTracking().Where(x => x.NgayKhoiChieu >= lastWeek && x.NgayKhoiChieu <= today), "MaPhim", "TenPhim");
            ViewBag.GioChieu = new SelectList(db.GioChieus.ToList(), "MaGioChieu", "GioChieu1");
            ViewBag.MaLV = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaGhe = new SelectList(db.Ghes.ToList(), "MaGhe", "MaGhe");
            return View(ve);
        }

        [Route("XoaChiTietChieu")]
        [HttpGet]
        public IActionResult XoaChiTietChieu(int maVe)
        {
            TempData["Message"] = "";
            var ve = db.Ves.Where(x => x.MaVe == maVe).ToList();
            if (ve.Count() > 0)
            {
                TempData["Message"] = "Không xoá được vé này";
                return RedirectToAction("ChiTietChieuPhim", "Admin");
            }
            if (ve.Any())
            {
                db.RemoveRange(ve);
            }
            db.Remove(db.ChiTietChieuPhims.Find(maVe));
            db.SaveChanges();
            TempData["Message"] = "Chi tiết vé đã được xoá";
            return RedirectToAction("ChiTietChieuPhim", "Admin");
        }

        [Route("Ve")]
        public IActionResult Ve(int? page, string searchString)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstVe = db.Ves.AsNoTracking().OrderBy(x => x.MaVe);
            PagedList<Ve> lst = new PagedList<Ve>(lstVe, pageNumber, pageSize);
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    searchString = searchString.ToLower();
            //    var ve = db.Ves.Where(x => x.MaVe.ToLower().Contains(searchString));
            //    PagedList<Ve> lst1 = new PagedList<Ve>(ve, pageNumber, pageSize);
            //    return View(lst1);
            //};
            return View(lst);
        }

        [Route("ThemVe")]
        [HttpGet]
        public IActionResult ThemVe(int maVe)
        {
            ViewBag.MaLv = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "Hoten");
            ViewBag.MaKh = new SelectList(db.KhachHangs.ToList(), "MaKh", "Hoten");

            var ve = db.Ves.Find(maVe);
            return View(ve);
        }

        [Route("ThemVe")]
        [HttpPost]
        public IActionResult ThemVe(Ve ve)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(phim).State = EntityState.Modified;
                db.Add(ve);
                db.SaveChanges();
                return RedirectToAction("Ve", "Admin");
            }
            ViewBag.MaLv = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "Hoten");
            ViewBag.MaKh = new SelectList(db.KhachHangs.ToList(), "MaKh", "Hoten");
            return View(ve);
        }

        [Route("SuaVe")]
        [HttpGet]
        public IActionResult SuaVe(int maVe)
        {
            ViewBag.MaLv = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "Hoten");
            ViewBag.MaKh = new SelectList(db.KhachHangs.ToList(), "MaKh", "Hoten");
            var ve = db.Ves.Find(maVe);
            return View(ve);
        }

        [Route("SuaVe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaVe(Ve ve)
        {
            if (ModelState.IsValid)
            {
                db.Ves.Update(ve);
                db.SaveChanges();
                return RedirectToAction("Ve", "Admin");
            }
            ViewBag.MaLv = new SelectList(db.LoaiVes.ToList(), "MaLv", "TenLv");
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "Hoten");
            ViewBag.MaKh = new SelectList(db.KhachHangs.ToList(), "MaKh", "Hoten");
            return View(ve);
        }

        [Route("XoaVe")]
        [HttpGet]
        public IActionResult XoaVe(int maVe)
        {
            TempData["Message"] = "";
            db.Remove(db.Ves.Find(maVe));
            db.SaveChanges();
            TempData["Message"] = "Vé đã được xoá";
            return RedirectToAction("Ve", "Admin");
        }
    }
}
