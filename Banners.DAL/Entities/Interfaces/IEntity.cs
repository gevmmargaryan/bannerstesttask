using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banners.DAL.Entities.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedDt { get; set; }
        int CreatedBy { get; set; }
        DateTime UpdatedDt { get; set; }
        int UpdatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
