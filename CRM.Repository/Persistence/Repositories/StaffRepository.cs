﻿using CRM.DAL;
using CRM.Repository.Core.Repositories;

namespace CRM.Repository.Persistence.Repositories
{
    public class StaffRepository : Repository<Entities.DataModels.Basic.Staff>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
