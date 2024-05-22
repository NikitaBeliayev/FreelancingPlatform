using Application.Objectives.Categories;
using Application.Objectives.Types.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Users;

namespace Application.Objectives.ResponseDto
{
    public class GetObjectiveResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PaymentAmount { get; set; }
        public string CreatorPublicContacts { get; set; }
        public UserDto Creator { get; set; }
        public ICollection<CategoryDto> Tags { get; set; }
        public ResponseTypeDto Type { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
