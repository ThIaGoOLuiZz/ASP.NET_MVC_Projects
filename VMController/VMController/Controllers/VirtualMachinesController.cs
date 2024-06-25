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
    public class VirtualMachinesController : Controller
    {
        private readonly ContextoVM _context;

        public VirtualMachinesController(ContextoVM context)
        {
            _context = context;
        }

        // GET: VirtualMachines
        public async Task<IActionResult> Index()
        {
              return _context.VirtualMachines != null ? 
                          View(await _context.VirtualMachines.ToListAsync()) :
                          Problem("Entity set 'ContextoVM.VirtualMachines'  is null.");
        }

        // GET: VirtualMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VirtualMachines == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.VirtualMachines
                .FirstOrDefaultAsync(m => m.idVM == id);
            if (virtualMachine == null)
            {
                return NotFound();
            }

            return View(virtualMachine);
        }

        // GET: VirtualMachines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VirtualMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idVM,HostName,Processador,MemoriaRam,CapacidadeHoras")] VirtualMachine virtualMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virtualMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(virtualMachine);
        }

        // GET: VirtualMachines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VirtualMachines == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.VirtualMachines.FindAsync(id);
            if (virtualMachine == null)
            {
                return NotFound();
            }
            return View(virtualMachine);
        }

        // POST: VirtualMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idVM,HostName,Processador,MemoriaRam,CapacidadeHoras")] VirtualMachine virtualMachine)
        {
            if (id != virtualMachine.idVM)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virtualMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirtualMachineExists(virtualMachine.idVM))
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
            return View(virtualMachine);
        }

        // GET: VirtualMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VirtualMachines == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.VirtualMachines
                .FirstOrDefaultAsync(m => m.idVM == id);
            if (virtualMachine == null)
            {
                return NotFound();
            }

            return View(virtualMachine);
        }

        // POST: VirtualMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VirtualMachines == null)
            {
                return Problem("Entity set 'ContextoVM.VirtualMachines'  is null.");
            }
            var virtualMachine = await _context.VirtualMachines.FindAsync(id);
            if (virtualMachine != null)
            {
                _context.VirtualMachines.Remove(virtualMachine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirtualMachineExists(int id)
        {
          return (_context.VirtualMachines?.Any(e => e.idVM == id)).GetValueOrDefault();
        }
    }
}
