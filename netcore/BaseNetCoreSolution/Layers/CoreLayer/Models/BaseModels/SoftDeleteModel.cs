using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.BaseModels
{
    public class SoftDeleteModel
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
