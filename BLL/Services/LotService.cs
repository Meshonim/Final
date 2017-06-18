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
    public class LotService : ILotService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalLot> lotRepository;
        public LotService(IUnitOfWork uow, IRepository<DalLot> repository)
        {
            this.uow = uow;
            this.lotRepository = repository;
        }

        public LotEntity GetById(int id)
        {
            return lotRepository.GetById(id).ToBllLot();
        }

        public IEnumerable<LotEntity> GetAll()
        {
            return lotRepository.GetAll().Select(lot => lot.ToBllLot());
        }
        public LotEntity GetOneByPredicate(Expression<Func<LotEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<LotEntity, DalLot>(Expression.Parameter(typeof(DalLot), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalLot, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return lotRepository.GetOneByPredicate(newExpression).ToBllLot();
        }

        public IEnumerable<LotEntity> GetAllByPredicate(Expression<Func<LotEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<LotEntity, DalLot>(Expression.Parameter(typeof(DalLot), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalLot, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return lotRepository.GetAllByPredicate(newExpression).Select(lot => lot.ToBllLot()).ToList();
        }
        public void Create(LotEntity lot)
        {
            lotRepository.Create(lot.ToDalLot());
            uow.Commit();
        }

        public void Update(LotEntity lot)
        {
            lotRepository.Update(lot.ToDalLot());
            uow.Commit();
        }
        public void Delete(LotEntity lot)
        {
            lotRepository.Delete(lot.ToDalLot());
            uow.Commit();
        }
    }
}
