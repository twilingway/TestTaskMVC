using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Data;
using TestTaskMVC.Models;

namespace TestTaskMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly TestTaskMVCContext _context;

        public LogController(TestTaskMVCContext context)
        {
            _context = context;
        }

        // GET: Log
        public async Task<IActionResult> Index()
        {
            return View(await _context.Log.ToListAsync());
        }

        // GET: Log/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.Log
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // GET: Log/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationTime,Message")] LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logEntry);
        }

        // GET: Log/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.Log.FindAsync(id);
            if (logEntry == null)
            {
                return NotFound();
            }
            return View(logEntry);
        }

        // POST: Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationTime,Message")] LogEntry logEntry)
        {
            if (id != logEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogEntryExists(logEntry.Id))
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
            return View(logEntry);
        }

        // GET: Log/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logEntry = await _context.Log
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logEntry = await _context.Log.FindAsync(id);
            _context.Log.Remove(logEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogEntryExists(int id)
        {
            return _context.Log.Any(e => e.Id == id);
        }
    }
}
