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
    public class BidService : IBidService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalBid> bidRepository;
        public BidService(IUnitOfWork uow, IRepository<DalBid> repository)
        {
            this.uow = uow;
            this.bidRepository = repository;
        }

        public BidEntity GetById(int id)
        {
            return bidRepository.GetById(id).ToBllBid();
        }

        public IEnumerable<BidEntity> GetAll()
        {
            return bidRepository.GetAll().Select(bid => bid.ToBllBid());
        }
        public BidEntity GetOneByPredicate(Expression<Func<BidEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BidEntity, DalBid>(Expression.Parameter(typeof(DalBid), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalBid, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return bidRepository.GetOneByPredicate(newExpression).ToBllBid();
        }

        public IEnumerable<BidEntity> GetAllByPredicate(Expression<Func<BidEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BidEntity, DalBid>(Expression.Parameter(typeof(DalBid), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalBid, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return bidRepository.GetAllByPredicate(newExpression).Select(bid => bid.ToBllBid()).ToList();
        }
        public void Create(BidEntity bid)
        {
            bidRepository.Create(bid.ToDalBid());
            uow.Commit();
        }
        public void Delete(BidEntity bid)
        {
            bidRepository.Delete(bid.ToDalBid());
            uow.Commit();
        }
    }
}
