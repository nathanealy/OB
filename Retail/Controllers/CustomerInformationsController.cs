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
    public class CustomerInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerInformations
        public async Task<IActionResult> Index()
        {
              return _context.CustomerInformation != null ? 
                          View(await _context.CustomerInformation.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerInformation'  is null.");
        }

        // GET: CustomerInformations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CustomerInformation == null)
            {
                return NotFound();
            }

            var customerInformation = await _context.CustomerInformation
                .FirstOrDefaultAsync(m => m.id == id);
            if (customerInformation == null)
            {
                return NotFound();
            }

            return View(customerInformation);
        }

        // GET: CustomerInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Address,City,State,Zip")] CustomerInformation customerInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerInformation);
        }

        // GET: CustomerInformations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CustomerInformation == null)
            {
                return NotFound();
            }

            var customerInformation = await _context.CustomerInformation.FindAsync(id);
            if (customerInformation == null)
            {
                return NotFound();
            }
            return View(customerInformation);
        }

        // POST: CustomerInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,Name,Address,City,State,Zip")] CustomerInformation customerInformation)
        {
            if (id != customerInformation.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInformationExists(customerInformation.id))
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
            return View(customerInformation);
        }

        // GET: CustomerInformations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CustomerInformation == null)
            {
                return NotFound();
            }

            var customerInformation = await _context.CustomerInformation
                .FirstOrDefaultAsync(m => m.id == id);
            if (customerInformation == null)
            {
                return NotFound();
            }

            return View(customerInformation);
        }

        // POST: CustomerInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CustomerInformation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerInformation'  is null.");
            }
            var customerInformation = await _context.CustomerInformation.FindAsync(id);
            if (customerInformation != null)
            {
                _context.CustomerInformation.Remove(customerInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInformationExists(string id)
        {
          return (_context.CustomerInformation?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
