package Interface;

import java.sql.SQLException;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

import Injector.DatabaseInjectorFactory;

public class Menu {
	private static Scanner scanner;
	private static List<Integer> menuOptions;
	private static List<Integer> submenuOptions;

	// ########### OPENING PAGE ##################
	public static void Interface() throws SQLException, ClassNotFoundException {
		// Loading the SQLite JDBC driver
		Class.forName("org.sqlite.JDBC");

		// Initialising scanner
		scanner = new Scanner(System.in);

		// Controlling the main menu loop
		boolean cont = true;

		// Displaying header
		displayHeader();

		// Main menu loop

		while (cont) {
			// Displaying menu
			displayMainMenu();

			// Getting user input for table selection
			menuOptions = Arrays.asList(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);
			submenuOptions = Arrays.asList(0, 1, 2);
			int option = scanner.nextInt();

			// Controlling using input for table selection
			while (!menuOptions.contains(option)) {
				System.out.println("  Please enter a valid option:");
				option = scanner.nextInt();
			}

			// Main menu switch - cases to be added for further table methods
			switch (option) {

			// Exit selected
			case 0:
				System.out.println("  Goodbye! Thank you for using ACADEMY RPT DATA INJECTOR v1.!");
				cont = false;
				break;

			// Holidays selected
			case 1:
				System.out.println("  You have selected HOLIDAYS");
				selectedHolidays();
				break;

			// Countries selected
			case 2:
				System.out.println("  You have selected COUNTRIES");
				selectedCountries();
				break;

			// Venues selected
			case 3:
				System.out.println("  You have selected VENUES");
				selectedVenues();
				break;

			// Offices selected
			case 4:
				System.out.println(" You have selected OFFICES");
				selectedOffices();
				break;

			// Programmes selected
			case 5:
				System.out.println("You have selected PROGRAMMES");
				selectedProgrammes();
				break;

			// Trainers selected
			case 6:
				System.out.println("You have selected TRAINERS");
				selectedTrainers();
				break;

			// Courses selected
			case 7:
				System.out.println(" You have selected COURSES");
				selectedCourses();
				break;

			// Pathways selected
			case 8:
				System.out.println(" You have selected PATHWAYS");
				selectedPathways();
				break;

			// Regions selected
			case 9:
				System.out.println(" You have selected REGIONS");
				selectedRegions();
				break;

			// TrainerRoles selected
			case 10:
				System.out.println(" You have selected TrainerRoles");
				selectedTrainerRoles();
				break;

			// PathwayTypes selected
			case 11:
				System.out.println(" You have selected PathwayTypes");
				selectedPathwayTypes();
				break;

			// PathwayTemplates selected
			case 12:
				System.out.println(" You have selected PathwayTemplates");
				selectedPathwayTemplates();
				break;

			// CourseTemplates selected
			case 13:
				System.out.println(" You have selected CourseTemplates");
				selectedCourseTemplates();
				break;
				
			// CourseTemplates selected
			case 14:
				System.out.println(" You have selected All");
				selectedAll();
				break;
			}
		}
		scanner.close();

	}

	// ############ UI ###############
	public static void displayHeader() {
		System.out.println("  ==================================================");
		System.out.println(" |                                                  |");
		System.out.println(" |           ACADEMY RPT DATA INJECTOR v1.          |");
		System.out.println(" |                                                  |");
		System.out.println("  ==================================================");
		System.out.println("");
	}

	public static void displayMainMenu() {
		System.out.println("  ==================================================");
		System.out.println(" |                                                  |");
		System.out.println(" |                       MENU                       |");
		System.out.println(" |                                                  |");
		System.out.println(" |              ----------------------              |");
		System.out.println(" |             |                      |             |");
		System.out.println(" |             |    1 - Holidays      |             |");
		System.out.println(" |             |    2 - Countries     |             |");
		System.out.println(" |             |    3 - Venues        |             |");
		System.out.println(" |             |    4 - Offices       |             |");
		System.out.println(" |             |    5 - Programmes    |             |");
		System.out.println(" |             |    6 - Trainers      |             |");
		System.out.println(" |             |    7 - Courses       |             |");
		System.out.println(" |             |    8 - Pathways      |             |");
		System.out.println(" |             |    9 - Regions       |             |");
		System.out.println(" |             |   10 - TrainerRoles  |             |");
		System.out.println(" |             |   11 - PathwayTypes  |             |");
		System.out.println(" |             | 12 - PathwayTemplates|             |");
		System.out.println(" |             | 13 - CourseTemplates |             |");
		System.out.println(" |             |   14 - All           |             |");
		System.out.println(" |             |                      |             |");
		System.out.println(" |              ----------------------              |");
		System.out.println(" |                                                  |");
		System.out.println(" |              Please, enter the number            |");
		System.out.println(" |          of the table you wish to modify         |");
		System.out.println(" |                                                  |");
		System.out.println(" |                        OR                        |");
		System.out.println(" |                                                  |");
		System.out.println(" |                  Enter 0 to Exit                 |");
		System.out.println("  ==================================================");
		System.out.println("");
	}

