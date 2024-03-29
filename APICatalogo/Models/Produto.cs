﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
