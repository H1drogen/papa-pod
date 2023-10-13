																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																				package Entities;

public class CourseTemplates implements DatabaseElement {

	// Properties
	private int id;
	private String createdBy;
	private String description;
	private String duration;
	private int isExtra;
	private String name;
	private int pathwayTemplateId;
	private String preparationNotes;
	private int sequence;
	private int regionId;
	private String createdDate;
	private String lastModifiedDate;

	

	// Constructor
	public CourseTemplates(int id, String createdBy, String description, String duration, int isExtra, String name,
			int pathwayTemplateId, String preparationNotes, int sequence, int regionId, String createdDate,
			String lastModifiedDate) {
		super();
		this.id = id;
		this.createdBy = createdBy;
		this.description = description;
		this.duration = duration;
		this.isExtra = isExtra;
		this.name = name;
		this.pathwayTemplateId = pathwayTemplateId;
		this.preparationNotes = preparationNotes;
		this.sequence = sequence;
		this.regionId = regionId;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}

	// SQL Insert statement
	public String generateSQLInsertStatement() {
		return "INSERT INTO CourseTemplates "
				+ "(Id, CreatedBy, Description, Duration, IsExtra, Name, PathwayTemplateId, PreparationNotes, Sequence, RegionId, CreatedDate, LastModifiedDate) "
				+ "VALUES (" + id + ", '" + createdBy + "', '" + description + "', '" + duration + "', " + isExtra
				+ ", '" + name + "', "+ pathwayTemplateId +", '"+ preparationNotes + "', " + sequence + ", " + regionId + ", '" + createdDate + "', '"
				+ lastModifiedDate + "')";
	}



	

}
