package Entities;

public class Country implements DatabaseElement 
{
	//properties
	private int id;
	private int isActive;
	private String name;
	private int regionId;
	private String createdDate;
	private String lastModifiedDate;

	//constructor
	public Country(int id, int isActive, String name, int regionId, String createdDate, String lastModifiedDate) {
		super();
		this.id = id;
		this.isActive = isActive;
		this.name = name;
		this.regionId = regionId;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}

	//to string method
	@Override
	public String toString() {
		return "Country [id=" + id + ", isActive=" + isActive + ", name=" + name + ", regionId=" + regionId
				+ ", createdDate=" + createdDate + ", lastModifiedDate=" + lastModifiedDate + "]";
	}


	//SQL insert statement
	public String generateSQLInsertStatement() 
	{
		return "INSERT INTO Countries "
    			+ "(Id, IsActive, Name, RegionId, CreatedDate, LastModifiedDate) "
				+ "VALUES ("
				+ id + ", " + isActive + ", \"" + name + "\", " + regionId + ", \""
				+ createdDate + "\", \"" + lastModifiedDate + "\")";
	}

}
