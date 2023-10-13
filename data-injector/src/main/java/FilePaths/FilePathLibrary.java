package FilePaths;

public class FilePathLibrary 
{
	//The file path of the database.sqlite file in the Resource planning tool
	private static String dbPath = "..\\API\\Fdm.WebAPI\\Database.sqlite";
	
	//The file path of the directory that holds all the .csv files
	private static String DirectoryPath = "..\\Mock Data Csv\\";
	
	private static String CountriesPath = DirectoryPath + "Countries.csv";
	private static String CoursesTemplatePath = DirectoryPath + "Course Templates.csv";
	private static String CoursesPath = DirectoryPath + "Courses.csv";
	private static String HolidaysPath = DirectoryPath + "Holidays.csv";
	private static String OfficesPath = DirectoryPath + "Offices.csv";
	private static String PathwayTemplatesPath = DirectoryPath + "Pathway Templates.csv";
	private static String PathwayTypesPath = DirectoryPath + "Pathway Types.csv";
	private static String PathwaysPath = DirectoryPath + "Pathways.csv";
	private static String ProgrammesPath = DirectoryPath + "Programmes.csv";
	private static String RegionsPath = DirectoryPath + "Regions.csv";
	private static String TrainerCoursesPath = DirectoryPath + "Trainer Courses.csv";
	private static String TrainerRolesPath = DirectoryPath + "Trainer Roles.csv";
	private static String TrainersPath = DirectoryPath + "Trainers.csv";
	private static String VenuesPath = DirectoryPath + "Venues.csv";
	
	public static String getDbPath() {return dbPath;}
	public static String getDirectoryPath() {return DirectoryPath;}
	public static String getCountriesPath() {return CountriesPath;}
	public static String getCoursesTemplatePath() {return CoursesTemplatePath;}
	public static String getCoursesPath() {return CoursesPath;}
	public static String getHolidaysPath() {return HolidaysPath;}
	public static String getOfficesPath() {return OfficesPath;}
	public static String getPathwayTemplatesPath() {return PathwayTemplatesPath;}
	public static String getPathwayTypesPath() {return PathwayTypesPath;}
	public static String getPathwaysPath() {return PathwaysPath;}
	public static String getProgrammesPath() {return ProgrammesPath;}
	public static String getRegionsPath() {return RegionsPath;}
	public static String getTrainerCoursesPath() {return TrainerCoursesPath;}
	public static String getTrainerRolesPath() {return TrainerRolesPath;}
	public static String getTrainersPath() {return TrainersPath;}
	public static String getVenuesPath() {return VenuesPath;}

	
	
}
