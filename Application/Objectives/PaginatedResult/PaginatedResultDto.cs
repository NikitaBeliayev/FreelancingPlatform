using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.PaginatedResult
{
	public class PaginatedResultDto<T>
	{
		public int Count { get; set; }
		public string? Next { get; set; }
		public string? Previous { get; set; }
		public IEnumerable<T>? Results { get; set; }
	}
}
