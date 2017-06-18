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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalTag> tagRepository;
        public TagService(IUnitOfWork uow, IRepository<DalTag> repository)
        {
            this.uow = uow;
            this.tagRepository = repository;
        }

        public TagEntity GetById(int id)
        {
            return tagRepository.GetById(id).ToBllTag();
        }

        public IEnumerable<TagEntity> GetAll()
        {
            return tagRepository.GetAll().Select(tag => tag.ToBllTag());
        }
        public TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return tagRepository.GetOneByPredicate(newExpression).ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return tagRepository.GetAllByPredicate(newExpression).Select(tag => tag.ToBllTag()).ToList();
        }
        public void Create(TagEntity tag)
        {
            tagRepository.Create(tag.ToDalTag());
            uow.Commit();
        }
        public void Delete(TagEntity tag)
        {
            tagRepository.Delete(tag.ToDalTag());
            uow.Commit();
        }
    }
}
