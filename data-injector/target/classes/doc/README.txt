

     ACADEMY RPT DATA INJECTOR

     Author: Mario Lugilde

     Version: 1.0

     Date: 26/05/2023



     * SUMMARY:

			Academy RPT Data Injector is a simple Java program that automates and partially randomises
		injection of data in an SQLite database file, specifically designed for the Academy RPT project.
			It allows adding and deleting records from the existing database tables, for testing purposes
		within the project development phase.
	
	
     * MAIN FEATURES:

		- The developer can SELECT A TABLE to modify
		
		- The developer can ADD records to the selected table
		
		- The developer can SPECIFY THE NUMBER OF RECORDS to be added to the table
		
		- The developer can DELETE all records from the specified table
		
		- A separate ClearTable class has been included for convenience while testing. It contains
		  the deleteFromTable(String table) method to make it easier for the developer to clear 
		  table records that have been added without having to run the main program and go through
		  the menus
	
	
     * DEPENDENCIES:

		(To be added to the pom.xml file in a new project)
	
		- SQLite driver :
		
			<dependency>
			    <groupId>org.xerial</groupId>
			    <artifactId>sqlite-jdbc</artifactId>
			    <version>3.42.0.0</version>
			</dependency>
			
		- DataFaker:
			
			<dependency>
				<groupId>net.datafaker</groupId>
				<artifactId>datafaker</artifactId>
				<version>1.6.0</version>
			</dependency>
			
		Note: The above dependencies have already been provided with this package
			
			
     * FUTURE IMPROVEMENTS:

		- The main menu and submenus have been designed as switch statements, so further cases 
		  can be added as more methods are created to add records to different tables
		  
		  Note: At the moment, only methods for the Holidays and Countries tables have been implemented
		  
		- The deleteFromTable method could be expanded to allow for the specification of the number
		  of records to be deleted
		  
		- Current and future addToTable methods could read data from files written
		  in Python (already in development), which allows for better automation and randomisation
		  
		- The country name in the addToCountriesMethod could account for the region id
		
		  Note: At the moment, a random country name is generated since a way to constrain it to a
		        a region hasn't been found yet
		  
		- Most relevant exception handling has been implemented in order to keep the program from crashing,
		  but further improvements could be made to account for database server issues.
		  
		  Note: The database path has been hardcoded to a local path. This would have to be changed
		        once the database is hosted in a server and goes live

	
	