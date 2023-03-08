using FGD.Bussines.Model;
using FGD.Bussines.Service.Helper;
using FGD.Data.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace FGD.Bussines.Service
{
    public class RootFolderService : IRootFolderService<RootFolderModelBussines<int>, int>
    {
        private IRootFolderRepository<RootFolderModelBussines<int>, int> _rootFolderRepository;
        private HostingEnvironmentHelper _hostingEnvironmentHelper;
       

        public RootFolderService(IRootFolderRepository<RootFolderModelBussines<int>, int> rootFolderRepository,
            HostingEnvironmentHelper hostingEnvironmentHelper)
        {
            this._rootFolderRepository = rootFolderRepository;
            this._hostingEnvironmentHelper = hostingEnvironmentHelper;
        }

        public async Task<RootFolderModelBussines<int>> CreateRootFolderAsync()
        {
            var rootFolderMB = new RootFolderModelBussines<int>
            {
                Path = this._hostingEnvironmentHelper.CreateRootFolder()
            };

            return await this._rootFolderRepository.CreateAsync(rootFolderMB);
        } 

    }
}
