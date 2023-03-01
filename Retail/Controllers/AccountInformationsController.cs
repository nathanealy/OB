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
    public class AccountInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountInformations
        public async Task<IActionResult> Index()
        {
              return _context.AccountInformation != null ? 
                          View(await _context.AccountInformation.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AccountInformation'  is null.");
        }

        // GET: AccountInformations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AccountInformation == null)
            {
                return NotFound();
            }

            var accountInformation = await _context.AccountInformation
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (accountInformation == null)
            {
                return NotFound();
            }

            return View(accountInformation);
        }

        // GET: AccountInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,AccountBalance,AvailableBalance,HoldBalance,HoldsTotal,Description,Nickname")] AccountInformation accountInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountInformation);
        }

        // GET: AccountInformations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AccountInformation == null)
            {
                return NotFound();
            }

            var accountInformation = await _context.AccountInformation.FindAsync(id);
            if (accountInformation == null)
            {
                return NotFound();
            }
            return View(accountInformation);
        }

        // POST: AccountInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AccountNumber,AccountBalance,AvailableBalance,HoldBalance,HoldsTotal,Description,Nickname")] AccountInformation accountInformation)
        {
            if (id != accountInformation.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountInformationExists(accountInformation.AccountNumber))
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
            return View(accountInformation);
        }

        // GET: AccountInformations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AccountInformation == null)
            {
                return NotFound();
            }

            var accountInformation = await _context.AccountInformation
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (accountInformation == null)
            {
                return NotFound();
            }

            return View(accountInformation);
        }

        // POST: AccountInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AccountInformation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AccountInformation'  is null.");
            }
            var accountInformation = await _context.AccountInformation.FindAsync(id);
            if (accountInformation != null)
            {
                _context.AccountInformation.Remove(accountInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountInformationExists(string id)
        {
          return (_context.AccountInformation?.Any(e => e.AccountNumber == id)).GetValueOrDefault();
        }
    }
}
