using System;
using System.ComponentModel.DataAnnotations;

namespace News.Models
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please, add content")]
        public string Content { get; set; }

        public string Photo { get; set; }

        public int? CategoryId { get; set; }
        public Category MyCategory { get; set; }
    }
}