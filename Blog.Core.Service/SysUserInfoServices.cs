using Blog.Core.Common.Helper;
using Blog.Core.Model.Models;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Service
{
    public class SysUserInfoServices : BaseService<SysUserInfo>, ISysUserInfoServices
    {
        private readonly IBaseRepository<UserRole> userRoleRepository;

        private readonly IBaseRepository<Role> roleRepository;

        public SysUserInfoServices(IBaseRepository<SysUserInfo> repository, IBaseRepository<UserRole> userRoleRepository, IBaseRepository<Role> roleRepository)
        {
            base.repository = repository;
            this.userRoleRepository = userRoleRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<SysUserInfo> SaveUserInfo(string loginName, string loginPwd)
        {
            SysUserInfo model = new SysUserInfo();
            var userList = (await Query(u => u.LoginName.Equals(loginName) && u.LoginPwd.Equals(loginPwd))).ToList();
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await Add(new SysUserInfo(loginName, loginPwd));
                model = await QueryById(id);
            }
            return model;
        }


        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await Query(u => u.LoginName.Equals(loginName) && u.LoginPwd.Equals(loginPwd))).FirstOrDefault();
            var roleList = await roleRepository.Query(r => r.IsDeleted.Equals(false));
            if(user != null)
            {
                var userRoles = (await userRoleRepository.Query(ur => ur.UserId.Equals(user.Id))).ToList();
                if(userRoles.Count > 0)
                {
                    var urArr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(r => urArr.Contains(r.Id.ObjToString()));
                    roleName = string.Join(",", roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }
    }
}