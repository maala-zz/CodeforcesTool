using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.Models.Helpers
{
    public class HomePageParameters
    {
        const int maxPageSize = 30;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = ( value > maxPageSize )? maxPageSize : value ;
            }
        }

        public string TagName { get; set; }
        public string UserHandle { get; set; }
        public string Solved { get; set; }
        public string Search { get; set; }
    }
}
