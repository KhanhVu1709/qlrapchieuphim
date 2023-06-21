using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Repository
{
    public class QuocGiaPhimRepository : IQuocGiaPhimRepository
    {
        private readonly QlrapChieuPhimContext _context;

        public QuocGiaPhimRepository(QlrapChieuPhimContext context)
        {
            _context = context;
        }

        public QuocGia Add(QuocGia maNuoc)
        {
            _context.QuocGia.Add(maNuoc);
            _context.SaveChanges();
            return maNuoc;
        }

        public QuocGia Delete(QuocGia maNuoc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuocGia> GetAllQuocGia()
        {
            return _context.QuocGia;
        }

        public QuocGia GetQuocGium(string maNuoc)
        {
            return _context.QuocGia.Find(maNuoc);
        }

        public QuocGia Update(QuocGia maNuoc)
        {
            _context.Update(maNuoc);
            _context.SaveChanges();
            return maNuoc;
        }
    }
}
