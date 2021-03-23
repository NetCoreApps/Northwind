using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types
{
	[DataContract]
	public class CustomerOrder
	{
		[DataMember]
		public Order Order { get; set; }

		[DataMember]
		public List<OrderDetail> OrderDetails { get; set; }
	}
}