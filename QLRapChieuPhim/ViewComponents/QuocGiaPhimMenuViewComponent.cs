using QLRapChieuPhim.Models;
using Microsoft.AspNetCore.Mvc;
namespace QLRapChieuPhim.Repository;

public class QuocGiaPhimMenuViewComponent : ViewComponent
{
    private readonly IQuocGiaPhimRepository _quocGiaPhim;
    public QuocGiaPhimMenuViewComponent(IQuocGiaPhimRepository loaiQuocGiaRepository)
    {
        _quocGiaPhim = loaiQuocGiaRepository;
    }

    public IViewComponentResult Invoke()
    {
        var quocGiaPhim = _quocGiaPhim.GetAllQuocGia().OrderBy(x => x.TenNuoc);
        return View(quocGiaPhim);
    }
}
