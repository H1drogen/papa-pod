package Injector;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashSet;

import Entities.Country;
import Entities.CourseTemplates;
import Entities.Courses;
import Entities.Holidays;
import Entities.Offices;
import Entities.PathwayTemplates;
import Entities.PathwayTypes;
import Entities.Pathways;
import Entities.Programmes;
import Entities.Regions;
import Entities.TrainerRoles;
import Entities.Trainers;
import Entities.Venue;
import FilePaths.FilePathLibrary;
import Retriever.DataRetrievalFactory;

public class DatabaseInjectorFactory {
	// Deletes all records from the table passed as argument
	public static void deleteFromTable(String table) {
		try {
			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			// Define the query to delete from table
			String deleteStatement = "DELETE FROM " + table;
			statement.executeUpdate(deleteStatement);

			// Closes connection to the database
			statement.close();
			connection.close();

			System.out.println("  Data deleted successfully from " + table);
			System.out.println("");

		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	/*
	 * Adds the number of records passed as an argument, specifically to Holidays
	 * Since each table has different attributes and therefore different ways to
	 * generate the dummy data, separate methods will be necessary for each table
	 */
	public static void addToHolidays() {
		try {
			// remove any data currently in the database
			deleteFromTable("Holidays");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Holidays> holidaysList = DataRetrievalFactory.holidayList(FilePathLibrary.getHolidaysPath());

			// Loop adds as many records as the amount passed as an argument

			for (Holidays holiday : holidaysList) {
				statement.executeUpdate(holiday.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Holidays");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/*
	 * Adds the number of records passed as an argument, specifically to Countries
	 * Since each table has different attributes and therefore different ways to
	 * generate the dummy data, separate methods will be necessary for each table
	 */
	public static void addToCountries() {
		try {
			// remove any data currently in the database
			deleteFromTable("Countries");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Country> countryList = DataRetrievalFactory.countryList(FilePathLibrary.getCountriesPath());
			// Loop adds as many records as the amount passed as an argument

			for (Country country : countryList) {
				statement.executeUpdate(country.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Countries");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/* Adds all the data in the venues.csv file to the database 8 */
	public static void addToVenues() {
		try {
			// remove any data currently in the database
			deleteFromTable("Venues");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Venue> venueList = DataRetrievalFactory.venueList(FilePathLibrary.getVenuesPath());

			HashSet<String> uniqueNameCheck = new HashSet<String>();
			// Loop adds as many records as the amount passed as an argument

			for (Venue venue : venueList) {
				if (!uniqueNameCheck.contains(venue.getName())) {
					statement.executeUpdate(venue.generateSQLInsertStatement());
					uniqueNameCheck.add(venue.getName());
				}
			}

		
			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Venues");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToOffices() {
		try {
			// remove any data currently in the database
			deleteFromTable("Offices");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Offices> officesList = DataRetrievalFactory.officesList(FilePathLibrary.getOfficesPath());

			HashSet<String> uniqueNameCheck = new HashSet<String>();

			// Loop adds as many records as the amount passed as an argument
			for (Offices item : officesList) {
				if (!uniqueNameCheck.contains(item.getName())) {
					statement.executeUpdate(item.generateSQLInsertStatement());
					uniqueNameCheck.add(item.getName());
					}
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Offices");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToProgrammes() {
		try {
			// remove any data currently in the database
			deleteFromTable("Programmes");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Programmes> programmesList = DataRetrievalFactory
					.programmesList(FilePathLibrary.getProgrammesPath());

			// Loop adds as many records as the amount passed as an argument
			for (Programmes item : programmesList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Programmes");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToTrainers() {
		try {
			// remove any data currently in the database
			deleteFromTable("Trainers");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Trainers> trainersList = DataRetrievalFactory.trainersList(FilePathLibrary.getTrainersPath());

			// Loop adds as many records as the amount passed as an argument
			for (Trainers item : trainersList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Trainers");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToCourses() {
		try {
			// remove any data currently in the database
			deleteFromTable("Courses");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Courses> courseList = DataRetrievalFactory.coursesList(FilePathLibrary.getCoursesPath());

			// Loop adds as many records as the amount passed as an argument
			for (Courses item : courseList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Courses");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToPathways() {
		try {
			// remove any data currently in the database
			deleteFromTable("Pathways");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Pathways> pathwaysList = DataRetrievalFactory.pathwaysList(FilePathLibrary.getPathwaysPath());

			// Loop adds as many records as the amount passed as an argument
			for (Pathways item : pathwaysList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Pathways");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToRegions() {
		try {
			// remove any data currently in the database
			deleteFromTable("Regions");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<Regions> regionsList = DataRetrievalFactory.regionsList(FilePathLibrary.getRegionsPath());

			// Loop adds as many records as the amount passed as an argument
			for (Regions item : regionsList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}
		
			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to Regions");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToTrainerRoles() {
		try {
			// remove any data currently in the database
			deleteFromTable("TrainerRoles");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<TrainerRoles> trainerRolesList = DataRetrievalFactory
					.trainerRolesList(FilePathLibrary.getTrainerRolesPath());

			// Loop adds as many records as the amount passed as an argument
			for (TrainerRoles item : trainerRolesList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to TrainerRoles");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToPathwayTypes() {
		try {
			// remove any data currently in the database
			deleteFromTable("PathwayTypes");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<PathwayTypes> pathwayTypesList = DataRetrievalFactory
					.pathwayTypesList(FilePathLibrary.getPathwayTypesPath());

			// Loop adds as many records as the amount passed as an argument
			for (PathwayTypes item : pathwayTypesList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to PathwayTypes");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToPathwayTemplates() {
		try {
			// remove any data currently in the database
			deleteFromTable("PathwayTemplates");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<PathwayTemplates> pathwayTemplatesList = DataRetrievalFactory
					.pathwayTemplatesList(FilePathLibrary.getPathwayTemplatesPath());

			// Loop adds as many records as the amount passed as an argument
			for (PathwayTemplates item : pathwayTemplatesList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to PathwayTemplates");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void addToCourseTemplates() {
		try {
			// remove any data currently in the database
			deleteFromTable("CourseTemplates");

			// Establish a connection to the SQLite database
			Connection connection = DriverManager.getConnection("jdbc:sqlite:" + FilePathLibrary.getDbPath());

			// Create a statement object
			java.sql.Statement statement = connection.createStatement();

			ArrayList<CourseTemplates> courseTemplatesList = DataRetrievalFactory
					.courseTemplatesList(FilePathLibrary.getCoursesTemplatePath());

			// Loop adds as many records as the amount passed as an argument
			for (CourseTemplates item : courseTemplatesList) {
				statement.executeUpdate(item.generateSQLInsertStatement());
			}

			// Close statement and connection
			statement.close();
			connection.close();

			System.out.println("Data have been successfully added to CourseTemplates");
			System.out.println("");

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}

// ########### MARIO'S OLD COLD WHICH USES FAKER TO GENERATE A SET OF DATA TO INPUT INTO THE DATABASE. THE CURRENT FORMAT INPUTS DATA FROM THE CSV FILES ###########
// ########### HOWEVER THIS CODE COULD BE USEFUL IN THE EVENT THAT WE CHANGE THE FUNCTIONALITY BACK TO USING FAKER TO GENERATE DATA.                     ###########

/*
 * try {
 * 
 * //Faker
 * 
 * Faker faker = new Faker();
 * 
 * // Replace with your own path to the SQLite file or the local host address
 * 
 * 
 * // Establish a connection to the SQLite database
 * 
 * Connection connection = DriverManager.getConnection("jdbc:sqlite:" +
 * FilePathLibrary.getDbPath());
 * 
 * // Create a statement object
 * 
 * java.sql.Statement statement = connection.createStatement();
 * 
 * // Id starts at 1 if table has no records
 * 
 * int id = 1;
 * 
 * // Define the query to retrieve the count of current records in the table
 * 
 * String countryQuery = "SELECT COUNT(*) FROM Countries";
 * 
 * // Execute the query
 * 
 * ResultSet countryResultSet = statement.executeQuery(countryQuery);
 * 
 * // Retrieve the result and make id start from recordCount + 1
 * 
 * if (countryResultSet.next()) { int countryCount = countryResultSet.getInt(1);
 * id += countryCount; }
 * 
 * countryResultSet.close();
 * 
 * // Loop adds as many records as the amount passed as an argument
 * 
 * for (int i = 0; i < records; i++) {
 * 
 * // RegionId selects randomly from current regionIds in the same way as above
 * 
 * String regionQuery = "SELECT COUNT(*) FROM Regions"; ResultSet
 * regionResultSet = statement.executeQuery(regionQuery); int regionCount =
 * regionResultSet.getInt(1);
 * 
 * int regionId = ThreadLocalRandom.current().nextInt(1, regionCount + 1);
 * 
 * regionResultSet.close();
 * 
 * // IsActive boolean as a random 0 or 1
 * 
 * int isActive = ThreadLocalRandom.current().nextInt(0, 2);
 * 
 * // Name using Faker
 * 
 * String name = faker.country().name();
 * 
 * // Dates use current date and are formatted as strings
 * 
 * LocalDateTime created = LocalDateTime.now(); LocalDateTime lastModified =
 * LocalDateTime.now();
 * 
 * DateTimeFormatter formatter =
 * DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
 * 
 * String createdDate = created.format(formatter); String lastModifiedDate =
 * lastModified.format(formatter);
 * 
 * // Execute SQL statements to insert dummy data
 * 
 * String insertStatement = "INSERT INTO Countries " +
 * "(Id, IsActive, Name, RegionId, CreatedDate, LastModifiedDate) " + "VALUES ("
 * + id + ", " + isActive + ", '" + name + "', " + regionId + ", '" +
 * createdDate + "', '" + lastModifiedDate + "')";
 * statement.executeUpdate(insertStatement);
 * 
 * id += 1; }
 * 
 * // Close statement and connection
 * 
 * statement.close(); connection.close();
 * 
 * System.out.println("  " + records +
 * " records have been successfully added to Countries");
 * System.out.println("");
 * 
 * } catch (Exception e) { e.printStackTrace(); }
 */