using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMController.Data;
using VMController.Models;

namespace VMController.Controllers
{
    public class ProcessosRPAsController : Controller
    {
        private readonly ProcessosRPAContext _context;

        public ProcessosRPAsController(ProcessosRPAContext context)
        {
            _context = context;
        }

        // GET: ProcessosRPAs
        public async Task<IActionResult> Index()
        {
            var processosRPAContext = _context.ProcessosRPA.Include(p => p.VirtualMachine);
            return View(await processosRPAContext.ToListAsync());
        }

        // GET: ProcessosRPAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProcessosRPA == null)
            {
                return NotFound();
            }

            var processosRPA = await _context.ProcessosRPA
                .Include(p => p.VirtualMachine)
                .FirstOrDefaultAsync(m => m.IdProcesso == id);
            if (processosRPA == null)
            {
                return NotFound();
            }

            return View(processosRPA);
        }

        // GET: ProcessosRPAs/Create
        public IActionResult Create()
        {
            ViewData["IdVM"] = new SelectList(_context.Set<VirtualMachine>(), "idVM", "idVM");
            return View();
        }

        // POST: ProcessosRPAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProcesso,NomeProcesso,StatusProcesso,IdVM")] ProcessosRPA processosRPA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processosRPA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVM"] = new SelectList(_context.Set<VirtualMachine>(), "idVM", "idVM", processosRPA.IdVM);
            return View(processosRPA);
        }

        // GET: ProcessosRPAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProcessosRPA == null)
            {
                return NotFound();
            }

            var processosRPA = await _context.ProcessosRPA.FindAsync(id);
            if (processosRPA == null)
            {
                return NotFound();
            }
            ViewData["IdVM"] = new SelectList(_context.Set<VirtualMachine>(), "idVM", "idVM", processosRPA.IdVM);
            return View(processosRPA);
        }

        // POST: ProcessosRPAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProcesso,NomeProcesso,StatusProcesso,IdVM")] ProcessosRPA processosRPA)
        {
            if (id != processosRPA.IdProcesso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processosRPA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessosRPAExists(processosRPA.IdProcesso))
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
            ViewData["IdVM"] = new SelectList(_context.Set<VirtualMachine>(), "idVM", "idVM", processosRPA.IdVM);
            return View(processosRPA);
        }

        // GET: ProcessosRPAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProcessosRPA == null)
            {
                return NotFound();
            }

            var processosRPA = await _context.ProcessosRPA
                .Include(p => p.VirtualMachine)
                .FirstOrDefaultAsync(m => m.IdProcesso == id);
            if (processosRPA == null)
            {
                return NotFound();
            }

            return View(processosRPA);
        }

        // POST: ProcessosRPAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProcessosRPA == null)
            {
                return Problem("Entity set 'ProcessosRPAContext.ProcessosRPA'  is null.");
            }
            var processosRPA = await _context.ProcessosRPA.FindAsync(id);
            if (processosRPA != null)
            {
                _context.ProcessosRPA.Remove(processosRPA);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessosRPAExists(int id)
        {
          return (_context.ProcessosRPA?.Any(e => e.IdProcesso == id)).GetValueOrDefault();
        }
    }
}
