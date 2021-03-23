using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Northwind.ServiceModel.Types;

namespace Northwind.ServiceModel
{
    [Route("/customers/{Id}"), Tag(Tags.Customers)]
    public class GetCustomerDetails : IReturn<CustomerDetailsResponse>
    {
        public string Id { get; set; }
    }

    [DataContract]
    public class CustomerDetailsResponse : IHasResponseStatus
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public List<CustomerOrder> Orders { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}