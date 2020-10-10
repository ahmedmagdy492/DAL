using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBranchService : IRepository<Branch>
    {
        void ChangeOnlineStatus(int branchId, bool isOnline);
    }
}
