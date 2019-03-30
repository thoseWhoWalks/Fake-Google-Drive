using FGD.Data.Configuration;
using FGD.Data.Configuration.Account;
using FGD.Data.Configuration.Notification;
using FGD.Data.Configuration.Storage;
using FGD.Data.Configuration.Storage.Shared;
using Microsoft.EntityFrameworkCore;

namespace FGD.Data
{
    public class FakeGoogleDriveContext : DbContext
    { 

        public FakeGoogleDriveContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            
            modelBuilder.ApplyConfiguration(new AccountInfoConfiguration());

            modelBuilder.ApplyConfiguration(new AccountCryptoConfiguration());

            modelBuilder.ApplyConfiguration(new AccountSubscriptionConfiguration());

            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());

            modelBuilder.ApplyConfiguration(new RootFolderConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationStateConfiguration());

            modelBuilder.ApplyConfiguration(new StoredFolderConfiguration());

            modelBuilder.ApplyConfiguration(new StoredFileConfiguration());

            modelBuilder.ApplyConfiguration(new SharedFileConfiguration());

            modelBuilder.ApplyConfiguration(new SharedFolderConfiguration());

        }

        public DbSet<AccountModel<int>> Accounts { get; set; }
         
        public DbSet<AccountInfoModel<int>> AccountInfos { get; set; }

        public DbSet<AccountCryptoModel<int>> AccountCryptos { get; set; }

        public DbSet<AccountSubscriptionModel<int>> AccountSubscriptions { get; set; }

        public DbSet<SubscriptionModel<int>> Subscriptions { get; set; }

        public DbSet<RootFolderModel<int>> RootFolders { get; set; }

        public DbSet<NotificationModel<int>> Notifications { get; set; }

        public DbSet<NotificationStateModel> NotificationStates { get; set; }
         
        public DbSet<StoredFolderModel<int>> StoredFolders { get; set; }

        public DbSet<StoredFileModel<int>> StoredFiles { get; set; }

        public DbSet<SharedFileModel<int>> SharedFiles { get; set; }

        public DbSet<SharedFolderModel<int>> SharedFolders { get; set; }

    }
}
