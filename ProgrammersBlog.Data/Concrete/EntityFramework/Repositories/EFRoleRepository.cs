using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EFRoleRepository : EFEntityRepositoryBase<Role>, IRoleRepository
    {
        public EFRoleRepository(DbContext context) : base(context)
        {

        }
    }
}
