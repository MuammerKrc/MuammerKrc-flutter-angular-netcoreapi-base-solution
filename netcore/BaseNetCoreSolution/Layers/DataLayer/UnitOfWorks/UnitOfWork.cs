using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IRepositories;
using CoreLayer.IUnitOfWorks;
using CoreLayer.Models.BaseModels;
using CoreLayer.Models.IdentityModels;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }


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
