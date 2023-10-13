package Entities;

public class Pathways implements DatabaseElement{
	
	//Properties
	private int id;
	private int cancelled;
	private int isPond;
	private String pathwayCode;
	private int maxCapacity;
	private String createdBy;
	private String endDate;
	private String startDate;
	private String createdDate;
	private String lastModifiedDate;
	private int programmeId;
	private int pathwayTypeId;
	private int regionId;
	
	
	//Constructor
	public Pathways(int id, int cancelled, int isPond, String pathwayCode, int maxCapacity, String createdBy,
			String endDate, String startDate, String createdDate, String lastModifiedDate, int programmeId,
			int pathwayTypeId, int regionId) {
		super();
		this.id = id;
		this.cancelled = cancelled;
		this.isPond = isPond;
		this.pathwayCode = pathwayCode;
		this.maxCapacity = maxCapacity;
		this.createdBy = createdBy;
		this.endDate = endDate;
		this.startDate = startDate;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
		this.programmeId = programmeId;
		this.pathwayTypeId = pathwayTypeId;
		this.regionId = regionId;
	}
	
	
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Pathways "
	+ "(Id, Cancelled, IsPond, PathwayCode, MaxCapacity, CreatedBy, EndDate, StartDate, CreatedDate, LastModifiedDate, ProgrammeId, PathwayTypeId, RegionId) "
	+ "VALUES ("
	+ id + ", "+ cancelled+", "+isPond+", '"+pathwayCode+"', "+maxCapacity+", '"+createdBy+"', '"+endDate
	+"', '"+startDate+"', '"+createdDate+"', '"+lastModifiedDate+"', "+programmeId+", "+ pathwayTypeId+", "
	+regionId+")";
	}


	
	

}
