using ServiceStack;
using System.Runtime.Serialization;

namespace Northwind.ServiceModel
{
    [Route("/cached/customers"), Tag(Tags.Cached)]
    public class CachedGetAllCustomers : IReturn<CustomersResponse> {}

    [DataContract]
    [Route("/cached/customers/{Id}"), Tag(Tags.Cached)]
    public class CachedGetCustomerDetails : IReturn<CustomerDetailsResponse>
    {
        [DataMember]
        public string Id { get; set; }
    }

    [Tag(Tags.Cached)]
    [Route("/cached/orders")]
    [Route("/cached/orders/page/{Page}")]
    [Route("/cached/customers/{CustomerId}/orders")]
    public class CachedGetOrders : IReturn<OrdersResponse>
    {
        public int? Page { get; set; }
        public string CustomerId { get; set; }
    }
}