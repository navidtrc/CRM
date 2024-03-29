﻿using CRM.DAL;
using CRM.Repository.Core.Repositories;
using CRM.Entities.DataModels.Security;

namespace CRM.Repository.Persistence.Repositories
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        public SettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
