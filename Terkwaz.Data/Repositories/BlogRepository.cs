using System;
using Terkwaz.Data.Context;

namespace Terkwaz.Data.Repositories
{
    public class BlogRepository
    {
        private readonly TerkwazDbContext _context;

        public BlogRepository(TerkwazDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("DbContext is null");
        }

    }
}
