using ClosedXML.Excel;
using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimoModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.id==id);

            if(emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados,"Dados empréstimo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheet.sheet", "Emprestimo.xls");
                }
            }
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados empréstimos";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof (string));
            dataTable.Columns.Add("Data de empréstimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.dataUltimaAtualizacao);
                });
            }

            return dataTable;
        }


        [HttpPost]
        public IActionResult Cadastrar(EmprestimoModel emprestimos)
        {
            if(ModelState.IsValid)
            {
                emprestimos.dataUltimaAtualizacao = DateTime.Now;

                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Editar(EmprestimoModel emprestimo)
        {
            if(ModelState.IsValid)
            {
                var emprestimoDB = _db.Emprestimos.Find(emprestimo.id);

                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                emprestimoDB.Recebedor = emprestimo.Recebedor;
                emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;

                _db.Emprestimos.Update(emprestimoDB);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";

                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimoModel emprestimo)
        {
            if(emprestimo == null)
            {
                return NotFound();
            }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
