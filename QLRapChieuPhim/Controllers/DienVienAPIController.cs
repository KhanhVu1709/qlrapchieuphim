using QLRapChieuPhim.Models;
using QLRapChieuPhim.Models.DienVienModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace QLRapChieuPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DienVienAPIController : ControllerBase
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        [HttpGet]
        public IEnumerable<MDienVien> GetAllDienVien()
        {
            var dienVien = (from p in db.DienViens
                            select new MDienVien
                            {
                                MaDv = p.MaDv,
                                TenDv = p.TenDv,
                                MoTa = p.MoTa,
                                NgaySinh = p.NgaySinh,
                                ChieuCao = p.ChieuCao,
                                AnhDaiDien = p.AnhDaiDien,
                                MaNuoc = p.MaNuoc
                            }).ToList();
            return dienVien;
        }

        [HttpGet("{maNuoc}")]
        public IEnumerable<MDienVien> GetDienVienByCountry(string maNuoc)
        {
            var dienVien = (from p in db.DienViens
                            where p.MaNuoc == maNuoc
                            select new MDienVien
                            {
                                MaDv = p.MaDv,
                                TenDv = p.TenDv,
                                MoTa = p.MoTa,
                                NgaySinh = p.NgaySinh,
                                ChieuCao = p.ChieuCao,
                                AnhDaiDien = p.AnhDaiDien,
                                MaNuoc = p.MaNuoc
                            }).ToList();
            return dienVien;
        }
    }
}
