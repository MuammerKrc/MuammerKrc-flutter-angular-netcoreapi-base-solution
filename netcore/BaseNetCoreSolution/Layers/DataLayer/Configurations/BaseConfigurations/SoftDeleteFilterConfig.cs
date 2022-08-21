using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations.BaseConfigurations
{
    public class SoftDeleteFilterConfig<TModel>:IEntityTypeConfiguration<TModel>where TModel:SoftDeleteModel
    {
        public void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
