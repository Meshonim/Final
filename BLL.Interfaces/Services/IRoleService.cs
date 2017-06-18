using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IRoleService
    {
        RoleEntity GetById(int id);
        IEnumerable<RoleEntity> GetAll();
        RoleEntity GetOneByPredicate(Expression<Func<RoleEntity, bool>> predicates);
        IEnumerable<RoleEntity> GetAllByPredicate(Expression<Func<RoleEntity, bool>> predicates);
        void Create(RoleEntity role);
        void Delete(RoleEntity role);
    }
}
