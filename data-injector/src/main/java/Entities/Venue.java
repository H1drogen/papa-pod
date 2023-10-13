package Entities;

public class Venue implements DatabaseElement
{
	private int id;
	private int active;
	private int maxCapacity;
	private String name;
	private int officeId;
	private String createdDate;
	private String lastModifiedDate;
	
	public Venue(int id, int active, int maxCapacity, String name, int officeId, String createdDate,
			String lastModifiedDate) {
		super();
		this.id = id;
		this.active = active;
		this.maxCapacity = maxCapacity;
		this.name = name;
		this.officeId = officeId;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	public String getName() {return name;}
	
	@Override
	public String toString() 
	{
		return "Venue [id=" + id + ", active=" + active + ", maxCapacity=" + maxCapacity + ", name=" + name
				+ ", officeId=" + officeId + ", createdDate=" + createdDate + ", lastModifiedDate=" + lastModifiedDate
				+ "]";
	}
	
	public String generateSQLInsertStatement() 
	{
		return "INSERT INTO Venues "
    			+ "(Id, Active, MaxCapacity, Name, OfficeId, CreatedDate, LastModifiedDate) "
				+ "VALUES ("
				+ id + ", " + active + ", " + maxCapacity + ", '" + name + "', " + officeId + ", '"
				+ createdDate + "', '" + lastModifiedDate + "')";
	}

	
	
}
