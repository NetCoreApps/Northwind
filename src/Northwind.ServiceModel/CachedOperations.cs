using ServiceStack;

namespace Northwind.ServiceModel
{
    [Route("/cached/customers"), Tag(Tags.Cached)]
    public class CachedGetAllCustomers : IGet, IReturn<CustomersResponse> {}

    [Route("/cached/customers/{Id}"), Tag(Tags.Cached)]
    public class CachedGetCustomerDetails : IGet, IReturn<CustomerDetailsResponse>
    {
        public string Id { get; set; }
    }

    [Tag(Tags.Cached)]
    [Route("/cached/orders")]
    [Route("/cached/orders/page/{Page}")]
    [Route("/cached/customers/{CustomerId}/orders")]
    public class CachedGetOrders : IGet, IReturn<OrdersResponse>
    {
        public int? Page { get; set; }
        public string CustomerId { get; set; }
    }
}