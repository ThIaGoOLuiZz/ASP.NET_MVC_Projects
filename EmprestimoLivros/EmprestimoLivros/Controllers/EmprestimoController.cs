using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {

        readonly private ApplicationDbContext _db;

        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<EmprestimoModel> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            EmprestimoModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimoModel emprestimos)
        {
            if(ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Editar(EmprestimoModel emprestimo)
        {
            if(ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }
    }
}
