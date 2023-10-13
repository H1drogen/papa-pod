package Entities;

public class TrainerRoles implements DatabaseElement{
	
	//properties
	private int id;
	private String name;
	private String description;
	private String createdDate;
	private String lastModifiedDate;
	
	//Constructor
	public TrainerRoles(int id, String name, String description, String createdDate, String lastModifiedDate) {
		super();
		this.id = id;
		this.name = name;
		this.description = description;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO TrainerRoles "
	+ "(Id, Name, Description, CreatedDate, LastModifiedDate) "
	+ "VALUES ("
	+ id + ", '" + name + "', '" + description + "', '" + createdDate + "', '" 
	+ lastModifiedDate + "')";
	}

	
	

}
