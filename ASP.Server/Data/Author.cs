﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Server.Model
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
