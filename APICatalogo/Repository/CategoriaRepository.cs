﻿using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoDbContext context) : base(context)
        {                
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            return PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.Nome),
                categoriasParameters.PageNumber,
                categoriasParameters.PageSize);
        }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(c => c.Produtos);
        }       
    }
}
