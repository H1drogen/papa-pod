# Resource Planning Tool
# Overview

DM currently have a Delegate Management System (DMS) in place which is primarily used by Academy Administrators, trainers and trainees. The DMS has many uses and functionalities within it. For Academy Administrators and trainers, it allows them to manage trainee attendance, schedule upcoming training pathways and upload trainee assessment results to name a few. The system also gives trainees visibility to course information and placement opportunities amongst other factors.
Several technical issues have arisen from Academy Administrators using the DMS as the number of trainees and consultants it holds has exponentially grown since the company has vastly expanded in size over the years since it was built. The system was designed to manage a smaller number of people, but with the higher volume, it is no longer performing at an adequate level.
In addition to this, there have been some areas of functionality within the DMS while have been identified to be able to be improved upon to enhance the Academy Administrators’ ease of use of the system and their productivity.
The agreed solution was to build a brand-new system to be able to handle a larger number of users and refine some of the functionality difficulties previously experienced by Academy. The original idea was to build an Academy Management System (AMS) which in essence was going to be a brand-new version of the DMS which would take on board all above points.
However, after requirements were gathered and development work had begun, the plan changed to instead create an Academy Management Suite (keeping the same ‘AMS’ acronym) to cover off the initial requirements, but also to be used as a suite for accreditation, scheduling, the Learning Management System (LMS), sales reporting and SharePoint course content management.
The Academy Management Suite will be built as two parts:
Resource Planning Tool – This will be the back-end system only accessed by the Academy Administrators. It will allow them to create training pathways, assign trainers to courses, assign skills to trainers and record trainer availability.
Third-party LMS system – This will be the front-end system which can be interacted by Academy Administrators, trainers and trainees. All data from the Resource Planning Tool will pull into this third-party system. Within this system, users can add new trainees, send trainee/trainer schedules to Outlook calendars, send Microsoft Teams invites, and will also integrate with SuccessFactors, LinkedIn Learning and Intuition. Azure Active Directory will link with this too enabling single sign-on for both this system and the Resource Planning Tool, and the Azure Virtual Desktop will be accessible from here also. Accredited courses highlighted in this system will begin an automated workflow and course content from SharePoint will be mirrored here. The Classroom Management System will possibly be integrated in future.
We will build the Resource Planning Tool in-house while a third-party vendor will build the external LMS system from our requirements.

## Setup

1. Download Visual Studio 2022 Community [from here](https://visualstudio.microsoft.com/downloads/)
2. From the installer, choose `ASP.NET and Web Development`

## Retrieving code

1. Install Git [from here](https://git-scm.com/download).
   - On Windows, use the standalone installer.
2. Make a new folder inside Documents called `Code`
3. Open a terminal
   - On Windows 11: Search for `Terminal` in the Start Menu
   - [On macOS](https://support.apple.com/en-gb/guide/terminal/apd5265185d-f365-44cb-8b09-71a064a42125/mac): Search for `Terminal` in Launchpad
   - On Linux: Open Terminal/Console/Shell
4. Go to your new Code folder
   - `cd Documents/Code`
5. Clone the repository
   - On GitLab, click the `Clone` button and copy the `Clone with HTTPS` link
   - Enter the following command but replace `<clone-https-link>`, including the `<>`
   - `git clone <clone-https-link> "Resource Planning Tool"`
   - Use your FDM GitLab account email and password

## Running

1. Open the folder for the repository you cloned earlier
   - Documents -> Code -> Resource Planning Tool
2. Open API/Fdm.WebAPI.sln
3. Open RPT/Fdm.Ams.Web.sln
4. Click the green play button on the top of Visual Studio 2022 for both applications

- **API:** https://localhost:50000/swagger/
- **RPT:** https://localhost:60000/

## Useful information

### Resetting database

To reset the database and clear all data:

1. Stop running the API
2. Go to `Git Changes` and hover over `Database.sqlite`
3. Click the undo arrow

### Exploring database

All data is stored in an SQLite database.

To view the data:

1. Visit https://sqliteviewer.app/
2. Stop running the API
   - This will save the latest changes to Database.db
3. From the website, open (or drag and drop) API/Fdm.WebAPI/Database.sqlite
