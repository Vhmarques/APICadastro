﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Models
{
    public class Categoria
    {
        
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
    }
}
