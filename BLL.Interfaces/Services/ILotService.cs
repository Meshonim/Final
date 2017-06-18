using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface ILotService
    {
        LotEntity GetById(int id);
        IEnumerable<LotEntity> GetAll();
        LotEntity GetOneByPredicate(Expression<Func<LotEntity, bool>> predicates);
        IEnumerable<LotEntity> GetAllByPredicate(Expression<Func<LotEntity, bool>> predicates);
        void Create(LotEntity lot);
        void Update(LotEntity lot);
        void Delete(LotEntity lot);
    }
}
