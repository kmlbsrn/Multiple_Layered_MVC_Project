using BilgeShop.Business.Dtos.User;
using BilgeShop.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeShop.Business.Services
{
    public interface IUserService
    {
        ServiceMessage AddUser(UserAddDto userAddDto);


        UserInfoDto Login(UserLoginDto userLoginDto);
    }
}
