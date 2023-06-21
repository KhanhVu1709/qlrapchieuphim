using QLRapChieuPhim.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.ViewComponents
{
    public class QuocGiaMenuViewComponent : ViewComponent
    {
        private readonly IQuocGiaRepository _maNuoc;

        public QuocGiaMenuViewComponent(IQuocGiaRepository maNuoc)
        {
            _maNuoc = maNuoc;
        }

        public IViewComponentResult Invoke()
        {
            var manuoc = _maNuoc.GetAllQuocGia().OrderBy(x => x.MaNuoc);
            return View(manuoc);
        }
    }
}
