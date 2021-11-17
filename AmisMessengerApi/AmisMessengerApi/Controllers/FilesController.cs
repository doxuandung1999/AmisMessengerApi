//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using AmisMessengerApi.Entities;
//using AmisMessengerApi.Helper;
//using AutoMapper;
//using AmisMessengerApi.Services;
//using AmisMessengerApi.Models.Files;

//namespace AmisMessengerApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FilesController : ControllerBase
//    {
//        private readonly DataContext _context;
//        private IMapper _IMapper;
//        private IFileService _IFileService;

//        public FilesController(DataContext context, IMapper imapper, IFileService ifileService)
//        {
//            _context = context;
//            _IMapper = imapper;
//            _IFileService = ifileService;
//        }

//        // up file lên database
//        [HttpPost("upFile")]
//        public IActionResult creatFile([FromBody] UpFile model)
//        {
//            // ánh xạ model đến File
//            var file = _IMapper.Map<File>(model);
//            try
//            {
//                _IFileService.creatFile(file);
//                return Ok();
//            }
//            catch (ApplicationException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        // get file theo convId
//        [HttpGet("{companyId}")]
//        public async Task<ActionResult<File>> GetFile(int companyId)
//        {
//            var file = await _context.Fileimg.Where(u => u.CompanyId == companyId).ToListAsync();

//            return Ok(file);
//        }

//        // GET: api/Files
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<File>>> GetFile()
//        {
//            return await _context.Fileimg.ToListAsync();
//        }


//        // GET: api/Files/5


//        // PUT: api/Files/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for
//        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutFile(int id, File file)
//        {
//            if (id != file.FileId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(file).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FileExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Files
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for
//        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//        [HttpPost]
//        public async Task<ActionResult<File>> PostFile(File file)
//        {
//            _context.Fileimg.Add(file);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetFile", new { id = file.FileId }, file);
//        }

//        // DELETE: api/Files/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<File>> DeleteFile(Guid id)
//        {
//            var file = await _context.Fileimg.FindAsync(id);
//            if (file == null)
//            {
//                return NotFound();
//            }

//            _context.Fileimg.Remove(file);
//            await _context.SaveChangesAsync();

//            return file;
//        }

//        private bool FileExists(int id)
//        {
//            return _context.Fileimg.Any(e => e.FileId == id);
//        }
//    }
//}
