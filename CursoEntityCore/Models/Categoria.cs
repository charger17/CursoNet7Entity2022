﻿using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Categoria
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Categoria_Id { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[NULL]")]
        public string Nombre { get; set; } 

        public List<Articulo> Articulos { get; set; }

    }
}
