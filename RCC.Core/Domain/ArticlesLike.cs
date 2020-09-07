using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Domain
{
    public class ArticlesLike
    {
        public int Id { get; set; }
        public uint Likes { get; protected set; }
    }
}
