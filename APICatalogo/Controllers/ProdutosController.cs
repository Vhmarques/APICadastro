using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnityOfWork _context;

        public ProdutosController(IUnityOfWork context)
        {
            _context = context;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _context.ProdutoRepository.GetProdutoPorPreco().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                return _context.ProdutoRepository.Get().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os produtos do banco de dados");
            }
            
        }

        [HttpGet("{id}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _context.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound();
                }
                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter o produto de id= {id} do banco de dados");
            }
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            try
            {

                _context.ProdutoRepository.Add(produto);
                _context.Commit();
                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar uma novo produto");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                _context.ProdutoRepository.Update(produto);
                _context.Commit();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     $"Erro ao tentar atualizar o produto com o id= {id}");
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
                var produto = _context.ProdutoRepository.GetById(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound();
                }
                _context.ProdutoRepository.Delete(produto);
                _context.Commit();
                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Erro ao tentar excluir o produto com o id= {id}");
            }
            
        }
    }
}
