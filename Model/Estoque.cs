using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApiRest.Model
{
    public class Estoque
    {
        public int Id { get; set; }
        public int loja_id { get; set; }
        public Loja Loja { get; set; }

        public int produto_id { get; set; }
        public Produto Produto { get; set; }
        
        public decimal quantidade { get; set; }
        
        public DateTime DataRegistro { get; set; }
    }
}
