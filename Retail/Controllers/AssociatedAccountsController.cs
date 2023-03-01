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
    public class AssociatedAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssociatedAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssociatedAccounts
        public async Task<IActionResult> Index()
        {
              return _context.AssociatedAccount != null ? 
                          View(await _context.AssociatedAccount.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AssociatedAccount'  is null.");
        }

        // GET: AssociatedAccounts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AssociatedAccount == null)
            {
                return NotFound();
            }

            var associatedAccount = await _context.AssociatedAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associatedAccount == null)
            {
                return NotFound();
            }

            return View(associatedAccount);
        }

        // GET: AssociatedAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssociatedAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SocialSecurityNumber,AccountNumber")] AssociatedAccount associatedAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(associatedAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(associatedAccount);
        }

        // GET: AssociatedAccounts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.AssociatedAccount == null)
            {
                return NotFound();
            }

            var associatedAccount = await _context.AssociatedAccount.FindAsync(id);
            if (associatedAccount == null)
            {
                return NotFound();
            }
            return View(associatedAccount);
        }

        // POST: AssociatedAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SocialSecurityNumber,AccountNumber")] AssociatedAccount associatedAccount)
        {
            if (id != associatedAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(associatedAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociatedAccountExists(associatedAccount.Id))
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
            return View(associatedAccount);
        }

        // GET: AssociatedAccounts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AssociatedAccount == null)
            {
                return NotFound();
            }

            var associatedAccount = await _context.AssociatedAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associatedAccount == null)
            {
                return NotFound();
            }

            return View(associatedAccount);
        }

        // POST: AssociatedAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AssociatedAccount == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AssociatedAccount'  is null.");
            }
            var associatedAccount = await _context.AssociatedAccount.FindAsync(id);
            if (associatedAccount != null)
            {
                _context.AssociatedAccount.Remove(associatedAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssociatedAccountExists(long id)
        {
          return (_context.AssociatedAccount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
