using GapUp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GapUp.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }
    }
}
