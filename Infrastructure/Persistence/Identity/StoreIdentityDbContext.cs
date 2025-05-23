using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<User>
    {
        public StoreIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
    }
}
