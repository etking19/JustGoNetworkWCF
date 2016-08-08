using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dao
{
    public class CompanyDao : BaseDao
    {
        public Model.Company GetCompany(string companyId)
        {
            return new Model.Company()
            {
                companyId = "1",
                address1 = "address 1",
                address2 = "address 2",
                name = "company",
                enabled = true,
                deleted = false,
                countryId = "1",
                postcode = "123456",
                registrationNumber = "1234567890",
                stateId = "1",
                creationDate = "",
                lastModifiedDate = ""
            };
        }

        public List<Model.Company> GetAllCompanies(string number, string skip)
        {
            List<Model.Company> companyList = new List<Model.Company>();
            for (int i = 0; i < int.Parse(number); i++)
            {
                companyList.Add(new Model.Company()
                {
                    companyId = "" + i,
                    address1 = "address 1",
                    address2 = "address 2",
                    name = "company " + i,
                    enabled = true,
                    deleted = false,
                    countryId = "1",
                    postcode = "123456",
                    registrationNumber = "1234567890",
                    stateId = "1",
                    creationDate = "",
                    lastModifiedDate = ""
                });
            }

            return companyList;
        }

        public bool UpdateCompany(string companyId, Model.Company company)
        {
            return true;
        }

        public bool DeleteCompany(string companyId)
        {
            return true;
        }

        public string AddCompany(Model.Company company)
        {
            return "1";
        }
    }
}