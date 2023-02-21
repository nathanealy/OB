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
    public class CustomerRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerRecords
        public async Task<IActionResult> Index()
        {
              return _context.CustomerRecord != null ? 
                          View(await _context.CustomerRecord.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerRecord'  is null.");
        }

        // GET: CustomerRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerRecord == null)
            {
                return NotFound();
            }

            var customerRecord = await _context.CustomerRecord
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerRecord == null)
            {
                return NotFound();
            }

            return View(customerRecord);
        }

        // GET: CustomerRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,BirthDate,Address,City,State,ZipCode")] CustomerRecord customerRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerRecord);
        }

        // GET: CustomerRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerRecord == null)
            {
                return NotFound();
            }

            var customerRecord = await _context.CustomerRecord.FindAsync(id);
            if (customerRecord == null)
            {
                return NotFound();
            }
            return View(customerRecord);
        }

        // POST: CustomerRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,BirthDate,Address,City,State,ZipCode")] CustomerRecord customerRecord)
        {
            if (id != customerRecord.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerRecordExists(customerRecord.CustomerId))
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
            return View(customerRecord);
        }

        // GET: CustomerRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerRecord == null)
            {
                return NotFound();
            }

            var customerRecord = await _context.CustomerRecord
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerRecord == null)
            {
                return NotFound();
            }

            return View(customerRecord);
        }

        // POST: CustomerRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerRecord == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerRecord'  is null.");
            }
            var customerRecord = await _context.CustomerRecord.FindAsync(id);
            if (customerRecord != null)
            {
                _context.CustomerRecord.Remove(customerRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerRecordExists(int id)
        {
          return (_context.CustomerRecord?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
