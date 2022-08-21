using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IUnitOfWorks;

namespace DataLayer.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        //private UserRefreshTokenRepository _userRefreshTokenRepository;

        //public IUserRefreshTokenRepository UserRefreshTokenRepository => _userRefreshTokenRepository =
        //    _userRefreshTokenRepository ?? new UserRefreshTokenRepository(context);

        public async Task SaveChangeAsync()
        {
            context.ChangeTracker.Entries().ToList().ForEach(i =>
            {
                if (i.Entity is TimeModel model)
                {
                    if (i.State == EntityState.Added)
                    {
                        model.CreationTime = DateTime.Now;
                    }

                    if (i.State == EntityState.Modified)
                    {
                        model.ModifiedTime = DateTime.Now;
                    }
                }
            });

            await context.SaveChangesAsync();
        }

        public void SaveChange()
        {
            context.ChangeTracker.Entries().ToList().ForEach(i =>
            {
                if (i.Entity is TimeModel model)
                {
                    if (i.State == EntityState.Added)
                    {
                        model.CreationTime = DateTime.Now;
                    }

                    if (i.State == EntityState.Modified)
                    {
                        model.ModifiedTime = DateTime.Now;
                    }
                }
            });

            context.SaveChanges();
        }
    }
}
