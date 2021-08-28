using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoDbContext context) : base(context)
        {                
        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(c => c.Produtos);
        }       
    }
}
