using QLRapChieuPhim.Models;
using Microsoft.AspNetCore.Mvc;

namespace QLRapChieuPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
    public class ApiPhimController : Controller
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        [HttpGet]
        public IEnumerable<Phim> GetAllPhim()
        {
            var phim = (from p in db.Phims
                          select new Phim
                          {
                              MaPhim = p.MaPhim,
                              MaLp = p.MaLp,
                              MaDp = p.MaDp,
                              TenPhim = p.TenPhim,
                              Nsx = p.Nsx,
                              AnhDaiDien = p.AnhDaiDien,
                              NoiDung = p.NoiDung,
                              NgayKhoiChieu = p.NgayKhoiChieu,
                              MaNuoc = p.MaNuoc
                          }).ToList();
            return phim;
        }



        //[HttpGet("{malp}")]
        [HttpGet("theloai/{malp}")]
        public IEnumerable<Phim> GetAllPhimByTheLoaiPhim(string malp)
        {
            var phim = (from p in db.Phims
                        where p.MaLp == malp
                        select new Phim
                        {
                            MaPhim = p.MaPhim,
                            MaLp = p.MaLp,
                            MaDp = p.MaDp,
                            TenPhim = p.TenPhim,
                            Nsx = p.Nsx,
                            AnhDaiDien = p.AnhDaiDien,
                            NoiDung = p.NoiDung,
                            NgayKhoiChieu = p.NgayKhoiChieu,
                            MaNuoc = p.MaNuoc
                        }).ToList();
            return phim;
        }

        //[HttpGet("{manuoc}")]
        [HttpGet("quocgia/{manuoc}")]
        public IEnumerable<Phim> GetAllPhimByQuocGiaPhim(string manuoc)
        {
            var phim = (from p in db.Phims
                        where p.MaNuoc == manuoc
                        select new Phim
                        {
                            MaPhim = p.MaPhim,
                            MaLp = p.MaLp,
                            MaDp = p.MaDp,
                            TenPhim = p.TenPhim,
                            Nsx = p.Nsx,
                            AnhDaiDien = p.AnhDaiDien,
                            NoiDung = p.NoiDung,
                            NgayKhoiChieu = p.NgayKhoiChieu,
                            MaNuoc = p.MaNuoc
                        }).ToList();
            return phim;
        }
    }
}
