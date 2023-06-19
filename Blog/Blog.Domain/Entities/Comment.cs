﻿namespace Blog.Domain.Entities
{
    public class Comment
    {
        public int IdComment { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PageCount { get; set; }
    }
}
