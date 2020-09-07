using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public bool Liked { get; set; }
    }
}
