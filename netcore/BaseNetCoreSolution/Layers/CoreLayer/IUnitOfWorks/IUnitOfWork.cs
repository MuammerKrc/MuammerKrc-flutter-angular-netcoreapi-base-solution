using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace CoreLayer.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync();
        void SaveChange();
    }
}
