using QLRapChieuPhim.Models;
using Microsoft.AspNetCore.Mvc;
namespace QLRapChieuPhim.Repository;

public class LoaiPhimMenuViewComponent: ViewComponent
{
    private readonly IDangPhimRepository _loaiSp;
    public LoaiPhimMenuViewComponent(IDangPhimRepository loaiSpRepository)
    {
        _loaiSp = loaiSpRepository;
    }

    public IViewComponentResult Invoke()
    {
        var loaiSp = _loaiSp.GetAllLoaiPhim().OrderBy(x => x.LoaiPhim1);
        return View(loaiSp);
    }
}
