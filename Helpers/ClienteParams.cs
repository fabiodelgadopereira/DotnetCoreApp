namespace CadastroApp.API.Helpers
{
    public class ClienteParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize;}
               set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }
    }
}