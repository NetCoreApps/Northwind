using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types;

[DataContract]
public class CustomerCustomerDemo
{
	[DataMember]
	public string Id { get; set; }

	[DataMember]
	public string CustomerTypeId { get; set; }
}