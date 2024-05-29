using Application.Objectives.Categories;
using Application.Objectives.Categories.ResponseDto;
using Application.Objectives.Types;
using Application.Objectives.Types.ResponseDto;
using Application.Payments;
using Application.Users;
using Application.Users.ResponseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.RequestDto
{
	public class ObjectiveCreateDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		// public PaymentDto Payment { get; set; }
		public decimal PaymentAmount { get; set; }
		public ICollection<SimpleCategoryResponseDto> Tags { get; set; }
		public string CreatorPublicContacts { get; set; }
		public SimpleResponseTypeDto Type { get; set; }
		public DateTime Deadline { get; set; }
	}
}
