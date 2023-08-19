﻿using System.ComponentModel.DataAnnotations;

namespace CursoEntityCore.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[NULL]")]
        public string Nombre { get; set; } 

    }
}
