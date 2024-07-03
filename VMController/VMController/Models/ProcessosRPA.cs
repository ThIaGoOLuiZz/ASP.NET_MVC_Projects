using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMController.Models
{
    public class ProcessosRPA
    {
        [Key]
        public int IdProcesso { get; set; }

        [Required]
        public string NomeProcesso { get; set; }

        [Required]
        public string StatusProcesso { get; set; }

        [ForeignKey("VirtualMachine")]
        public int IdVM { get; set; }

        public VirtualMachine VirtualMachine { get; set; }
    }
}