using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppPaq.Data;
using WebAppPaq.Models.Paq;

namespace WebAppPaq.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Facturas.Include(f => f.Sucursal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Sucursal)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Descripcion");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,NombreClienteEnvia,ApellidoClienteEnvia,TelefonoClienteEnvia,CedulaClienteEnvia,NombreClienteRecibe,ApellidoClienteRecibe,TelefonoClienteRecibe,CedulaClienteRecibe,FechaCreacion,SucursalId,Total")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
          //  ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Id", factura.SucursalId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Descripcion");
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.SingleOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Id", factura.SucursalId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,NombreClienteEnvia,ApellidoClienteEnvia,TelefonoClienteEnvia,CedulaClienteEnvia,NombreClienteRecibe,ApellidoClienteRecibe,TelefonoClienteRecibe,CedulaClienteRecibe,FechaCreacion,SucursalId,Total")] Factura factura)
        {
            if (id != factura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Id", factura.SucursalId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Sucursal)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.SingleOrDefaultAsync(m => m.Id == id);
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
