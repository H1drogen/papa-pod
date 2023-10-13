package Entities;

public class PathwayTypes implements DatabaseElement {
	//Properties
	private int id;
	private String abbreviation;
	private String colour;
	private int isConcludingPathway;
	private String lmsAccessGroup;
	private String name;
	private String createdDate;
	private String lastModifiedDate;
	
	
	//Constructor
	public PathwayTypes(int id, String abbreviation, String colour, int isConcludingPathway, String lmsAccessGroup,
			String name, String createdDate, String lastModifiedDate) {
		super();
		this.id = id;
		this.abbreviation = abbreviation;
		this.colour = colour;
		this.isConcludingPathway = isConcludingPathway;
		this.lmsAccessGroup = lmsAccessGroup;
		this.name = name;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO PathwayTypes "
	+ "(Id, Abbreviation, Colour, IsConcludingPathway, LMSAccessGroup, Name, CreatedDate, LastModifiedDate) "
	+ "VALUES ("
	+ id + ", '" + abbreviation + "', '" + colour + "', " + isConcludingPathway + ", '" + lmsAccessGroup 
	+ "', '"+name + "', '" + createdDate + "', '" + lastModifiedDate + "')";
	}
	
	

}
