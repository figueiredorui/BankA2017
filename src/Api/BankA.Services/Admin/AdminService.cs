using BankA.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA.Services.Admin
{
    public class AdminService
    {
        VersionRepository versionRepository;
        public AdminService()
        {
            versionRepository = new VersionRepository();
        }

        public void CreateIfNotExists()
        {
            versionRepository.CreateIfNotExists();
        }

        public string GetDbVersion()
        {
            var t = versionRepository.Table.ToList();

            string version = versionRepository.Table.Select(q => q.Version).FirstOrDefault();
            return version;
        }

    }
}
