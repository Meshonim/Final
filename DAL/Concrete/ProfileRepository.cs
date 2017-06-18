using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    public class ProfileRepository : IRepository<DalProfile>
    {
        private readonly DbContext context;

        public ProfileRepository(DbContext context)
        {
            this.context = context;
        }
        public DalProfile GetById(int key)
        {
            return context.Set<Profile>().FirstOrDefault(profile => profile.Id == key).ToDalProfile();
        }
        public IEnumerable<DalProfile> GetAll()
        {
            return context.Set<Profile>().ToList().Select(profile => profile.ToDalProfile());
        }
        public DalProfile GetOneByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalProfile> GetAllByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalProfile, Profile>(Expression.Parameter(typeof(Profile), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Profile, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<Profile>().Where(express).ToList();
            return result.Select(profile => profile.ToDalProfile());
        }

        public void Create(DalProfile e)
        {
            var profile = e.ToOrmProfile();
            context.Set<Profile>().Add(profile);
        }

        public void Delete(DalProfile e)
        {
            var profile = context.Set<Profile>().Single(u => u.Id == e.Id);
            context.Set<Profile>().Remove(profile);
        }

        public void Update(DalProfile e)
        {
            var profile = e.ToOrmProfile();
            context.Set<Profile>().AddOrUpdate(profile);
        }

    }
}
