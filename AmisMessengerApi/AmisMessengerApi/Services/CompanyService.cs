using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmisMessengerApi.Services
{
    public interface ICompanyService
    {
        Company creatCompany(Company company);
        Task<Company> EditCompany(Company model);

        Task<Company> GetCompany(Guid id);
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

        public async Task<Company> EditCompany(Company model)
        {

            if (model == null)
                throw new ApplicationException("Không có dữ liệu update");

            var company = _context.Company.FirstOrDefault(u => u.UserId == model.UserId);

            if (company == null)
            {
                throw new ApplicationException("Người dùng không tồn tại");
            }


            company.CompanyBanner = model.CompanyBanner;
            company.CompanyAvatar = model.CompanyAvatar;
            company.Address = model.Address;
            company.CompanyDescriber = model.CompanyDescriber;
            company.CompanyName = model.CompanyName;

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ApplicationException("Cập nhật thất bại!");
            }
            return company;
        }

        public async Task<Company> GetCompany(Guid id)
        {
            var user =  _context.Company.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

    }


}
