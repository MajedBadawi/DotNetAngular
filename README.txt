- I used Microsoft SQL Server Management Studio 18 to host the database and vs code to run the angular application
- Before starting part 2 (web api), run the following commands in the package manager console:
	Add-Migration APISetup -o Data/Migration
	Update-Database
- Before starting part 3, run the following commands in the package manager console (VS):
	Add-Migration APISetup -o Data/Migration
	Update-Database
+ open the angular application (MajedBadawi_CME\PropertyManager_Part3\PropertyManager) in vs code and run this in the terminal:
	ng serve --o

Note: I changed the web api when implementing part 3 to add user authentication and some sense of security to the application by
 authorizing the api calls using jwt.