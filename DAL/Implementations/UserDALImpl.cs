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
    public class UserDALImpl : GenericDALImpl<User>, IUserDAL
    {
        CinemaDataContext _context { get; set; }
        ILogger<UserDALImpl> _logger { get; set; }

        public UserDALImpl(CinemaDataContext context, ILogger<UserDALImpl> logger)
            : base(context,logger)
        {
            this._context = context;
            this._logger = logger;
        }

    }
}
