using QLRapChieuPhim.Models;
using QLRapChieuPhim.Reponsitory;
using Microsoft.AspNetCore.Mvc;

namespace QLRapChieuPhim.ViewComponents
{
	public class VTCTMenuViewComponent : ViewComponent
	{
		private readonly IViTriReponsiory _vtCT;

		public VTCTMenuViewComponent(IViTriReponsiory vtCT)
		{
			_vtCT = vtCT;
		}
		public IViewComponentResult Invoke()
		{
			var giochieu = _vtCT.GetAllGioChieu().OrderBy(x => x.GioChieu1);
            return View(giochieu);
        }
	}
}
