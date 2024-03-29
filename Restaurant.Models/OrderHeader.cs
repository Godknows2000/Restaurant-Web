﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public User User { get; set; }
		[Required]
		[Display(Name = "Order Date")]
		public DateTime OrderDate { get; set; }
		[DisplayFormat(DataFormatString ="0:C")]
		[Display(Name ="Order Total")]
		public double OrderTotal { get; set; }
		[Required]
		[Display(Name ="Pick Up Time")]
		public DateTime PickUpTime { get; set; }
		[Required]
		[NotMapped]
		public DateTime PickUpDate { get; set;}
		public string Status { get; set; }
		public string? Comments { get; set; }
		public string TransactionId { get; set; }
		[Display(Name ="Pickup Name")]
		public string PickUpName { get; set; }
		[Display(Name ="Phone Number")]
		public string PhoneNumber { get; set; }
	}
}
