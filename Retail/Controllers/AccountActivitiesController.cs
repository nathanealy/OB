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
    public class AccountActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountActivities
        public async Task<IActionResult> Index()
        {
              return _context.AccountActivity != null ? 
                          View(await _context.AccountActivity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AccountActivity'  is null.");
        }

        // GET: AccountActivities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AccountActivity == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountActivity == null)
            {
                return NotFound();
            }

            return View(accountActivity);
        }

        // GET: AccountActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,TransactionDate,PostDate,TransactionAmount,Account,TransactionBalance,TransactionType")] AccountActivity accountActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountActivity);
        }

        // GET: AccountActivities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.AccountActivity == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity.FindAsync(id);
            if (accountActivity == null)
            {
                return NotFound();
            }
            return View(accountActivity);
        }

        // POST: AccountActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Description,TransactionDate,PostDate,TransactionAmount,Account,TransactionBalance,TransactionType")] AccountActivity accountActivity)
        {
            if (id != accountActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountActivityExists(accountActivity.Id))
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
            return View(accountActivity);
        }

        // GET: AccountActivities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AccountActivity == null)
            {
                return NotFound();
            }

            var accountActivity = await _context.AccountActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountActivity == null)
            {
                return NotFound();
            }

            return View(accountActivity);
        }

        // POST: AccountActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AccountActivity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AccountActivity'  is null.");
            }
            var accountActivity = await _context.AccountActivity.FindAsync(id);
            if (accountActivity != null)
            {
                _context.AccountActivity.Remove(accountActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountActivityExists(long id)
        {
          return (_context.AccountActivity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
