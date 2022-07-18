using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie Add(Pie newPie)
        {
            _appDbContext.Add(newPie);
            return newPie;
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.Include(c=>c.Category).Where(p=>p.PieId==pieId).FirstOrDefault();
        }

        public IEnumerable<Pie> GetPiesByName(string name)
        {
            var query = from r in _appDbContext.Pies.Include(c => c.Category)
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
                        
            return query;
        }

        public Pie Remove(int PieId)
        {
            var pie = GetPieById(PieId);
            if (pie != null) 
                _appDbContext.Pies.Remove(pie);
            return pie;
        }

        public Pie Update(Pie updatedPie)
        {
            var entity = _appDbContext.Pies.Attach(updatedPie);
            entity.State = EntityState.Modified;
            return updatedPie;
        }
    }
}
