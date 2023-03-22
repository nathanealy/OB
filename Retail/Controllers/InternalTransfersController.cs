using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Retail.Data;
using Retail.Models;

namespace Retail.Controllers
{
    public class InternalTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InternalTransfersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InternalTransfers
        public async Task<IActionResult> Index()
        {
              return _context.InternalTransfer != null ? 
                          View(await _context.InternalTransfer.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InternalTransfer'  is null.");
        }

        // GET: InternalTransfers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.InternalTransfer == null)
            {
                return NotFound();
            }

            var internalTransfer = await _context.InternalTransfer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internalTransfer == null)
            {
                return NotFound();
            }

            return View(internalTransfer);
        }

        // GET: InternalTransfers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InternalTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromAccount,ToAccount,Amount,SchedulingOption,TransferDate,Frequency,Delivery,EndBy,NumberOfTransfers,Memo")] InternalTransfer internalTransfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(internalTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(internalTransfer);
        }

        // GET: InternalTransfers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.InternalTransfer == null)
            {
                return NotFound();
            }

            var internalTransfer = await _context.InternalTransfer.FindAsync(id);
            if (internalTransfer == null)
            {
                return NotFound();
            }
            return View(internalTransfer);
        }

        // POST: InternalTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FromAccount,ToAccount,Amount,SchedulingOption,TransferDate,Frequency,Delivery,EndBy,NumberOfTransfers,Memo")] InternalTransfer internalTransfer)
        {
            if (id != internalTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internalTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternalTransferExists(internalTransfer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(internalTransfer);
        }

        // GET: InternalTransfers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.InternalTransfer == null)
            {
                return NotFound();
            }

            var internalTransfer = await _context.InternalTransfer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internalTransfer == null)
            {
                return NotFound();
            }

            return View(internalTransfer);
        }

        // POST: InternalTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.InternalTransfer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InternalTransfer'  is null.");
            }
            var internalTransfer = await _context.InternalTransfer.FindAsync(id);
            if (internalTransfer != null)
            {
                _context.InternalTransfer.Remove(internalTransfer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternalTransferExists(long id)
        {
          return (_context.InternalTransfer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