	public static void displaySecondaryMenu() {
		System.out.println("  ==================================================");
		System.out.println(" |                                                  |");
		System.out.println(" |              ----------------------              |");
		System.out.println(" |             |                      |             |");
		System.out.println(" |             |  1 - Add records     |             |");
		System.out.println(" |             |  2 - Delete records  |             |");
		System.out.println(" |             |                      |             |");
		System.out.println(" |              ----------------------              |");
		System.out.println(" |                                                  |");
		System.out.println(" |              Please, select an option            |");
		System.out.println(" |                                                  |");
		System.out.println(" |                        OR                        |");
		System.out.println(" |                                                  |");
		System.out.println(" |             Enter 0 to go back to Menu           |");
		System.out.println("  ==================================================");
		System.out.println("");
	}

	// ############ SUB-PAGES FOR EACH SELECTED OPTION ##############
	public static void selectedHolidays() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu

		int optionHoliday = scanner.nextInt();
		while (!submenuOptions.contains(optionHoliday)) {
			System.out.println("  Please enter a valid option:");
			optionHoliday = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionHoliday) {

		// Back to main menu selected
		case 0:
			System.out.println("  You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("current directory=" + System.getProperty("user.dir"));
			DatabaseInjectorFactory.addToHolidays();
			break;

		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Holidays");
			break;

		}
	}

	public static void selectedCountries() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionCountry = scanner.nextInt();
		while (!submenuOptions.contains(optionCountry)) {
			System.out.println("  Please enter a valid option:");
			optionCountry = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionCountry) {

		// Back to main menu selected
		case 0:
			System.out.println("  You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			DatabaseInjectorFactory.addToCountries();
			break;

		// Delete records selected

		case 2:
			DatabaseInjectorFactory.deleteFromTable("Countries");
			break;
		}
	}

