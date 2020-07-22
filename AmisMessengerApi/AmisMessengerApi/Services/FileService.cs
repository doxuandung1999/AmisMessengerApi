using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmisMessengerApi.Entities;
using AmisMessengerApi.Helper;

namespace AmisMessengerApi.Services
{
    public interface IFileService
    {
        File creatFile(File file);
    }
    public class FileService : IFileService
    {
        private DataContext _context;
       public FileService(DataContext context)
        {
            _context = context;
        }

        public File creatFile(File file)
        {
            _context.File.Add(file);
            _context.SaveChanges();
            return file;
        }

    }

    
}
