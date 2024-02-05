using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using USUARIOSWEB.Models;
using X.PagedList;

namespace USUARIOSWEB.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DbUsuariosContext _context;

        public UsuariosController(DbUsuariosContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(int? page)
        {
			if (_context.Usuarios == null)
			{
				return Problem("Entity set 'DbUsuariosContext.Usuarios' is null.");
			}

			// Número de página actual (predeterminado es 1 si no se proporciona)
			int pageNumber = page ?? 1;

			// Número de elementos por página
			int pageSize = 10; // ajusta según tus necesidades

			// Crear una lista paginada
			IPagedList<Usuario> pagedUsuarios = await _context.Usuarios.ToPagedListAsync(pageNumber, pageSize);

			// Retornar la vista con la lista paginada
			return View(pagedUsuarios);

			//return _context.Usuarios != null ? 
   //                       View(await _context.Usuarios.ToListAsync()) :
   //                       Problem("Entity set 'DbUsuariosContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,FechaNacimiento,Sexo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,FechaNacimiento,Sexo")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DbUsuariosContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }

		public IActionResult DownloadExcel()
		{
			var userList = _context.Usuarios.ToList();  // Obtén la lista de usuarios desde tu base de datos

			//ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Usuarios");

				// Encabezados
				worksheet.Cells["A1"].Value = "Nombre";
				worksheet.Cells["B1"].Value = "Fecha de Nacimiento";
				worksheet.Cells["C1"].Value = "Sexo";

				// Datos
				for (var i = 0; i < userList.Count; i++)
				{
					worksheet.Cells[i + 2, 1].Value = userList[i].Nombre;
					worksheet.Cells[i + 2, 2].Value = userList[i].FechaNacimiento;
					worksheet.Cells[i + 2, 3].Value = userList[i].Sexo;
				}

				var stream = new MemoryStream(package.GetAsByteArray());

				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Usuarios.xlsx");
			}
		}
	}
}
