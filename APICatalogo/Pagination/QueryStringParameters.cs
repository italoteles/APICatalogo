namespace APICatalogo.Pagination
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                //se o valor da página for maior que o máximo permitido, então coloque o maxPageSize 
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