	public static void selectedVenues() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionVenue = scanner.nextInt();
		while (!submenuOptions.contains(optionVenue)) {
			System.out.println("  Please enter a valid option:");
			optionVenue = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionVenue) {

		// Back to main menu selected
		case 0:
			System.out.println("  You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to venues");
			DatabaseInjectorFactory.addToVenues();
			break;

		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Venues");
			break;
		}
	}

	public static void selectedOffices() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionOffices = scanner.nextInt();
		while (!submenuOptions.contains(optionOffices)) {
			System.out.println(" Please enter a valid option:");
			optionOffices = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionOffices) {

		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Offices ");
			DatabaseInjectorFactory.addToOffices();
			break;

		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Offices");
			break;
		}
	}

	public static void selectedProgrammes() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionProgrammes = scanner.nextInt();
		while (!submenuOptions.contains(optionProgrammes)) {
			System.out.println(" Please enter a valid option:");
			optionProgrammes = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionProgrammes) {

		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Programmes ");
			DatabaseInjectorFactory.addToProgrammes();
			break;

		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Programmes");
			break;
		}
	}

	public static void selectedTrainers() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionTrainers = scanner.nextInt();
		while (!submenuOptions.contains(optionTrainers)) {
			System.out.println(" Please enter a valid option:");
			optionTrainers = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionTrainers) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Trainers ");
			DatabaseInjectorFactory.addToTrainers();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Trainers");
			break;
		}
	}

	public static void selectedCourses() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionCourses = scanner.nextInt();
		while (!submenuOptions.contains(optionCourses)) {
			System.out.println(" Please enter a valid option:");
			optionCourses = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionCourses) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Courses ");
			DatabaseInjectorFactory.addToCourses();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Courses");
			break;
		}
	}

	public static void selectedPathways() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionPathways = scanner.nextInt();
		while (!submenuOptions.contains(optionPathways)) {
			System.out.println(" Please enter a valid option:");
			optionPathways = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionPathways) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Pathways ");
			DatabaseInjectorFactory.addToPathways();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Pathways");
			break;
		}
	}

	public static void selectedRegions() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionRegions = scanner.nextInt();
		while (!submenuOptions.contains(optionRegions)) {
			System.out.println(" Please enter a valid option:");
			optionRegions = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionRegions) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to Regions ");
			DatabaseInjectorFactory.addToRegions();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Regions");
			break;
		}
	}

	public static void selectedTrainerRoles() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionTrainerRoles = scanner.nextInt();
		while (!submenuOptions.contains(optionTrainerRoles)) {
			System.out.println(" Please enter a valid option:");
			optionTrainerRoles = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionTrainerRoles) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to TrainerRoles ");
			DatabaseInjectorFactory.addToTrainerRoles();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("TrainerRoles");
			break;
		}
	}

	public static void selectedPathwayTypes() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionPathwayTypes = scanner.nextInt();
		while (!submenuOptions.contains(optionPathwayTypes)) {
			System.out.println(" Please enter a valid option:");
			optionPathwayTypes = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionPathwayTypes) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to PathwayTypes ");
			DatabaseInjectorFactory.addToPathwayTypes();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("PathwayTypes");
			break;
		}
	}

	public static void selectedPathwayTemplates() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionPathwayTemplates = scanner.nextInt();
		while (!submenuOptions.contains(optionPathwayTemplates)) {
			System.out.println(" Please enter a valid option:");
			optionPathwayTemplates = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionPathwayTemplates) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to PathwayTemplates ");
			DatabaseInjectorFactory.addToPathwayTemplates();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("PathwayTemplates");
			break;
		}
	}

	public static void selectedCourseTemplates() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionCourseTemplates = scanner.nextInt();
		while (!submenuOptions.contains(optionCourseTemplates)) {
			System.out.println(" Please enter a valid option:");
			optionCourseTemplates = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionCourseTemplates) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding csv files to CourseTemplates ");
			DatabaseInjectorFactory.addToCourseTemplates();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("CourseTemplates");
			break;
		}
	}
	
	public static void selectedAll() {
		displaySecondaryMenu();

		// Controlling user input for secondary menu
		int optionCourseTemplates = scanner.nextInt();
		while (!submenuOptions.contains(optionCourseTemplates)) {
			System.out.println(" Please enter a valid option:");
			optionCourseTemplates = scanner.nextInt();
		}

		// Secondary menu switch - cases to be added if further features are included
		switch (optionCourseTemplates) {
		// Back to main menu selected
		case 0:
			System.out.println(" You have selected MAIN MENU");
			break;

		// Add records selected
		case 1:
			System.out.println("Adding all csv Files ");
			DatabaseInjectorFactory.addToHolidays();
			DatabaseInjectorFactory.addToCountries();
			DatabaseInjectorFactory.addToVenues();
			DatabaseInjectorFactory.addToOffices();
			DatabaseInjectorFactory.addToProgrammes();
			DatabaseInjectorFactory.addToTrainers();
			DatabaseInjectorFactory.addToCourses();
			DatabaseInjectorFactory.addToPathways();
			DatabaseInjectorFactory.addToRegions();
			DatabaseInjectorFactory.addToTrainerRoles();
			DatabaseInjectorFactory.addToPathwayTypes();
			DatabaseInjectorFactory.addToPathwayTemplates();
			DatabaseInjectorFactory.addToCourseTemplates();
			break;
		// Delete records selected
		case 2:
			DatabaseInjectorFactory.deleteFromTable("Holidays");
			DatabaseInjectorFactory.deleteFromTable("Countries");
			DatabaseInjectorFactory.deleteFromTable("Venue");
			DatabaseInjectorFactory.deleteFromTable("Offices");
			DatabaseInjectorFactory.deleteFromTable("Programmes");
			DatabaseInjectorFactory.deleteFromTable("Trainers");
			DatabaseInjectorFactory.deleteFromTable("Courses");
			DatabaseInjectorFactory.deleteFromTable("Pathways");
			DatabaseInjectorFactory.deleteFromTable("Regions");
			DatabaseInjectorFactory.deleteFromTable("TrainerRoles");
			DatabaseInjectorFactory.deleteFromTable("PathwayTypes");
			DatabaseInjectorFactory.deleteFromTable("PathwayTemplates");
			DatabaseInjectorFactory.deleteFromTable("CourseTemplates");
			break;
		}
	}

}
