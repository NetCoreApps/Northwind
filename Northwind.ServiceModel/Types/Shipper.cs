using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types;

[DataContract]
public class Shipper
{
	[DataMember]
	public int Id { get; set; }

	[DataMember]
	public string CompanyName { get; set; }

	[DataMember]
	public string Phone { get; set; }
}