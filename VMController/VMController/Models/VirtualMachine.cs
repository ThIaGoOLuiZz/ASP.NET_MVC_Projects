using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VMController.Models
{
    public class VirtualMachine
    {
        [Key]
        public int idVM { get; set; }

        [Display(Name = "Nome do Host")]
        public string HostName { get; set; }
        public string Processador { get; set; }
        public int MemoriaRam { get; set; }

        [Display(Name = "Capacidade de Horas")]
        public float CapacidadeHoras { get; set; }
    }
}
