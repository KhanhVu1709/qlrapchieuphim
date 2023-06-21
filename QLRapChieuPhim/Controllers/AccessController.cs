using QLRapChieuPhim.Models;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhim.Models.ThongTinTaiKhoanModels;

namespace QLRapChieuPhim.Controllers
{
    public class AccessController : Controller
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessController(QlrapChieuPhimContext db, IHttpContextAccessor httpContextAccessor)
        {
            db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                // nếu chưa đăng nhập thì đưa về trang Login
                return View();
            }
            else
            {
                // đăng nhập thành công chuyển về trang index
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            //if (HttpContext.Session.GetString("username") == null)
            //{
            //    {
            //        var u = db.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
            //        if (u != null && u.LoaiUser == "1")
            //        {
            //            HttpContext.Session.SetString("username", u.Username.ToString());
            //            return RedirectToAction("Index", "admin");
            //        }
            //        if (u != null && u.LoaiUser == "2")
            //        {
            //            HttpContext.Session.SetString("username", u.Username.ToString());
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //}
            TempData["Error"] = "";
            if(HttpContext.Session.GetString("username") == null)
            {
                var u = db.Users.FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password));
                if (u != null && u.LoaiUser == null)
                {
                    HttpContext.Session.SetString("username", u.Username.ToString());
                    //var nv = db.KhachHangs.FirstOrDefault(x => x.Username == u.Username);
                    return RedirectToAction("Index", "Home");
                }
                else if (u != null && u.LoaiUser == "1")
                {
                    HttpContext.Session.SetString("username", u.Username.ToString());
                    HttpContext.Session.SetString("LoaiUser", u.LoaiUser.ToString());
                    //var nv = db.NhanViens.FirstOrDefault(x => x.Username == u.Username);

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["Error"] = "Sai tài khoản hoặc mật khẩu!";
                    return View(user); // Trả về view "Login" với thông báo lỗi
                }
            }
            return View(user);
            //return View();
        }

        //[HttpPost]
        //public IActionResult Login(User user)
        //{
        //    if (HttpContext.Session.GetString("username") == null)
        //    {
        //        var u = db.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
        //        if (u != null)
        //        {
        //            ViewBag.username = user.Username;
        //            HttpContext.Session.SetString("username", user.Username.ToString());
        //            return RedirectToAction("Index", "Admin");
        //        }
        //    }
        //    return View();
        //}

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(ThongTinTaiKhoanModel taikhoan)
        {
			var khachHang = new KhachHang();
            var user = new User();
            var checkUser = db.Users.Where(x => x.Username.Equals(taikhoan.User.Username)).FirstOrDefault();
            if(checkUser == null)
            {
                khachHang.Hoten = taikhoan.KhachHang.Hoten;
                khachHang.Username = taikhoan.User.Username;

                user.Username = taikhoan.User.Username;
                user.Password = taikhoan.User.Password;
                user.LoaiUser = taikhoan.User.LoaiUser;

                db.Users.Add(user);
                db.KhachHangs.Add(khachHang);

                db.SaveChanges();
                return RedirectToAction("Login", "Access");
            }
            return View(taikhoan);
        }
    }
}
