using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Northwind.ServiceModel.Types;

namespace Northwind.ServiceModel
{
    [Tag(Tags.Orders)]
    [Route("/orders")]
    [Route("/orders/page/{Page}")]
    [Route("/customers/{CustomerId}/orders")]
    public class GetOrders : IGet, IReturn<OrdersResponse>
    {
        public int? Page { get; set; }
        public string CustomerId { get; set; }
    }

    [DataContract]
    public class OrdersResponse : IHasResponseStatus
    {
        [DataMember]
        public List<CustomerOrder> Results { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}