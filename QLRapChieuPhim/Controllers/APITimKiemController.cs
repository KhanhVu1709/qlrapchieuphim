using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Models.ApiModel;

namespace QLRapChieuPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APITimKiemController : ControllerBase
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        [HttpGet("{tenPhim}")]
        public IEnumerable<PhimCanTim> TimKiemTheoTenPhim(string tenPhim)
        {
            var name = from p in db.Phims
                       where p.TenPhim.Contains(tenPhim)
                       select new PhimCanTim
                       {
                           MaPhim = p.MaPhim,
                           TenPhim = p.TenPhim,
                           MaLp = p.MaLp,
                           MaDp = p.MaDp,
                           AnhDaiDien = p.AnhDaiDien
                       };
            return name;
        }
    }
}
