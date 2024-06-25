using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VMController.Models
{
    public class VirtualMachine
    {
        public int idVM { get; set; }
        public string HostName { get; set; }
        public string Processador { get; set; }
        public int MemoriaRam { get; set; }
        public float CapacidadeHoras { get; set; }
    }
}
