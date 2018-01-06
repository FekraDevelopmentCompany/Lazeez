using System;
using System.Data.Entity;
using System.Linq;
using Lazeez.Helper;

namespace Lazeez.Repository.Infrastructure
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Name=LazeezEntities")
        { }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                                       .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            int currentUserId = GlobalSettings.CurrentUserID;
            DateTime now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatorUserID").CurrentValue = currentUserId;
                    entry.Property("CreationDate").CurrentValue = now;
                }
                else
                {
                    entry.Property("CreatorUserID").IsModified = false;
                    entry.Property("CreationDate").IsModified = false;
                }

                entry.Property("ModifiedUserID").CurrentValue = currentUserId;
                entry.Property("ModificationDate").CurrentValue = now;
            }

            return base.SaveChanges();
        }
    }
}
