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
    public interface IJobCareService
    {
        JobCare creatJobCare(JobCare JobCare);
        Task<JobCare> GetJobCare(int id);

        JobCare UpdateJobCare(JobCare jobCare);
        //IEnumerable<JobCare> GetAll();
    }
    public class JobCareService : IJobCareService
    {
        private DataContext _context;
        public JobCareService(DataContext context)
        {
            _context = context;
        }

        public JobCare creatJobCare(JobCare JobCare)
        {
            //JobCare.ExpireDate = DateTime.ParseExact(JobCare.ExpireDate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            _context.jobcare.Add(JobCare);
            _context.SaveChanges();
            return JobCare;
        }
        
        public JobCare UpdateJobCare(JobCare jobCare)
        {
            var jobCareObj = _context.jobcare.FirstOrDefault(x => x.PostId == jobCare.PostId && x.UserId == jobCare.UserId);
            if(jobCareObj == null)
            {
                return creatJobCare(jobCare);
            }
            else
            {
                _context.Entry(jobCareObj).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return null;
        }

        public async Task<JobCare> GetJobCare(int JobCareId)
        {
            var JobCare =  _context.jobcare.FirstOrDefault(x => x.JobCareId == JobCareId);

            if (JobCare == null)
            {
                return null;
            }

            return JobCare;
        }


    }


}
