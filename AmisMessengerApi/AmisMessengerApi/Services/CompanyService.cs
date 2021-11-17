using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;

namespace AmisMessengerApi.Services
{
    public interface ICompanyService
    {
        Company creatCompany(Company company);
    }
    public class CompanyService : ICompanyService
    {
        private DataContext _context;
        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public Company creatCompany(Company company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();
            return company;
        }

    }


}
