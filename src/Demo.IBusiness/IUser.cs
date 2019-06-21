using Demo.DataTranferObject;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Demo.IBusiness
{
    public interface IUser : IGrainWithIntegerKey
    {
        Task<string> GetUserName(long userId);

        Task<GetUserInfo_Out> GetUserInfo(GetUserInfo_In input);
    }
}
