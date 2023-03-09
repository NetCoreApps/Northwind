using System.Runtime.Serialization;

namespace Northwind.ServiceModel.Types;

[DataContract]
public class EmployeeTerritory
{
	public string Id => EmployeeId + "/" + TerritoryId;

	[DataMember]
	public int EmployeeId { get; set; }

	[DataMember]
	public string TerritoryId { get; set; }
}