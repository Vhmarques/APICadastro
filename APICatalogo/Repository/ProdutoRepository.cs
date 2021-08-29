using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CatalogoDbContext context) : base(context)
        {
        }
        public IEnumerable<Produto> GetProdutoPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId),
                produtosParameters.PageNumber, produtosParameters.PageSize);
        }
    }
}
