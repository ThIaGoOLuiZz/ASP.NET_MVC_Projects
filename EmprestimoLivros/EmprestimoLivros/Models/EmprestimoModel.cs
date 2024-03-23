namespace EmprestimoLivros.Models
{
    public class EmprestimoModel
    {
        public int id { get; set; }
        public string Recebedor { get; set; }
        public string Fornecedor { get; set; }
        public string LivroEmprestado { get; set; }
        public DateTime dataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}
