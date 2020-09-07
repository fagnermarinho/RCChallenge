using RCC.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCC.Core.Repositories
{
    public interface ILikeRepository
    {       
        void Add(int articleId, bool liked);
        ICollection<Like> GetLikes(int maxitems);
        void Delete(ICollection<Like> items);
    }
}
