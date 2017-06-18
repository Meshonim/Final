using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {

        #region ExceptionObject
        public static DalExceptionObject ToDalExceptionObject(this ExceptionObjectEntity exceptionObjectEntity)
        {
            if (exceptionObjectEntity == null)
                return null;
            return new DalExceptionObject()
            {
                Id = exceptionObjectEntity.Id,
                Message = exceptionObjectEntity.Message,
                Controller = exceptionObjectEntity.Controller,
                Action = exceptionObjectEntity.Action,
                Date = exceptionObjectEntity.Date,
                StackTrace = exceptionObjectEntity.StackTrace
            };
        }

        public static ExceptionObjectEntity ToBllExceptionObject(this DalExceptionObject dalExceptionObject)
        {
            if (dalExceptionObject == null)
                return null;
            return new ExceptionObjectEntity()
            {
                Id = dalExceptionObject.Id,
                Message = dalExceptionObject.Message,
                Controller = dalExceptionObject.Controller,
                Action = dalExceptionObject.Action,
                Date = dalExceptionObject.Date,
                StackTrace = dalExceptionObject.StackTrace
            };
        }

        #endregion

        #region User
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            if (userEntity == null)
                return null;
            return new DalUser()
            {
                Id = userEntity.Id,
                ProfileId = userEntity.ProfileId,
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Account = userEntity.Account,
                RoleId = userEntity.RoleId
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;
            return new UserEntity()
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                Name = dalUser.Name,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Account = dalUser.Account,
                RoleId = dalUser.RoleId
            };
        }

        #endregion

        #region Role

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            if (roleEntity == null)
                return null;
            return new DalRole()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null)
                return null;
            return new RoleEntity()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
        }
        #endregion

        #region Tag

        public static DalTag ToDalTag(this TagEntity tagEntity)
        {
            if (tagEntity == null)
                return null;
            var tag = new DalTag
            {
                Id = tagEntity.Id,
                Name = tagEntity.Name
            };
            foreach (LotEntity lot in tagEntity.Lots)
            {
                tag.Lots.Add(lot.ToDalLot());
            }
            return tag;
        }

        public static TagEntity ToBllTag(this DalTag dalTag)
        {
            if (dalTag == null)
                return null;
            var tag = new TagEntity
            {
                Id = dalTag.Id,
                Name = dalTag.Name
            };
            foreach (DalLot lot in dalTag.Lots)
            {
                tag.Lots.Add(lot.ToBllLot());
            }
            return tag;
        }
        #endregion

        #region Lot

        public static DalLot ToDalLot(this LotEntity lotEntity)
        {
            if (lotEntity == null)
                return null;
            var lot = new DalLot
            {
                Id = lotEntity.Id,
                Name = lotEntity.Name,
                StartDate = lotEntity.StartDate,
                EndDate = lotEntity.EndDate,
                IsActive = lotEntity.IsActive,
                IsChecked = lotEntity.IsChecked,
                Picture = lotEntity.Picture,
                ProfileId = lotEntity.ProfileId,
                Description = lotEntity.Description,
                Tags = lotEntity.Tags.Select(tag => new DalTag
                {
                    Id = tag.Id,
                    Name = tag.Name
                }
                ).ToList(),
                Bids = lotEntity.Bids.Select(bid => new DalBid
                {
                    Id = bid.Id,
                    ProfileId = bid.ProfileId,
                    Date = bid.Date,
                    Price = bid.Price,
                    LotId = bid.LotId
                }
                ).ToList()
            };
            return lot;
        }

        public static LotEntity ToBllLot(this DalLot dalLot)
        {
            if (dalLot == null)
                return null;
            var lot = new LotEntity
            {
                Id = dalLot.Id,
                Name = dalLot.Name,
                StartDate = dalLot.StartDate,
                EndDate = dalLot.EndDate,
                IsActive = dalLot.IsActive,
                IsChecked = dalLot.IsChecked,
                Picture = dalLot.Picture,
                ProfileId = dalLot.ProfileId,
                Description = dalLot.Description,
                Tags = dalLot.Tags.Select(tag => new TagEntity
                {
                    Id = tag.Id,
                    Name = tag.Name
                }
                ).ToList(),
                Bids = dalLot.Bids.Select(bid => new BidEntity
                {
                    Id = bid.Id,
                    ProfileId = bid.ProfileId,
                    Date = bid.Date,
                    Price = bid.Price,
                    LotId = bid.LotId
                }
                ).ToList()
            };
            return lot;
        }
        #endregion

        #region Profile
        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
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

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                return null;
            return new ProfileEntity
            {
                Id = dalProfile.Id,
                Name = dalProfile.Name,
                Bids = dalProfile.Bids.Select(bid => new BidEntity
                {
                    Id = bid.Id,
                    Date = bid.Date,
                    LotId = bid.LotId,
                    Price = bid.Price,
                    ProfileId = bid.ProfileId
                }
                ).ToList(),
                Lots = dalProfile.Lots.Select(lot => new LotEntity
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
                    Tags = lot.Tags.Select(tag => new TagEntity
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    }
                    ).ToList(),
                    Bids = lot.Bids.Select(bid => new BidEntity
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

        public static DalBid ToDalBid(this BidEntity bidEntity)
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

        public static BidEntity ToBllBid(this DalBid dalBid)
        {
            if (dalBid == null)
                return null;
            return new BidEntity
            {
                Id = dalBid.Id,
                ProfileId = dalBid.ProfileId,
                Date = dalBid.Date,
                Price = dalBid.Price,
                LotId = dalBid.LotId
            };
        }

        #endregion
    }
}
