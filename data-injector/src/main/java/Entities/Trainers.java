package Entities;

public class Trainers implements DatabaseElement{
	
	//Properties
	private int id;
	private int active;
	private String email;
	private String firstName;
	private int isCoreTrainer;
	private int isTutor;
	private String lastName;
	private int officeId;
	private String photo;
	private String teamName;
	private String userName;
	private int regionId;
	private String createdDate;
	private String lastModifiedDate;
	
	
	//constructor
	public Trainers(int id, int active, String email, String firstName, int isCoreTrainer, int isTutor, String lastName,
			int officeId, String photo, String teamName, String userName, int regionId, String createdDate,
			String lastModifiedDate) {
		super();
		this.id = id;
		this.active = active;
		this.email = email;
		this.firstName = firstName;
		this.isCoreTrainer = isCoreTrainer;
		this.isTutor = isTutor;
		this.lastName = lastName;
		this.officeId = officeId;
		this.photo = photo;
		this.teamName = teamName;
		this.userName = userName;
		this.regionId = regionId;
		this.createdDate = createdDate;
		this.lastModifiedDate = lastModifiedDate;
	}
	
	
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Trainers "
	+ "(Id, Active, Email, FirstName, IsCoreTrainer, IsTutor, LastName, OfficeId, Photo, TeamName, UserName, RegionId, CreatedDate, LastModifiedDate) "
	+ "VALUES ("
	+ id + ", " + active + ", '" + email + "', '" + firstName + "', " + isCoreTrainer + ", "
	+ isTutor + ", '" + lastName + "' , " +officeId+", '"+ photo + "', '"+ teamName +"', '"+ userName +"', "
	+regionId+", '"+ createdDate + "', '"+lastModifiedDate+"')";
	}


	

}
