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
    public class BidRepository : IRepository<DalBid>
    {
        private readonly DbContext context;

        public BidRepository(DbContext context)
        {
            this.context = context;
        }
        public DalBid GetById(int key)
        {
            return context.Set<Bid>().FirstOrDefault(bid => bid.Id == key).ToDalBid();
        }
        public IEnumerable<DalBid> GetAll()
        {
            return context.Set<Bid>().ToList().Select(bid => bid.ToDalBid());
        }
        public DalBid GetOneByPredicate(Expression<Func<DalBid, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalBid> GetAllByPredicate(Expression<Func<DalBid, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalBid, Bid>(Expression.Parameter(typeof(Bid), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Bid, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<Bid>().Where(express).ToList();
            return result.Select(bid => bid.ToDalBid());
        }

        public void Create(DalBid e)
        {
            var bid = e.ToOrmBid();
            context.Set<Bid>().Add(bid);
        }

        public void Delete(DalBid e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalBid entity)
        {
            throw new NotImplementedException();
        }
    }
}
