using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface ITagService
    {
        TagEntity GetById(int id);
        IEnumerable<TagEntity> GetAll();
        TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> predicates);
        IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> predicates);
        void Create(TagEntity tag);
        void Delete(TagEntity tag);
    }
}
