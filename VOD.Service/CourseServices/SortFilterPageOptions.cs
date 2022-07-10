using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Service.CourseServices.QueryObjects;

namespace VOD.Service.CourseServices
{
    public class SortFilterPageOptions
    {
        public const int DefaultPageSize = 10;
        private int _pageNum = 1;
        private int _pageSize = DefaultPageSize;

        /// <summary>
        /// Holds possible pagesize
        /// </summary>
        public int[] PageSizes = new[] { 5, DefaultPageSize, 20, 50, 100, 500, 1000 };

        public OrderByOptions OrderByOptions { get; set; }

        public CoursesFilterBy FilterBy { get; set; }

        public string FilterValue { get; set; }

        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int NumPages { get; private set; }

        public string PrevCheckState { get; set; }

        public void SetupRestOfDTO<T>(IQueryable<T> query)
        {
            NumPages = (int)Math.Ceiling(
                (double)query.Count() / PageSize);
            PageNum = Math.Min(
                Math.Max(1, PageNum), NumPages);

            var newCheckState = GenerateCheckState();

            if (PrevCheckState != newCheckState)
                PageNum = 1;

            PrevCheckState = newCheckState;
        }
        //private methods
        /// <summary>
        /// This returns a string containing the state of the SortFilterPage data
        /// that, if the change, should cause PageNum to be set back to 0
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckState()
        {
            return $"{(int)FilterBy},{FilterValue},{PageSize},{NumPages}";
        }
    }
}
