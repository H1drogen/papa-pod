package Retriever;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

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

public class DataRetrievalFactory {

	public static ArrayList<Venue> venueList(String filepath) {
		ArrayList<Venue> venueList = new ArrayList<Venue>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					venueList.add(new Venue(Integer.parseInt(propertyArray[0]), Integer.parseInt(propertyArray[1]),
							Integer.parseInt(propertyArray[2]), propertyArray[3], Integer.parseInt(propertyArray[4]),
							propertyArray[5], propertyArray[6]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return venueList;
	}

	public static ArrayList<Holidays> holidayList(String filepath) {
		ArrayList<Holidays> holidaysList = new ArrayList<Holidays>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					holidaysList.add(new Holidays(Integer.parseInt(propertyArray[0]),
							Integer.parseInt(propertyArray[1]), propertyArray[2], propertyArray[3], propertyArray[4],
							propertyArray[5], propertyArray[6], propertyArray[7]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return holidaysList;
	}

	public static ArrayList<Country> countryList(String filepath) {
		ArrayList<Country> countryList = new ArrayList<Country>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					countryList.add(new Country(Integer.parseInt(propertyArray[0]), Integer.parseInt(propertyArray[1]),
							propertyArray[2], Integer.parseInt(propertyArray[3]), propertyArray[4], propertyArray[5]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return countryList;
	}

	public static ArrayList<Offices> officesList(String filepath) {
		ArrayList<Offices> officesList = new ArrayList<Offices>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					System.out.println(line);
					officesList.add(new Offices(
							Integer.parseInt(propertyArray[0]), 
							propertyArray[1], 
							Integer.parseInt(propertyArray[2]),
							Integer.parseInt(propertyArray[3]),
							Integer.parseInt(propertyArray[4]),
							propertyArray[5], 
							propertyArray[6],
							propertyArray[7]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return officesList;
	}

	public static ArrayList<Programmes> programmesList(String filepath) {
		ArrayList<Programmes> programmesList = new ArrayList<Programmes>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					programmesList.add(new Programmes(Integer.parseInt(propertyArray[0]), propertyArray[1],
							propertyArray[2], propertyArray[3], propertyArray[4], propertyArray[5]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return programmesList;
	}

	public static ArrayList<Trainers> trainersList(String filepath) {
		ArrayList<Trainers> trainersList = new ArrayList<Trainers>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					trainersList.add(new Trainers(
							Integer.parseInt(propertyArray[0]),
							Integer.parseInt(propertyArray[1]), 
							propertyArray[2], 
							propertyArray[3],
							Integer.parseInt(propertyArray[4]), 
							Integer.parseInt(propertyArray[5]), 
							propertyArray[6],
							Integer.parseInt(propertyArray[7]),
							propertyArray[8], 
							propertyArray[9], 
							propertyArray[10], 
							Integer.parseInt(propertyArray[11]),
							propertyArray[12],
							propertyArray[13]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return trainersList;
	}

	public static ArrayList<Courses> coursesList(String filepath) {
		ArrayList<Courses> coursesList = new ArrayList<Courses>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					coursesList.add(new Courses(
							Integer.parseInt(propertyArray[0]),
							propertyArray[1],
							propertyArray[2],
							propertyArray[3],
							Integer.parseInt(propertyArray[4]),
							propertyArray[5],
							propertyArray[6],
							propertyArray[7],
							propertyArray[8],
							Integer.parseInt(propertyArray[9]),
							Integer.parseInt(propertyArray[10]),
							Integer.parseInt(propertyArray[11])));
							
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return coursesList;
	}

	public static ArrayList<Pathways> pathwaysList(String filepath) {
		ArrayList<Pathways> pathwaysList = new ArrayList<Pathways>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					pathwaysList.add(new Pathways(
							Integer.parseInt(propertyArray[0]),
							Integer.parseInt(propertyArray[1]), 
							Integer.parseInt(propertyArray[2]),
							propertyArray[3], 
							Integer.parseInt(propertyArray[4]),
							propertyArray[5],
							propertyArray[6], 
							propertyArray[7],
							propertyArray[8], 
							propertyArray[9], 
							Integer.parseInt(propertyArray[10]),
							Integer.parseInt(propertyArray[11]), 
							Integer.parseInt(propertyArray[12])));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return pathwaysList;
	}

	public static ArrayList<Regions> regionsList(String filepath) {
		ArrayList<Regions> regionsList = new ArrayList<Regions>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					regionsList.add(new Regions(
							Integer.parseInt(propertyArray[0]), 
							propertyArray[1],
							propertyArray[2],
							Integer.parseInt(propertyArray[3]),
							propertyArray[4], 
							propertyArray[5]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return regionsList;
	}

	public static ArrayList<TrainerRoles> trainerRolesList(String filepath) {
		ArrayList<TrainerRoles> trainerRolesList = new ArrayList<TrainerRoles>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					trainerRolesList.add(new TrainerRoles(Integer.parseInt(propertyArray[0]), propertyArray[1],
							propertyArray[2], propertyArray[3], propertyArray[4]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return trainerRolesList;
	}

	public static ArrayList<PathwayTypes> pathwayTypesList(String filepath) {
		ArrayList<PathwayTypes> pathwayTypesList = new ArrayList<PathwayTypes>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					pathwayTypesList.add(new PathwayTypes(Integer.parseInt(propertyArray[0]), propertyArray[1],
							propertyArray[2], Integer.parseInt(propertyArray[3]), propertyArray[4], propertyArray[5],
							propertyArray[6], propertyArray[7]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return pathwayTypesList;
	}

	public static ArrayList<PathwayTemplates> pathwayTemplatesList(String filepath) {
		ArrayList<PathwayTemplates> pathwayTemplatesList = new ArrayList<PathwayTemplates>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					pathwayTemplatesList.add(new PathwayTemplates(
							Integer.parseInt(propertyArray[0]), 
							propertyArray[1],
							propertyArray[2], 
							propertyArray[3], 
							Integer.parseInt(propertyArray[4]), 
							Integer.parseInt(propertyArray[5]),
							propertyArray[6], 
							propertyArray[7]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return pathwayTemplatesList;
	}

	public static ArrayList<CourseTemplates> courseTemplatesList(String filepath) {
		ArrayList<CourseTemplates> courseTemplatesList = new ArrayList<CourseTemplates>();

		FileReader fr;
		try {
			fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr);
			br.readLine();
			String line = br.readLine();
			while (line != null) {
				if (line.substring(0, 2) != "Id") {
					String[] propertyArray = line.split(",");
					courseTemplatesList.add(new CourseTemplates(
							Integer.parseInt(propertyArray[0]),
							propertyArray[1],
							propertyArray[2], 
							propertyArray[3], 
							Integer.parseInt(propertyArray[4]), 
							propertyArray[5],
							Integer.parseInt(propertyArray[6]), 
							propertyArray[7], 
							Integer.parseInt(propertyArray[8]), 
							Integer.parseInt(propertyArray[9]), 
							propertyArray[10], 
							propertyArray[11]));
				}
				line = br.readLine();
			}
			br.close();
			fr.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();

		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return courseTemplatesList;
	}

}
