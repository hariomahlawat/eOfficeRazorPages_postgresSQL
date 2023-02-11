using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    public class OrdersToday
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Orders / Message")]
        public string Order { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
    }
}
