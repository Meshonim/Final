using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interface;
using System.Linq.Expressions;
using ORM.Models;
using DAL.Mappers;
using DAL.Interfaces.EXPVisitor;

namespace DAL.Concrete
{
    public class ExceptionObjectRepository : IRepository<DalExceptionObject>
    {
        private readonly DbContext context;

        public ExceptionObjectRepository(DbContext context)
        {
            this.context = context;
        }
        public DalExceptionObject GetById(int key)
        {
            return context.Set<ExceptionObject>().FirstOrDefault(exceptionObject => exceptionObject.Id == key).ToDalExceptionObject();
        }
        public IEnumerable<DalExceptionObject> GetAll()
        {
            return context.Set<ExceptionObject>().ToList().Select(exceptionObject => exceptionObject.ToDalExceptionObject());
        }
        public DalExceptionObject GetOneByPredicate(Expression<Func<DalExceptionObject, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalExceptionObject> GetAllByPredicate(Expression<Func<DalExceptionObject, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalExceptionObject, ExceptionObject>(Expression.Parameter(typeof(ExceptionObject), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<ExceptionObject, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<ExceptionObject>().Where(express).ToList();
            return result.Select(exceptionObject => exceptionObject.ToDalExceptionObject());
        }

        public void Create(DalExceptionObject e)
        {
            var exceptionObject = e.ToOrmExceptionObject();
            context.Set<ExceptionObject>().Add(exceptionObject);
        }

        public void Delete(DalExceptionObject e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalExceptionObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
