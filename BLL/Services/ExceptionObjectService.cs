using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;
using System.Diagnostics;
using System.Linq.Expressions;
using DAL.Interfaces.EXPVisitor;

namespace BLL
{
    public class ExceptionObjectService : IExceptionObjectService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalExceptionObject> exceptionObjectRepository;
        public ExceptionObjectService(IUnitOfWork uow, IRepository<DalExceptionObject> repository)
        {
            this.uow = uow;
            this.exceptionObjectRepository = repository;
        }

        public ExceptionObjectEntity GetById(int id)
        {
            return exceptionObjectRepository.GetById(id).ToBllExceptionObject();
        }

        public IEnumerable<ExceptionObjectEntity> GetAll()
        {
            return exceptionObjectRepository.GetAll().Select(exceptionObject => exceptionObject.ToBllExceptionObject());
        }
        public ExceptionObjectEntity GetOneByPredicate(Expression<Func<ExceptionObjectEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ExceptionObjectEntity, DalExceptionObject>(Expression.Parameter(typeof(DalExceptionObject), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalExceptionObject, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return exceptionObjectRepository.GetOneByPredicate(newExpression).ToBllExceptionObject();
        }

        public IEnumerable<ExceptionObjectEntity> GetAllByPredicate(Expression<Func<ExceptionObjectEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ExceptionObjectEntity, DalExceptionObject>(Expression.Parameter(typeof(DalExceptionObject), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalExceptionObject, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return exceptionObjectRepository.GetAllByPredicate(newExpression).Select(exceptionObject => exceptionObject.ToBllExceptionObject()).ToList();
        }
        public void Create(ExceptionObjectEntity exceptionObject)
        {
            exceptionObjectRepository.Create(exceptionObject.ToDalExceptionObject());
            uow.Commit();
        }
        public void Delete(ExceptionObjectEntity exceptionObject)
        {
            exceptionObjectRepository.Delete(exceptionObject.ToDalExceptionObject());
            uow.Commit();
        }
    }
}
