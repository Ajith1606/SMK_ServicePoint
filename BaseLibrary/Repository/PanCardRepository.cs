using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Interface;
using SMK_ServicePoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Repository
{
    public class PanCardRepository : IPancard
    {
        private readonly LoginDbContext _context;
        public PanCardRepository(LoginDbContext context)
        {
                this._context = context;
        }
        public async Task<PanCard> AddAsync(PanCard mod)
        {
            if (mod == null) return null;

            var newpancard = _context.panCards.Add(mod).Entity;
            await _context.SaveChangesAsync();
            return newpancard;
        }

        public async Task<PanCard> DeleteAsync(int id)
        {
            var pancard = await _context.panCards.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (pancard == null) return null;
            _context.Remove(pancard);
            await _context.SaveChangesAsync();

            return pancard;
        }

        public async Task<List<PanCard>> GetAllAsync()
        {
            var pancard = await _context.panCards.ToListAsync();
            return pancard;
        }

        public async Task<PanCard> GetByIdAsync(int id)
        {
            var singlepancard = await _context.panCards.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (singlepancard == null) return null;

            return singlepancard;
        }

        public async Task<PanCard> UpdateAsync(PanCard mod)
        {
            if (mod == null) return null;

            var newpancard = _context.panCards.Update(mod).Entity;
            await _context.SaveChangesAsync();
            return newpancard;
        }
    }
}
