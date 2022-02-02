using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApiRest.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo_barras { get; set; }
        public decimal Preco { get; set; }

        public List<Estoque> Estoques { get; set; }
        public ICollection<Loja> Lojas { get; set; }

    }
}
