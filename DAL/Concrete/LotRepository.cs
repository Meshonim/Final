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
using System.Diagnostics;


namespace DAL.Concrete
{
    public class LotRepository : IRepository<DalLot>
    {
        private readonly DbContext context;

        public LotRepository(DbContext context)
        {
            this.context = context;
        }
        public DalLot GetById(int key)
        {
            return context.Set<Lot>().FirstOrDefault(lot => lot.Id == key).ToDalLot();
        }
        public IEnumerable<DalLot> GetAll()
        {
            return context.Set<Lot>().ToList().Select(lot => lot.ToDalLot());
        }
        public DalLot GetOneByPredicate(Expression<Func<DalLot, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalLot> GetAllByPredicate(Expression<Func<DalLot, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalLot, Lot>(Expression.Parameter(typeof(Lot), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Lot, bool>>(visitor.Visit(predicate.Body), visitor.ReplacementParameter);
            var result = context.Set<Lot>().Where(express).ToList();
            return result.Select(lot => lot.ToDalLot());
        }

        public void Create(DalLot e)
        {
            var lot = e.ToOrmLot();
            var tagList = lot.Tags.Select(n => n).ToList();
            lot.Tags.Clear();
            foreach (Tag tag in tagList)
            {
                var tg = context.Set<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                if (tg == null)
                {
                    tg = new Tag { Name = tag.Name };
                }
                lot.Tags.Add(tg);
            }
            context.Set<Lot>().Add(lot);
        }

        public void Delete(DalLot e)
        {          
            var lot = context.Set<Lot>().Single(u => u.Id == e.Id);
            context.Set<Lot>().Remove(lot);
        }

        public void Update(DalLot e)
        {
            var lot = e.ToOrmLot();
            var tagList = lot.Tags.Select(n => n).ToList();
            lot.Tags.Clear();
            foreach (Tag tag in tagList)
            {
                var tg = context.Set<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                if (tg == null)
                {
                    tg = new Tag { Name = tag.Name };
                }
                lot.Tags.Add(tg);
            }
            context.Set<Lot>().AddOrUpdate(lot);
            context.SaveChanges();
        }
    }
}
