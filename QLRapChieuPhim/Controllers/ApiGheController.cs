using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Models.ApiModel;

namespace QLRapChieuPhim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGheController : ControllerBase
    {
        QlrapChieuPhimContext db = new QlrapChieuPhimContext();
        [HttpGet("{maGhe}")]
        public IEnumerable<GheModel> GheDaChon(string maGhe)
        {
            var ghe = from p in db.Ghes
                      where p.MaGhe == maGhe
                      select new GheModel { MaGhe = p.MaGhe};
            return ghe;
        }
    }
}
