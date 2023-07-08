using eOffice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eOffice.DataAccess.Data
{

    public class ApplicationDbContext : IdentityDbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<SocialCalendar> SocialCalendar { get; set; }
        public DbSet<Dak> Dak { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<DakComments> DakComments { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<DraftLetter> DraftLetters { get; set; }
        public DbSet<MarkedDak> MarkedDak { get; set; }
        public DbSet<EventCalendar> EventCalendar { get; set; }
        public DbSet<OrdersToday> OrdersToday { get; set; }
        public DbSet<OfficeBranch> OfficeBranch { get; set; }
        public DbSet<OfficeBranchSection> OfficeBranchSection { get; set; }
        public DbSet<OrderImage> OrderImages { get; set; }
        public DbSet<DakVisibilityTag> DakVisibilityTag { get; set; }
		public DbSet<DakSpeak> DakSpeak { get; set; }
	}
    
}
