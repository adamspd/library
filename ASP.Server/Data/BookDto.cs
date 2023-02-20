using System;
using System.Collections.Generic;

namespace ASP.Server.Data
{
	public class BookDto
	{
        public string Title { get; set; }
        public AuthorDto Author { get; set; }
        public string Content { get; set; }
        public double? Price { get; set; }
        public List<int> GenreIds { get; set; }
    }
}

