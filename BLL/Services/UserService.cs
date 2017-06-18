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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;
        private readonly IRepository<DalProfile> profileRepositoty;
        public UserService(IUnitOfWork uow, IRepository<DalUser> userRepository, IRepository<DalProfile> profileRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.profileRepositoty = profileRepository;
        }

        public UserEntity GetById(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }
        
        public IEnumerable<UserEntity> GetAll()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }
        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return userRepository.GetOneByPredicate(newExpression).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return userRepository.GetAllByPredicate(newExpression).Select(user => user.ToBllUser()).ToList();
        }
        public void Create(UserEntity user)
        {          
            userRepository.Create(user.ToDalUser());
            uow.Commit();        
        }
        public void Delete(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Update(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
    }
}
