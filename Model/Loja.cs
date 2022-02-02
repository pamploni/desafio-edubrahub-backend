using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApiRest.Model
{
    public class Loja
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Razao { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        public ICollection<Produto> Produtos { get; set; }

        public List<Estoque> Estoques { get; set; }

    }
}
