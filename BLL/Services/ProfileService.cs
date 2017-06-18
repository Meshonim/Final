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
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalProfile> profileRepository;
        public ProfileService(IUnitOfWork uow, IRepository<DalProfile> repository)
        {
            this.uow = uow;
            this.profileRepository = repository;
        }

        public ProfileEntity GetById(int id)
        {
            return profileRepository.GetById(id).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAll()
        {
            return profileRepository.GetAll().Select(profile => profile.ToBllProfile());
        }
        public ProfileEntity GetOneByPredicate(Expression<Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return profileRepository.GetOneByPredicate(newExpression).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllByPredicate(Expression<Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var newExpression = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.ReplacementParameter);
            return profileRepository.GetAllByPredicate(newExpression).Select(profile => profile.ToBllProfile()).ToList();
        }

        public void Update(ProfileEntity profile)
        {
            profileRepository.Update(profile.ToDalProfile());
            uow.Commit();
        }

        public void Delete(ProfileEntity profile)
        {
            profileRepository.Delete(profile.ToDalProfile());
            uow.Commit();
        }
    }
}
