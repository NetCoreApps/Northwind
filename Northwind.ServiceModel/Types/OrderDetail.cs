using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types;

[DataContract]
public class OrderDetail
{
	public string Id => OrderId + "/" + ProductId;

	[DataMember]
	public int OrderId { get; set; }

	[DataMember]
	public int ProductId { get; set; }

	[DataMember]
	public decimal UnitPrice { get; set; }

	[DataMember]
	public short Quantity { get; set; }

	[DataMember]
	public double Discount { get; set; }
}