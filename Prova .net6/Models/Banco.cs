using System.ComponentModel.DataAnnotations;

namespace Prova_.net6.Models
{
    public class Banco
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public string Codigo { get; set; }

        public decimal PercentualJuros { get; set; }
    }
}
