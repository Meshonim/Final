using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interfaces.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{

    
    public static class MvcPLMappers
    {
        #region User

        public static UserViewModel ToUserViewModel(this UserEntity model)
        {
            if (model == null)
                return null;
            return new UserViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email
            };
        }

        public static UserEntity ToUserEntity(this UserViewModel model)
        {
            if (model == null)
                return null;
            return new UserEntity
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email
            };
        } 

        #endregion

        #region Tag
        public static TagViewModel ToTagViewModel(this TagEntity model)
        {
            if (model == null)
                return null;
            return new TagViewModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static TagEntity ToTagEntity(this TagViewModel model)
        {
            if (model == null)
                return null;
            return new TagEntity
            {
                Id = model.Id,
                Name = model.Name
            };
        }
        #endregion

        #region Bid
        public static BidViewModel ToBidViewModel(this BidEntity model)
        {
            if (model == null)
                return null;
            return new BidViewModel
            {
                Id = model.Id,
                Date = model.Date,
                Price = model.Price,
                LotId = model.LotId,
            };
        }

        public static BidEntity ToBidEntity(this BidViewModel model)
        {
            if (model == null)
                return null;
            return new BidEntity
            {
                Id = model.Id,
                Date = model.Date,
                Price = model.Price,
                LotId = model.LotId,
            };
        }

        #endregion

        #region Lot
        public static LotEntity ToLotEntity(this LotViewModel model)
        {
            if (model == null)
                return null;
            return new LotEntity
            {
                Id = model.Id,
                Description = model.Description,
                IsChecked = model.IsChecked,
                IsActive = model.IsActive,
                Name = model.Name,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Picture = model.Picture,
                ProfileId = model.ProfileId,
                Tags = model.Tags.Select(t => t.ToTagEntity()).ToList()
            };
        }

        public static LotViewModel ToLotViewModel(this LotEntity model)
        {
            if (model == null)
                return null;
            var result =  new LotViewModel
            {
                Id = model.Id,
                Description = model.Description,
                IsChecked = model.IsChecked,
                IsActive = model.IsActive,
                Name = model.Name,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Picture = model.Picture,
                ProfileId = model.ProfileId,
                Tags = model.Tags.Select(t => t.ToTagViewModel()).ToList()
            };
            return result;

        }
    #endregion
    }
       

        
}