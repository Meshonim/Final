using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        UserEntity GetById(int id);
        IEnumerable<UserEntity> GetAll();
        UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates);
        IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates);
        void Create(UserEntity user);
        void Update(UserEntity user);
        void Delete(UserEntity user);
    }
}
