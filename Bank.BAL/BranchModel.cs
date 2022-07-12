using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bank.BAL
{
    public class BranchModel
    {
        public int BranchId { get; set; }
        [Required]
        
        public string BranchName { get; set; }
        [Required]
       
        public long BranchNumber { get; set; }
    }
}
