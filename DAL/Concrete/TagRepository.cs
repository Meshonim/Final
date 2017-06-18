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
    public class TagRepository : IRepository<DalTag>
    {
        private readonly DbContext context;

        public TagRepository(DbContext context)
        {
            this.context = context;
        }
        public DalTag GetById(int key)
        {
            return context.Set<Tag>().FirstOrDefault(tag => tag.Id == key).ToDalTag();
        }
        public IEnumerable<DalTag> GetAll()
        {
            return context.Set<Tag>().ToList().Select(tag => tag.ToDalTag());
        }
        public DalTag GetOneByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalTag> GetAllByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTag, Tag>(Expression.Parameter(typeof(Tag), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Tag, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<Tag>().Where(express).ToList();
            return result.Select(tag => tag.ToDalTag());
        }

        public void Create(DalTag e)
        {
            var tag = e.ToOrmTag();
            context.Set<Tag>().Add(tag);
        }

        public void Delete(DalTag e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalTag entity)
        {
            throw new NotImplementedException();
        }
    }
}
