using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCC.WebApi.ViewModels
{
    public class LikeResponseViewModel
    {
        public int ArticleId { get; set; }
        public uint Likes { get; set; }
    }
}
