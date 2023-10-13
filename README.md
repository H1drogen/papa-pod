# Resource Planning Tool

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
