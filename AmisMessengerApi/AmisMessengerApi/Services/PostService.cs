using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;
using AmisMessengerApi.Models.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmisMessengerApi.Services
{
    public interface IPostService
    {
        Post creatPost(Post Post);
        Task<GetPost> GetPost(int id, Guid userID);
        Task<Post> GetPostByPostID(int id);
        Task<List<GetPost>> GetPostByCompanyID(int companyID, Guid userID);
        Task<List<GetPost>> GetAllByUserID(Guid useID);
        Task<List<GetPost>> GetAll();
        Task<List<GetPost>> GetPostNotAccept();
        Task<Post> EditPost(Post model);
        Task<Post> UpdateStatus(int postID);
        Task<Post> DeletePost(int postID);
    }
    public class PostService : IPostService
    {
        private DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }

        public Post creatPost(Post Post)
        {
            //Post.ExpireDate = DateTime.ParseExact(Post.ExpireDate, "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            _context.jobpost.Add(Post);
            _context.SaveChanges();
            return Post;
        }

        public async Task<Post> EditPost(Post model)
        {
            if (model == null)
                throw new ApplicationException("Không có dữ liệu update");

            var post = _context.jobpost.FirstOrDefault(u => u.PostId == model.PostId);

            if (post == null)
            {
                throw new ApplicationException("Người dùng không tồn tại");
            }

            post.Title = model.Title;
            post.Status = model.Status;
            post.Career = model.Career;
            post.Location = model.Location;
            post.ExpireDate = model.ExpireDate;
            post.Quantity = model.Quantity;
            post.Salary = model.Salary;
            post.TypeJob = model.TypeJob;
            post.RequestSex = model.RequestSex;
            post.Experience = model.Experience;
            post.JobAddress = model.JobAddress;
            post.JobDescribe = model.JobDescribe;
            post.Request = model.Request;
            post.Benefit = model.Benefit;
            post.MethodApply = model.MethodApply;

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ApplicationException("Cập nhật thất bại!");
            }
            return post;
        }

        public async Task<Post> UpdateStatus(int postID)
        {
            var post = _context.jobpost.FirstOrDefault(u => u.PostId == postID);
            if (post == null)
            {
                throw new ApplicationException("Người dùng không tồn tại");
            }
            post.Status = 0;
            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ApplicationException("Cập nhật thất bại!");
            }
            return post;

        }

        public async Task<Post> DeletePost(int postID)
        {
            var post = _context.jobpost.FirstOrDefault(u => u.PostId == postID);
            _context.Entry(post).State = EntityState.Deleted;
            _context.SaveChanges();
            return null;
        }

        public async Task<GetPost> GetPost(int postId, Guid userId)
        {
            var listPost = await GetAllByUserID(userId);
            return listPost.FirstOrDefault(x => x.PostId == postId);
        }

        public async Task<Post> GetPostByPostID(int id)
        {
            return _context.jobpost.FirstOrDefault(x => x.PostId == id);
        }

        public async Task<List<GetPost>> GetPostNotAccept()
        {
            var listPost = await GetAll();
            return listPost.Where(x => x.Status == 1).ToList();
        }

        public async Task<List<GetPost>> GetPostByCompanyID(int companyID, Guid userID)
        {
            List<GetPost> result = new List<GetPost>();
            List<GetPost> listPost = await GetAllByUserID(userID);
            result = listPost.Where(x => x.CompanyId == companyID).ToList();
            return result;
        }

        // lấy tất cả post
        public async Task<List<GetPost>> GetAll()
        {
            List<GetPost> result = new List<GetPost>();
            var listJob = _context.jobpost.Join(
            _context.Company,
            job => job.CompanyId,
            company => company.CompanyId,
            (job, company) => new
            {
                PostId = job.PostId,
                CompanyId = job.CompanyId,
                Title = job.Title,
                CompanyName = company.CompanyName,
                CompanyAvatar = company.CompanyAvatar,
                IsFavourite = false,
                Status = job.Status
            }
        ).ToList();


            if (listJob.Count() > 0)
            {
                foreach (var item in listJob)
                {

                    GetPost post = new GetPost();
                    post.PostId = item.PostId;
                    post.CompanyId = item.CompanyId;
                    post.CompanyName = item.CompanyName;
                    post.CompanyAvatar = item.CompanyAvatar;
                    post.Title = item.Title;
                    post.IsFavourite = item.IsFavourite;
                    post.Status = item.Status;
                    result.Add(post);


                }
            }

            return result;

        }

        // lấy tất cả Post theo userID 
        public async Task<List<GetPost>> GetAllByUserID(Guid useID)
        {
            List<GetPost> result = new List<GetPost>();
            var listJob = _context.jobpost.Join(
            _context.Company,
            job => job.CompanyId,
            company => company.CompanyId,
            (job, company) => new
            {
                PostId = job.PostId,
                CompanyId = job.CompanyId,
                Title = job.Title,
                Status = job.Status,
                ExpireDate = job.ExpireDate,
                Salary = job.Salary,
                Quantity = job.Quantity,
                TypeJob = job.TypeJob,
                RequestSex = job.RequestSex,
                Experience = job.Experience,
                JobAddress = job.JobAddress,
                JobDescribe = job.JobDescribe,
                Request = job.Request,
                Benefit = job.Benefit,
                MethodApply = job.MethodApply,
                Career = job.Career,
                Location = job.Location,

                CompanyName = company.CompanyName,
                CompanyAvatar = company.CompanyAvatar,
                IsFavourite = false
            }
        ).ToList();

            var listJobCare = _context.jobcare.Where(x => x.UserId == useID).ToArray();
            List<int> listPostIDCare = new List<int>();
            if (listJobCare.Count() > 0)
            {
                foreach (var item in listJobCare)
                {
                    listPostIDCare.Add(item.PostId);
                }
            }
            if (listJob.Count() > 0)
            {
                foreach (var item in listJob)
                {

                    GetPost post = new GetPost();
                    post.PostId = item.PostId;
                    post.CompanyId = item.CompanyId;
                    post.CompanyName = item.CompanyName;
                    post.CompanyAvatar = item.CompanyAvatar;
                    post.Title = item.Title;
                    post.Status = item.Status;
                    post.ExpireDate = item.ExpireDate;
                    post.Salary = item.Salary;
                    post.Quantity = item.Quantity;
                    post.TypeJob = item.TypeJob;
                    post.RequestSex = item.RequestSex;
                    post.Experience = item.Experience;
                    post.JobAddress = item.JobAddress;
                    post.JobDescribe = item.JobDescribe;
                    post.Request = item.Request;
                    post.Benefit = item.Benefit;
                    post.MethodApply = item.MethodApply;
                    post.Career = item.Career;
                    post.Location = item.Location;
                    if (listPostIDCare.Contains(item.PostId))
                    {
                        post.IsFavourite = true;
                    }
                    else
                    {
                        post.IsFavourite = item.IsFavourite;
                    }
                    result.Add(post);


                }
            }

            return result;

        }

    }


}
