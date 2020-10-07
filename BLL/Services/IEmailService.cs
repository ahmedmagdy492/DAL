using DAL.Models;
using SharedLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IEmailService
    {
        bool Send(SendingEmailViewModel model);
    }
}
