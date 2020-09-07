using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCC.WebApi.ViewModels
{
    public class LikeViewModel
    {
        public int ArticleId { get; set; }
        public bool Liked { get; set; }
    }
}
