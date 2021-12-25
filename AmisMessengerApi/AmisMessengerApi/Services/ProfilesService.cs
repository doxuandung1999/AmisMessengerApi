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
    public interface IProfilesService
    {
        Profiles creatProfiles(Profiles profiles);
        Task<List<Profiles>> GetListProfile(int postID);

    }
    public class ProfilesService : IProfilesService
    {
        private DataContext _context;
        public ProfilesService(DataContext context)
        {
            _context = context;
        }

        public Profiles creatProfiles(Profiles profiles)
        {
            //JobCare.ExpireDate = DateTime.ParseExact(JobCare.ExpireDate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            _context.profiles.Add(profiles);
            _context.SaveChanges();
            return profiles;
        }

        public async Task<List<Profiles>> GetListProfile(int postID)
        {
            List<Profiles> result = new List<Profiles>();
            result = _context.profiles.Where(x => x.PostId == postID).ToList();

            if (result == null)
            {
                return null;
            }

            return result;
        }




    }


}
