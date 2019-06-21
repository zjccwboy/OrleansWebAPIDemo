using System;
using System.Collections.Generic;
using System.Text;

namespace OrleansWebCore.Configuration
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据库名字
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 数据库用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return $"Data Source={this.DataSource};Initial Catalog={this.InitialCatalog};Persist Security Info=True;User ID={this.User};Password={this.Password}";
            }
        }
    }
}
