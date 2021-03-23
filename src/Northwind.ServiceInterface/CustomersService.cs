using Northwind.ServiceModel;
using Northwind.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Northwind.ServiceInterface
{
    public class CustomersService : Service
    {
        public object Get(GetAllCustomers request) => 
            new CustomersResponse { Results = Db.Select<Customer>() };
    }
}
