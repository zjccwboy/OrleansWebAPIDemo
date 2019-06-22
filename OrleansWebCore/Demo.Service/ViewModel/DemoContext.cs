using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.ViewModel
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options): base(options)
        {
        }

        public DbSet<GetUserInfo_Request> GetUserInfo_Request { get; set; }
        public DbSet<GetUserInfo_Response> GetUserInfo_Response { get; set; }
    }
}
