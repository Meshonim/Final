using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IExceptionObjectService
    {
        ExceptionObjectEntity GetById(int id);
        IEnumerable<ExceptionObjectEntity> GetAll();
        ExceptionObjectEntity GetOneByPredicate(Expression<Func<ExceptionObjectEntity, bool>> predicates);
        IEnumerable<ExceptionObjectEntity> GetAllByPredicate(Expression<Func<ExceptionObjectEntity, bool>> predicates);
        void Create(ExceptionObjectEntity exceptionObject);
    }
}
