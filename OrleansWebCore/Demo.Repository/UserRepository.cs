using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository
{
    public class UserRepository
    {
        private Dictionary<long, UserInfoEntity> UserTable { get; } = new Dictionary<long, UserInfoEntity>()
        {
            {1000000001, new UserInfoEntity{ UserId=1, UserName = "User1", Password="123456", NikeName="User1NikeName", Sex =true,} },
            {1000000002, new UserInfoEntity{ UserId=2, UserName = "User2", Password="123456", NikeName="User2NikeName", Sex =true,} },
            {1000000003, new UserInfoEntity{ UserId=3, UserName = "User3", Password="123456", NikeName="User3NikeName", Sex =true,} },
            {1000000004, new UserInfoEntity{ UserId=4, UserName = "User4", Password="123456", NikeName="User4NikeName", Sex =true,} },
            {1000000005, new UserInfoEntity{ UserId=5, UserName = "User5", Password="123456", NikeName="User5NikeName", Sex =true,} },
        };

        public Task<UserInfoEntity> GetUserInfo(long userId)
        {
            UserTable.TryGetValue(userId, out UserInfoEntity userInfo);
            return Task.FromResult(userInfo);
        }
    }
}
