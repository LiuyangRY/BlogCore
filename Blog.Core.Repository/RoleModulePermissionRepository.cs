using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using Blog.Core.Repository.UnitOfWork;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Repository
{
    public class RoleModulePermissionRepository : BaseRepository<RoleModulePermission>, IRoleModulePermissionRepository
    {
        public RoleModulePermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
                (rmp, m, r) => new object[] 
                {
                    JoinType.Left, rmp.Module.Id.Equals(m.Id),
                    JoinType.Left, rmp.Role.Id.Equals(r.Id)
                },
                (rmp, m, r) => new RoleModulePermission
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },
                (rmp, m, r) => rmp.IsDeleted.Equals(false) && m.IsDeleted.Equals(false) && r.IsDeleted.Equals(false)
            );
        }

        public async Task<IEnumerable<RoleModulePermission>> GetRMPMaps()
        {
            return await Db.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.Module.Id)
                .Mapper(rmp => rmp.Permission, rmp => rmp.Permission.Id)
                .Mapper(rmp => rmp.Role, rmp => rmp.Role.Id)
                .ToPageListAsync(1, 5, 10);
        }

        public async Task UpdateModuleId(int permissionId, int moduleId)
        {
            await Db.Updateable<RoleModulePermission>(rmp => rmp.Module.Id.Equals(moduleId))
                .Where(rmp => rmp.Permission.Id.Equals(permissionId))
                .ExecuteCommandAsync();
        }
    }
}