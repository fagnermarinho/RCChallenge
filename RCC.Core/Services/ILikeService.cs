using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Services
{
    public interface ILikeService
    {
        void Add(int articleId, bool liked);
    }
}
