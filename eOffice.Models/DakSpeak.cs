using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eOffice.Models
{
	public class DakSpeak
	{
		public int Id { get; set; }

		public int DakId { get; set; }
		[ForeignKey("DakId")]
		public Dak Dak { get; set; }

		public string MarkedForId { get; set; }
		[ForeignKey("MarkedForId")]
		public ApplicationUser MarkedFor { get; set; }

		public string MarkedById { get; set; }
		[ForeignKey("MarkedById")]
		public ApplicationUser MarkedBy { get; set; }

		[Required]
		public DateTime MarkedOn { get; set; } = DateTime.Now;

		public string Remarks { get; set; } = "";
	}
}
