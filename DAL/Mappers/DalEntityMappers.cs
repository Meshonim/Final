using DAL.Interfaces.DTO;
using ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DalEntityMappers
    {

        #region ExceptionObject

        public static ExceptionObject ToOrmExceptionObject(this DalExceptionObject exceptionObject)
        {
            if (exceptionObject == null)
                return null;
            var orm = new ExceptionObject
            {
                Id = exceptionObject.Id,
                Message = exceptionObject.Message,
                Controller = exceptionObject.Controller,
                Action = exceptionObject.Action,
                Date = exceptionObject.Date,
                StackTrace = exceptionObject.StackTrace
            };
            return orm;
        }

        public static DalExceptionObject ToDalExceptionObject(this ExceptionObject orm)
        {
            if (orm == null)
                return null;
            var exceptionObject = new DalExceptionObject
            {
                Id = orm.Id,
                Message = orm.Message,
                Controller = orm.Controller,
                Action = orm.Action,
                Date = orm.Date,
                StackTrace = orm.StackTrace
            };
            return exceptionObject;
        }

        #endregion

        #region Profile
        public static DalProfile ToDalProfile(this Profile profileEntity)
        {
            if (profileEntity == null)
                return null;
            return new DalProfile
            {
                Id = profileEntity.Id,
                Name = profileEntity.Name,
                Bids = profileEntity.Bids.Select(bid => new DalBid
                {
                    Id = bid.Id,
                    Date = bid.Date,
                    LotId = bid.LotId,
                    Price = bid.Price,
                    ProfileId = bid.ProfileId
                }
                ).ToList(),
                Lots = profileEntity.Lots.Select(lot => new DalLot
                {
                    Id = lot.Id,
                    Name = lot.Name,
                    StartDate = lot.StartDate,
                    EndDate = lot.EndDate,
                    IsActive = lot.IsActive,
                    IsChecked = lot.IsChecked,
                    Picture = lot.Picture,
                    ProfileId = lot.ProfileId,
                    Description = lot.Description,
                    Tags = lot.Tags.Select(tag => new DalTag
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    }
                    ).ToList(),
                    Bids = lot.Bids.Select(bid => new DalBid
                    {
                        Id = bid.Id,
                        Date = bid.Date,
                        LotId = bid.LotId,
                        Price = bid.Price,
                        ProfileId = bid.ProfileId
                    }
                    ).ToList(),
                }
                ).ToList()
            };
        }

        public static Profile ToOrmProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                return null;
            return new Profile
            {
                Id = dalProfile.Id,
                Name = dalProfile.Name,
                Bids = dalProfile.Bids.Select(bid => new Bid
                {
                    Id = bid.Id,
                    Date = bid.Date,
                    LotId = bid.LotId,
                    Price = bid.Price,
                    ProfileId = bid.ProfileId
                }
                ).ToList(),
                Lots = dalProfile.Lots.Select(lot => new Lot
                {
                    Id = lot.Id,
                    Name = lot.Name,
                    StartDate = lot.StartDate,
                    EndDate = lot.EndDate,
                    IsActive = lot.IsActive,
                    IsChecked = lot.IsChecked,
                    Picture = lot.Picture,
                    ProfileId = lot.ProfileId,
                    Description = lot.Description,
                    Tags = lot.Tags.Select(tag => new Tag
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    }
                    ).ToList(),
                    Bids = lot.Bids.Select(bid => new Bid
                    {
                        Id = bid.Id,
                        Date = bid.Date,
                        LotId = bid.LotId,
                        Price = bid.Price,
                        ProfileId = bid.ProfileId
                    }
                    ).ToList(),
                }
                ).ToList()
            };
        }
        #endregion

        #region Bid

        public static DalBid ToDalBid(this Bid bidEntity)
        {
            if (bidEntity == null)
                return null;
            return new DalBid
            {
                Id = bidEntity.Id,
                ProfileId = bidEntity.ProfileId,
                Date = bidEntity.Date,
                Price = bidEntity.Price,
                LotId = bidEntity.LotId
            };
        }

        public static Bid ToOrmBid(this DalBid dalBid)
        {
            if (dalBid == null)
                return null;
            return new Bid
            {
                Id = dalBid.Id,
                ProfileId = dalBid.ProfileId,
                Date = dalBid.Date,
                Price = dalBid.Price,
                LotId = dalBid.LotId
            };
        }

        #endregion

        #region User

        public static DalUser ToDalUser(this User userEntity)
        {
            if (userEntity == null)
                return null;
            return new DalUser
            {
                Id = userEntity.Id,
                ProfileId = userEntity.ProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Name = userEntity.Name,
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;
            return new User
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                Password = dalUser.Password,
                Email = dalUser.Email,
                RoleId = dalUser.RoleId,
                Name = dalUser.Name,
            };
        }

        #endregion

        #region Role

        public static Role ToOrmRole(this DalRole role)
        {
            if (role == null)
                return null;
            var orm = new Role
            {
                Id = role.Id,
                Name = role.Name
            };
            return orm;
        }

        public static DalRole ToDalRole(this Role orm)
        {
            if (orm == null)
                return null;
            var role = new DalRole
            {
                Id = orm.Id,
                Name = orm.Name
            };
            return role;
        }

        #endregion

        #region Lot

        public static Lot ToOrmLot(this DalLot lot)
        {
            if (lot == null)
                return null;
            var orm = new Lot
            {
                Id = lot.Id,
                Name = lot.Name,
                StartDate = lot.StartDate,
                EndDate = lot.EndDate,
                IsActive = lot.IsActive,
                IsChecked = lot.IsChecked,
                Picture = lot.Picture,
                ProfileId = lot.ProfileId,
                Description = lot.Description,
                Tags = lot.Tags.Select(tag => new Tag 
                {
                    Id = tag.Id,
                    Name = tag.Name
                }
                ).ToList(),
                Bids = lot.Bids.Select(bid => new Bid 
                {
                    Id = bid.Id,
                    Date = bid.Date,
                    LotId = bid.LotId,
                    Price = bid.Price,
                    ProfileId = bid.ProfileId
                }
                ).ToList(),
            };
            return orm;
        }

        public static DalLot ToDalLot(this Lot orm)
        {
            if (orm == null)
                return null;
            var lot = new DalLot
            {
                Id = orm.Id,
                Name = orm.Name,
                StartDate = orm.StartDate,
                EndDate = orm.EndDate,
                IsActive = orm.IsActive,
                IsChecked = orm.IsChecked,
                Picture = orm.Picture,
                ProfileId = orm.ProfileId,
                Description = orm.Description,
                Tags = orm.Tags.Select(tag => new DalTag
                {
                    Id = tag.Id,
                    Name = tag.Name
                }
                ).ToList(),
                Bids = orm.Bids.Select(bid => new DalBid
                {
                    Id = bid.Id,
                    Date = bid.Date,
                    LotId = bid.LotId,
                    Price = bid.Price,
                    ProfileId = bid.ProfileId
                }
                ).ToList()
            };
            return lot;
        }

        #endregion

        #region Tag

        public static Tag ToOrmTag(this DalTag tag)
        {
            if (tag == null)
                return null;
            var orm = new Tag
            {
                Id = tag.Id,
                Name = tag.Name
            };
            foreach (DalLot lot in tag.Lots)
            {
                orm.Lots.Add(lot.ToOrmLot());
            }
            return orm;
        }

        public static DalTag ToDalTag(this Tag orm)
        {
            if (orm == null)
                return null;
            var tag = new DalTag
            {
                Id = orm.Id,
                Name = orm.Name
            };
            foreach (Lot lot in orm.Lots)
            {
                tag.Lots.Add(lot.ToDalLot());
            }
            return tag;
        }

        #endregion
    }


}
