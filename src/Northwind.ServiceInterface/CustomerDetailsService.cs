using System;
using System.Net;
using Northwind.ServiceModel;
using Northwind.ServiceModel.Types;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Northwind.ServiceInterface
{
    public class CustomerDetailsService : Service
    {
        public CustomerDetailsResponse Get(GetCustomerDetails request)
        {
            var customer = Db.SingleById<Customer>(request.Id);
            if (customer == null)
                throw new HttpError(HttpStatusCode.NotFound,
                    new ArgumentException("Customer does not exist: " + request.Id));

            var ordersResponse = base.Gateway.Send(new GetOrders { CustomerId = customer.Id });
            return new CustomerDetailsResponse
            {
                Customer = customer,
                Orders = ordersResponse.Results,
            };
        }
    }
}
