using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmisMessengerApi.Entities

{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public Guid UserId { get; set; }
        public int CompanyId { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Salary { get; set; }
        // số lượng tuyển
        public int Quantity { get; set; }
        // loại công việc : 0 partime , 1 toàn tg
        public int TypeJob { get; set; }
        public int RequestSex { get; set; }
        public string Experience { get; set; }
        public string JobAddress { get; set; }
        public string JobDescribe { get; set; }
        public string Request { get; set; }
        public string Benefit { get; set; }
        public string MethodApply { get; set; }
        // ngành nghề
        public int Career { get; set; }
        public int Location { get; set; }

    }
}
