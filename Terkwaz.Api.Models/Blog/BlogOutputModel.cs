using System;

namespace Terkwaz.Api.Models.Blog
{
    public class BlogOutputModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public AuthorOutputModel Author { get; set; }
    }
}
