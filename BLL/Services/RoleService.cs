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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalRole> roleRepository;
        public RoleService(IUnitOfWork uow, IRepository<DalRole> repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        public RoleEntity GetById(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAll()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }
        public RoleEntity GetOneByPredicate(Expression<Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return roleRepository.GetOneByPredicate(newExpression).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllByPredicate(Expression<Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return roleRepository.GetAllByPredicate(newExpression).Select(role => role.ToBllRole()).ToList();
        }
        public void Create(RoleEntity role)
        {
            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }
        public void Delete(RoleEntity role)
        {
            roleRepository.Delete(role.ToDalRole());
            uow.Commit();
        }
    }
}
