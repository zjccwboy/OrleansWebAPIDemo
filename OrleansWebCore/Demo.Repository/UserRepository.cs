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
            {1, new UserInfoEntity{ UserId=1, UserName = "User1", Password="123456", NikeName="User1NikeName", Sex =true,} },
            {2, new UserInfoEntity{ UserId=2, UserName = "User2", Password="123456", NikeName="User2NikeName", Sex =true,} },
            {3, new UserInfoEntity{ UserId=3, UserName = "User3", Password="123456", NikeName="User3NikeName", Sex =true,} },
            {4, new UserInfoEntity{ UserId=4, UserName = "User4", Password="123456", NikeName="User4NikeName", Sex =true,} },
            {5, new UserInfoEntity{ UserId=5, UserName = "User5", Password="123456", NikeName="User5NikeName", Sex =true,} },
            {6, new UserInfoEntity{ UserId=6, UserName = "User6", Password="123456", NikeName="User6NikeName", Sex =true,} },
            {7, new UserInfoEntity{ UserId=7, UserName = "User7", Password="123456", NikeName="User7NikeName", Sex =true,} },
            {8, new UserInfoEntity{ UserId=8, UserName = "User8", Password="123456", NikeName="User8NikeName", Sex =true,} },
            {9, new UserInfoEntity{ UserId=9, UserName = "User9", Password="123456", NikeName="User9NikeName", Sex =true,} },
        };

        public Task<UserInfoEntity> GetUserInfo(long userId)
        {
            UserTable.TryGetValue(userId, out UserInfoEntity userInfo);
            return Task.FromResult(userInfo);
        }
    }
}
