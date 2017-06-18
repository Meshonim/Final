using BLL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IProfileService
    {
        ProfileEntity GetById(int id);
        IEnumerable<ProfileEntity> GetAll();
        ProfileEntity GetOneByPredicate(Expression<Func<ProfileEntity, bool>> predicates);
        IEnumerable<ProfileEntity> GetAllByPredicate(Expression<Func<ProfileEntity, bool>> predicates);
        void Update(ProfileEntity profile);
        void Delete(ProfileEntity profile);
    }
}
