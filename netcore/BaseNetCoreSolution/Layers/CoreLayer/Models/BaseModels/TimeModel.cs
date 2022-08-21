using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models.BaseModels
{
    public class TimeModel:SoftDeleteModel
    {
        public DateTime CreationTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
