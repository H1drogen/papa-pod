package Entities;

public class Offices implements DatabaseElement {
	
	//properties - matching ERD Diagram
		private int id;
		private String abbreviation;
		private int countryId;
		private int isActive;
		private int isPopUp;
		private String name;
		private String createdDate;
		private String lastModifiedDate;
		

	//Constructor
		public Offices(int id, String abbreviation, int countryId, int isActive, int isPopUp, String name,
				String createdDate, String lastModifiedDate) {
			super();
			this.id = id;
			this.abbreviation = abbreviation;
			this.countryId = countryId;
			this.isActive = isActive;
			this.isPopUp = isPopUp;
			this.name = name;
			this.createdDate = createdDate;
			this.lastModifiedDate = lastModifiedDate;
		}	
	
		public String getName() {return name;}
		
	//SQL Insert statement
	public String generateSQLInsertStatement() 
	{
	return "INSERT INTO Offices "
			+ "(Id,Abbreviation, IsActive, IsPopUp, Name, CreatedDate, LastModifiedDate, CountryId) "
			+ "VALUES ("
			+ id +", '"+abbreviation+"', "+isActive+", "+isPopUp+", '"+name+"', '"
			+createdDate+"', '"+lastModifiedDate+"', "+countryId + ")";
	}


	
	
}
