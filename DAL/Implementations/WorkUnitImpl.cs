using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class WorkUnitImpl : IWorkUnit
    {
        private readonly CinemaDataContext _context;
        private readonly ILogger<WorkUnitImpl> _logger;
        private bool _disposed = false;

        public WorkUnitImpl(CinemaDataContext context, ILogger<WorkUnitImpl> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CompleteAsync()
        {
            try
            {
                _logger.LogInformation("Saving changes to database.");
                await this._context.SaveChangesAsync();
                _logger.LogInformation("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes to database.");
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _logger.LogDebug("Disposing CinemaUnitOfWork.");
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
