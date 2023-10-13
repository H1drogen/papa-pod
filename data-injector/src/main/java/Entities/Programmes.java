package Entities;

public class Programmes implements DatabaseElement {
	
	//Properties
	private int id;
	private String abbreviation;
	private String description;
	private String name;
	private String createdDate;
	private String lastModifiedDate;
	
	//Constructor
	public Programmes(int id, String abbreviation, String description, String name, String createdDate,
			String lastModifiedDate) {
		super();
		this.id = id;
		this.abbreviation = abbreviation;
		this.description = description;
		this.name = name;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Programmes "
	+ "(Id,Abbreviation,Description,Name,CreatedDate,LastModifiedDate) "
	+ "VALUES ("
	+ id +  ", '" + abbreviation + "', '" + description + "', '" + name + "', '" + createdDate + "', '"
	+ lastModifiedDate + "')";
	}

}
