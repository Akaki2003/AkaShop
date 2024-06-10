namespace AkaShop.Application.BaseModels
{
    public class BaseListRequestModel
    {
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
    }
}
