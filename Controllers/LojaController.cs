
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
    public class LojaController : ControllerBase
    {
        private readonly ApiContext _context;

        public LojaController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Loja> GetStores()
        {
            return _context.Lojas.ToList();
        }

        [HttpGet("{id}")]
        public Loja GetStoreById(int id)
        {
            return _context.Lojas.SingleOrDefault(prd => prd.Id == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var loj = _context.Lojas.SingleOrDefault(loja => loja.Id == id);
            if (loj == null)
            {
                return NotFound("A Loja nao existe");
            }

            _context.Lojas.Remove(loj);
            _context.SaveChanges();

            return Ok("Loja excluida com sucesso");
        }

        [HttpPost]
        public IActionResult AddStore(Loja loja)
        {
            _context.Lojas.Add(loja);
            _context.SaveChanges();

            return Created("api/loja/" + loja.Id, loja);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Loja loja)
        {
            var lojaCheck = _context.Lojas.SingleOrDefault(loj => loj.Id == id);
            if (loja == null)
            {
                return NotFound("O Loja nao existe");
            }

            lojaCheck.Nome = loja.Nome;
            lojaCheck.Razao = loja.Razao;
           

            _context.Update(lojaCheck);
            _context.SaveChanges();


            return Ok("Loja atualizado com sucesso!");
        }
    }
}
