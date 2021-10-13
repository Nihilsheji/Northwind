using Northwind.DbContexts;
using Northwind.Models.Entities;
using Northwind.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class RegionsService : CrudServiceBase<Region>, IRegionsService
    {
        private readonly INorthwindDbContext _context;

        public RegionsService(INorthwindDbContext context) : base(context) { }
    }
}
