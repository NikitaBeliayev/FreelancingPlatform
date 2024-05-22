using Application.Objectives.Categories;
using Application.Objectives.Categories.ResponseDto;
using Application.Objectives.Types.ResponseDto;
using Application.Payments;
using Application.Users.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.ResponseDto
{
    public class TaskForYouDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public SimplePaymentDto Payment { get; set; }
        public decimal PaymentAmount { get; set; }
        public ICollection<CategoryDto> Tags { get; set; }
        public ResponseTypeDto Type { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
