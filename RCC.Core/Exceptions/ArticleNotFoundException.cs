using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Exceptions
{
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException() : base("The article was not found")
        {
        }
    }
}
