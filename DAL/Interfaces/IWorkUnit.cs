using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWorkUnit : IDisposable
    {
        IUserDAL UserDAL { get; }
        ISessionDAL SessionDAL { get; }
        Task CompleteAsync();
    }
}
