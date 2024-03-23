using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.Categories.RequestDto
{
    public class CategorySearchDto
    {
        public string search {  get; set; } = string.Empty;
        public int pageSize { get; set; }
        public int skip { get; set; }
    }
}
