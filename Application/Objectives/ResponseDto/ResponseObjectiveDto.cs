using Application.Objectives.Categories;
using Application.Objectives.Types;
using Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.ResponseDto
{
	public class ResponseObjectiveDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Guid PaymentId { get; set; }
		public decimal PaymentAmount { get; set; }
		public string CreatorPublicContacts { get; set; }
		public ICollection<CategoryDto> Tags { get; set; }
		public UserDto Creator { get; set; }
		public TypeDto Type { get; set; }
		public DateTime Deadline { get; set; }
	}
}
