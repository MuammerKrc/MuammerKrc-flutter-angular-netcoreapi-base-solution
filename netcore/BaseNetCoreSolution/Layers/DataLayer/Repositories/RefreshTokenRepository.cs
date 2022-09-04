using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.IRepositories;
using CoreLayer.Models.JwtModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class RefreshTokenRepository:BaseRepository<RefreshToken,int>,IRefreshTokenRepository
    {

        public RefreshTokenRepository(AppDbContext context) : base(context)
        {

        }

    }
}
