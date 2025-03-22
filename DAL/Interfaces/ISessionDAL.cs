using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISessionDAL : IGenericDAL<Session>
    {
        Task<Session> GetByIdAsync(Guid sessionId);
        Task DeleteAsync(Guid sessionId);
        Task<List<Session>> GetByUserIdAsync(int userId);
        Task<Session> GetValidSessionAsync(Guid sessionId);
    }
}
