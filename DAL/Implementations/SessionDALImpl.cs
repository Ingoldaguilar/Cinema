using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class SessionDALImpl : GenericDALImpl<Session>, ISessionDAL
    {
        CinemaDataContext _context;
        public SessionDALImpl(CinemaDataContext context, ILogger<SessionDALImpl> logger)
        : base(context, logger)
        {
            _context = context;
        }

        public async Task<Session> GetByIdAsync(Guid sessionId)
        {
            return await _context.Sessions.FindAsync(sessionId);
        }

        public async Task DeleteAsync(Guid sessionId)
        {
            var session = await GetByIdAsync(sessionId);
            if (session != null) _context.Sessions.Remove(session);
        }

        public async Task<List<Session>> GetByUserIdAsync(int userId)
        {
            return await _context.Sessions
                .Where(s => s.UserId == userId && s.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();
        }

        public async Task<Session> GetValidSessionAsync(Guid sessionId)
        {
            return await _context.Sessions
                .FirstOrDefaultAsync(s => s.SessionId == sessionId && s.ExpiresAt > DateTime.UtcNow);
        }
    }
}
