namespace EMSProj.Util
{
    public class PageModel
    {
        public PageModel()
        {

        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartRecord { get; private set; }
        public int EndRecord { get; private set; }

        // ---------
        public string Action { get; set; } = "Index";
        public string searchText { get; set; }
        public string sortExpression { get; set; }

        // ------------

        public PageModel(int totalItems, int currentPage, int pageSize = 5)
        {
            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            TotalPages = totalPages;

            int startPage = currentPage - pageSize;
            int endPage = currentPage + (pageSize - 1);

            if(startPage <= 0)
            {
                endPage = endPage-(startPage-1);
                startPage = 1;
            }

            if(endPage>totalPages)
            {
                endPage=totalPages;
                if (endPage > 10)
                    startPage = endPage - 9;
            }

            StartRecord = (currentPage - 1) * pageSize + 1;
            EndRecord = StartRecord - 1 + pageSize;

            if(EndRecord > TotalItems)
                EndRecord = TotalItems;

            if(TotalItems==0)
            {
                StartRecord = 0;
                StartPage = 0;
                CurrentPage = 0;
                EndRecord = 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
