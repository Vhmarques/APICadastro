using APICatalogo.Context;
using System;

namespace APICatalogo.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private ProdutoRepository _produtoRepository;

        private CategoriaRepository _categoriaRepository;

        public CatalogoDbContext _context;
        public UnityOfWork(CatalogoDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }        

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
         _context.Dispose();
        }


    }
}