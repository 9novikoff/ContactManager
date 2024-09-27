# Contact Manager
## Test task for Bits Orchestra

Repo contains test.csv file and db backup file (db has no records).

## Setup Instructions
### 1. Clone the repository
Clone the repository to your local machine using the following command:
git clone https://github.com/9novikoff/ContactManager.git
Navigate into the project directory:
cd ContactManager

### 2. Configure the Database
The application uses a SQL Server database. You can either restore the database from a provided backup file or use Entity Framework migrations to create a new database.

Option 1: Restore from Backup
You will find a SQL Server backup file ContactsDb.bak
Open SQL Server Management Studio (SSMS) and connect to your local SQL Server instance.
Right-click on the Databases folder and choose Restore Database.
In the Source section, select Device, click on the ellipsis (...), and choose the ContactsDb.bak file from the repository.
Follow the prompts to restore the database.

Option 2: Run Migrations
If you want to create the database using migrations, follow these steps:

Open the terminal or command line.
Navigate to the project folder where the .csproj file is located.
Enter valid connection string in the appsettings.json file.
Run the following commands:
dotnet restore
dotnet ef database update
This will apply all the migrations and create the necessary database structure.

### 3. Running the Application
To run the application:

Ensure the project is built successfully:
dotnet build
dotnet run
This will start the application, and you can access it in your browser by navigating to http://localhost:5288 (by default).

### 4 Upload the test.csv or your own file 

## Implemented features
After each file uploading new data inserted

Data filtering

Data sorting (by clicking on the column)

Inline editing and removing records


## TODO LIST

Data validation :)
