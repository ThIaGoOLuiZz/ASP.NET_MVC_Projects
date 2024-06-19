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
    public class UsuariosController : Controller
    {
        private readonly ContextoUsuario _context;

        public UsuariosController(ContextoUsuario context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return _context.Usuarios != null ?
                        View(await _context.Usuarios.ToListAsync()) :
                        Problem("Entity set 'ContextoUsuario.Usuarios'  is null.");
        }

        // GET: Usuarios/Register
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Register
        [HttpPost]
        public async Task<IActionResult> Register([Bind("IdUsuario,Nome,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o email já está cadastrado
                var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email já cadastrado.");
                    return View(usuario);
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Login", "Usuarios");
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Senha")] Usuario model)
        {
            ModelState.Remove("Nome");

            if (ModelState.IsValid)
            {
                var user = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Senha == model.Senha);

                if (user != null)
                {
                    Console.WriteLine("Usuário encontrado: " + user.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                }
            }
            else
            {
                Console.WriteLine("ModelState é inválido");
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            return View(model);
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
