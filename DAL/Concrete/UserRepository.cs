using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using DAL.Interfaces.Interface;
using System.Linq.Expressions;
using ORM.Models;
using DAL.Mappers;
using DAL.Interfaces.EXPVisitor;

namespace DAL.Concrete
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }
        public DalUser GetById(int key)
        {
            return context.Set<User>().FirstOrDefault(user => user.Id == key).ToDalUser();
        }
        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().ToList().Select(user => user.ToDalUser());
        }        
        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<User>().Where(express).ToList();
            return result.Select(user => user.ToDalUser());
        }

        public void Create(DalUser e)
        {
            Profile profile = new Profile();
            profile.Name = e.Name;
            context.Set<Profile>().Add(profile);         
            var user = e.ToOrmUser();
            user.ProfileId = profile.Id;
            context.Set<User>().Add(user);  
        }

        public void Delete(DalUser e)
        {
            var user = context.Set<User>().Single(u => u.Id == e.Id);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser e)
        {
            var user = e.ToOrmUser();
            context.Set<User>().AddOrUpdate(user);
        }
    }
}
