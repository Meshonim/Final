using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IBidService
    {
        BidEntity GetById(int id);
        IEnumerable<BidEntity> GetAll();
        BidEntity GetOneByPredicate(Expression<Func<BidEntity, bool>> predicates);
        IEnumerable<BidEntity> GetAllByPredicate(Expression<Func<BidEntity, bool>> predicates);
        void Create(BidEntity bid);
        void Delete(BidEntity bid);
    }
}
