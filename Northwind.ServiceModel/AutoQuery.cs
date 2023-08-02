using Northwind.ServiceModel.Types;
using ServiceStack;

namespace Northwind.ServiceModel;

[Route("/query/customers"), Tag(Tags.AutoQuery)]
public class QueryCustomers : QueryDb<Customer>
{
    public List<string> Ids { get; set; }
    public string CountryStartsWith { get; set; }
}

[Route("/query/orders"), Tag(Tags.AutoQuery)]
public class QueryOrders : QueryDb<Order>
{
    public decimal? Freight { get; set; }
}