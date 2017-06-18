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
    public class RoleRepository : IRepository<DalRole>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }
        public DalRole GetById(int key)
        {
            return context.Set<Role>().FirstOrDefault(Role => Role.Id == key).ToDalRole();
        }
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().ToList().Select(Role => Role.ToDalRole());
        }
        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<Role>().Where(express).ToList();
            return result.Select(Role => Role.ToDalRole());
        }

        public void Create(DalRole e)
        {
            var role = e.ToOrmRole();
            context.Set<Role>().Add(role);
        }

        public void Delete(DalRole e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
