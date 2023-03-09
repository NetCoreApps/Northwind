using Northwind.ServiceModel;
using ServiceStack;

namespace Northwind.ServiceInterface;

[CacheResponse(Duration = 60 * 60, MaxAge = 30 * 60)]
public class CachedServices : Service
{
    public object Get(CachedGetAllCustomers request) => 
        Gateway.Send(new GetAllCustomers());

    public object Get(CachedGetCustomerDetails request) => 
        Gateway.Send(new GetCustomerDetails { Id = request.Id });

    public object Get(CachedGetOrders request) => 
        Gateway.Send(new GetOrders { CustomerId = request.CustomerId, Page = request.Page });
}
