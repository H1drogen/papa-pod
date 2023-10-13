package Entities;

public class Regions implements DatabaseElement{
	
	//Properties
	private int id;
	private String name;
	private String abbreviation;
	private int isActive;
	private String createdDate;
	private String lastModifiedDate;
	
	
	//Constructor
	public Regions(int id, String name, String abbreviation, int isActive, String createdDate,
			String lastModifiedDate) {
		super();
		this.id = id;
		this.name = name;
		this.abbreviation = abbreviation;
		this.isActive = isActive;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Regions "
	+ "(Id, Name, Abbreviation, IsActive, CreatedDate, LastModifiedDate) "
	+ "VALUES ("
	+ id + ", '"+name+"', '" + abbreviation + "', " + isActive + ", '" + createdDate + "', '"
	+ lastModifiedDate + "')";
	}


	

}
