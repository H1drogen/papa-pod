package Entities;

public class Courses implements DatabaseElement{
	
	//Properties
	private int id;
	private String name;
	private String description;
	private String preparationNotes;
	private int provisional;
	private String startDate;
	private String endDate;
	private String createdDate;
	private String lastModifiedDate;
	private int pathwayId;
	private int venueId;
	private int regionId;
	private int virtual;
	
	//Constructor
	public Courses(int id, String name, String description, String preparationNotes, int provisional, String startDate,
			String endDate, String createdDate, String lastModifiedDate, int pathwayId, int venueId, int regionId) {
		super();
		this.id = id;
		this.name = name;
		this.description = description;
		this.preparationNotes = preparationNotes;
		this.provisional = provisional;
		this.startDate = startDate;
		this.endDate = endDate;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
		this.pathwayId = pathwayId;
		this.venueId = venueId;
		this.regionId = regionId;
		this.virtual = 0;
	}
	
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Courses "
	+ "(Id,Name, Description, PreparationNotes, Provisional, StartDate, EndDate, CreatedDate, LastModifiedDate, PathwayId, VenueId, RegionId, Virtual) "
	+ "VALUES ("
	+ id + ", '" + name+"', '"+ description +"', '"+ preparationNotes +"', "+provisional+", '"+ startDate
	+"', '"+endDate+"', '"+createdDate+"', '"+lastModifiedDate+"', "+pathwayId+", "+venueId+", "+regionId+", "+virtual+")";
	}

	

}
