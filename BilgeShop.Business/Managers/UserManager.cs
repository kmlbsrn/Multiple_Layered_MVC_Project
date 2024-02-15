using BilgeShop.Business.Dtos.User;
using BilgeShop.Business.Services;
using BilgeShop.Business.Types;
using BilgeShop.Data.Entities;
using BilgeShop.Data.Enums;
using BilgeShop.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepo;

     

        private readonly IDataProtector _dataProtector;

        public UserManager(IRepository<UserEntity> userRepo, IDataProtectionProvider dataProtectionProvider)
        {

            _userRepo = userRepo;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(UserAddDto userAddDto)
        {

            var hasMail = _userRepo.GetAll(x => x.Email.ToLower() == userAddDto.Email.ToLower()).ToList();


            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu mail Kullanılmaktadır."

                };
            }

            var userEntity = new UserEntity()
            {
                Email = userAddDto.Email,
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Password = _dataProtector.Protect(userAddDto.Password),
                UserType = UserTypeEnum.User
            };
        
            _userRepo.Add(userEntity);
            

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kullanıcı Başarıyla Eklendi"
            };

        }

        public UserInfoDto Login(UserLoginDto userLoginDto)
        {
            var userEntity = _userRepo.Get(x => x.Email.ToLower() == userLoginDto.Email.ToLower());

            if (userEntity is null)
            {
                return null;
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password);


            if (rawPassword == userLoginDto.Password)
            {
                return new UserInfoDto
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType


                };

            }

            else
            {
                return null;
            }

        }
    }
}
