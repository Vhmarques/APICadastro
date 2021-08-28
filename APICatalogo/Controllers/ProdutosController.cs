using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProdutosController(IUnityOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            var produtos = _context.ProdutoRepository.GetProdutoPorPreco().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            try
            {
                var produtos = _context.ProdutoRepository.Get().ToList();
                var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
                return produtosDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar obter os produtos do banco de dados");
            }
            
        }

        [HttpGet("{id}", Name ="ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            try
            {
                var produto = _context.ProdutoRepository.GetById(p => p.ProdutoId == id);
                
                if (produto == null)
                {
                    return NotFound();
                }
                var produtoDto = _mapper.Map<ProdutoDTO>(produto);
                return produtoDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter o produto de id= {id} do banco de dados");
            }
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoDto);

                _context.ProdutoRepository.Add(produto);
                _context.Commit();

                var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

                return new CreatedAtRouteResult("ObterProduto", new { id = produtoDTO.ProdutoId }, produtoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar uma novo produto");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                if (id != produtoDto.ProdutoId)
                {
                    return BadRequest();
                }

                var produto = _mapper.Map<Produto>(produtoDto);

                _context.ProdutoRepository.Update(produto);
                _context.Commit();
                return Ok($"Produto com o id= {id} foi atualizado com sucesso");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     $"Erro ao tentar atualizar o produto com o id= {id}");
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
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

                var produtoDto = _mapper.Map<ProdutoDTO>(produto);

                return produtoDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Erro ao tentar excluir o produto com o id= {id}");
            }
            
        }
    }
}
