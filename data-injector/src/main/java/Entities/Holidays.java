package Entities;

public class Holidays implements DatabaseElement
{
	//properties
	private int id;
	private int countryId;
	private String description;
	private String endDate;
	private String name;
	private String startDate;
	private String createdDate;
	private String lastModifiedDate;
	
	//constructor
	public Holidays(int id, int countryId, String description, String endDate, String name, String startDate,
			String createdDate, String lastModifiedDate) {
		super();
		this.id = id;
		this.countryId = countryId;
		this.description = description;
		this.endDate = endDate;
		this.name = name;
		this.startDate = startDate;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
		return "INSERT INTO Holidays "
    			+ "(Id, CountryId, Description, EndDate, Name, StartDate, CreatedDate, LastModifiedDate) "
				+ "VALUES ("
				+ id + ", " + countryId + ", '" + description + "', '" + endDate + "', '" + name + "', '"
				+ startDate + "', '" + createdDate + "', '" + lastModifiedDate + "')";
	}
}


