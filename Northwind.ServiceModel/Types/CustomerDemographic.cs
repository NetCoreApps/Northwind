using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types;

[DataContract]
public class CustomerDemographic
{
	[DataMember]
	public string Id { get; set; }

	[DataMember]
	public string CustomerDesc { get; set; }
}