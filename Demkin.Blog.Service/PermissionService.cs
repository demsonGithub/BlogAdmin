using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Service
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        public PermissionService(IBaseRepository<Permission> baseRepository) : base(baseRepository)
        {
        }
    }
}