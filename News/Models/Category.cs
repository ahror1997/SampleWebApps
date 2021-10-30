using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Post> Posts { get; set; }
    }
}
