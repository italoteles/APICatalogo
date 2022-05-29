using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        
        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _uof.ProdutoRepository.Get().ToList();
            if(produtos is null)
            {
                return NotFound();
            }
            return produtos;

        }
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if(produto == null)
            {
                return NotFound("Produto não encontrado!");
            }
            return produto;
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId==id);
            if(produto == null)
            {
                return NotFound("Produto não localizado!");
            }
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            return Ok(produto);
        }

    }
}
