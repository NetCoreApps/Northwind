using ServiceStack;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Northwind.ServiceModel.Types;

namespace Northwind.ServiceModel
{
    [Route("/customers"), Tag(Tags.Customers)]
    public class GetAllCustomers : IGet, IReturn<CustomersResponse> {}

    [DataContract]
    public class CustomersResponse : IHasResponseStatus
    {
        [DataMember]
        public List<Customer> Results { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}