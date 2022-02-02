using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApiRest.Model;

namespace DesafioApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProdutoController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Produto> GetProducts()
        {
            return _context.Produtos.ToList();
        }

        [HttpGet("{id}")]
        public Produto GetProductById(int id)
        {
            return _context.Produtos.SingleOrDefault(prd => prd.Id == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prod = _context.Produtos.SingleOrDefault(prd => prd.Id == id);
            if (prod == null)
            {
                return NotFound("O Produto n~ao existe");
            }

            _context.Produtos.Remove(prod);
            _context.SaveChanges();

            return Ok("Produto excluido com sucesso");
        }

        [HttpPost]
        public IActionResult AddProduct(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return Created("api/produto/" + produto.Id, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Produto produto)
        {
            var prod = _context.Produtos.SingleOrDefault(prd => prd.Id == id);
            if (prod == null)
            {
                return NotFound("O Produto nao existe");
            }

            prod.Nome = produto.Nome;
            prod.Codigo_barras = produto.Codigo_barras;
            prod.Preco = produto.Preco;
           

            _context.Update(prod);
            _context.SaveChanges();


            return Ok("Produto atualizado com sucesso!");
        }
    }
}
