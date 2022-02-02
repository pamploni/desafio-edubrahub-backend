
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
    public class EstoqueController : ControllerBase
    {
        private readonly ApiContext _context;

        public EstoqueController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Estoque> GetStock()
        {
            return _context.Estoques.ToList();
        }

        [HttpGet("{id}")]
        public Estoque GetStockById(int id)
        {
            return _context.Estoques.SingleOrDefault(est => est.Id == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estoqueCheck = _context.Estoques.SingleOrDefault(est => est.Id == id);
            if (estoqueCheck == null)
            {
                return NotFound("Estoque nao existe");
            }

            _context.Estoques.Remove(estoqueCheck);
            _context.SaveChanges();

            return Ok("Estoque excluido com sucesso");
        }

        [HttpPost("addStock")]
        public IActionResult AddStock(Estoque estoque)
        {
            //verificar se existe o item no estoque da loja
            var estoqueCheck = _context.Estoques.SingleOrDefault(est => (est.loja_id == estoque.loja_id) && (est.produto_id == estoque.produto_id));

            if (estoqueCheck == null)
            {
                return NotFound("O Estoque nao existe");
            }

            estoqueCheck.quantidade += estoque.quantidade;
            _context.Update(estoqueCheck);
            _context.SaveChanges();

            return Created("api/estoque/" + estoque.Id, estoque);
        }

        [HttpPost("removeStock")]
        public IActionResult RemoveStock(Estoque estoque)
        {
            //verificar se existe o item no estoque da loja
            var estoqueCheck = _context.Estoques.SingleOrDefault(est => est.Id == estoque.Id && est.loja_id == estoque.loja_id);

            if (estoqueCheck == null)
            {
                return NotFound("O Estoque nao existe");
            }

            estoqueCheck.quantidade = estoqueCheck.quantidade - estoque.quantidade;
            _context.Update(estoqueCheck);
            _context.SaveChanges();

            return Created("api/estoque/" + estoque.Id, estoque);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Estoque estoque)
        {
            var estoqueCheck = _context.Estoques.SingleOrDefault(est => est.Id == id);
            if (estoqueCheck == null)
            {
                return NotFound("O Loja nao existe");
            }

            estoqueCheck.produto_id = estoque.produto_id;
            estoqueCheck.loja_id = estoque.loja_id;
            estoqueCheck.quantidade = estoque.quantidade;
            estoqueCheck.DataRegistro = DateTime.Now;
           

            _context.Update(estoqueCheck);
            _context.SaveChanges();


            return Ok("Estoque atualizado com sucesso!");
        }
    }
}
