package Entities;

public class PathwayTemplates implements DatabaseElement {
	
	//Properties
	private int id;
	private String createdBy;
	private String description;
	private String name;
	private int pathwayTypeId;
	private int regionId;
	private String createdDate;
	private String lastModifiedDate;
	
	
	//constructor
	public PathwayTemplates(int id, String createdBy, String description, String name, int pathwayTypeId, int regionId,
			String createdDate, String lastModifiedDate) {
		super();
		this.id = id;
		this.createdBy = createdBy;
		this.description = description;
		this.name = name;
		this.pathwayTypeId = pathwayTypeId;
		this.regionId = regionId;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}

	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO PathwayTemplates "
	+ "(Id, CreatedBy, Description, Name, PathwayTypeId, RegionId ,CreatedDate, LastModifiedDate) "
	+ "VALUES ("
	+ id + ", '" + createdBy + "', '" + description + "', '" + name + "', " 
	+ pathwayTypeId + ", " + regionId + ", '"+createdDate+"', '"+lastModifiedDate+"')";
	}


	

}
