# LearnProject

## How to run Learn.Console

### 1. The detailed method
a) Open the terminal in the projects folder.  
b) run ___docker run --name my-db -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_DB=Learn -d -p 5432:5432 postgres___ to initialize the PostgreSQL database inside a docker container.  
c) run ___docker ps___ to check if you have succesfully created the database.  
d) run ___dotnet ef migrations add InitialCreate___ to initialize the migration file with the first migration.  
e) run ___dotnet ef migrations script -o ./script.sql -i___ to generate the SQL script for migrations. Now by running ___./script.sql___, you can check the migrations.  
f) run ___dotnet ef database update___ to update the database.  
g) run ___dontet run___ to execute the Program.cs  

### 2. Using Makefile

Simply run ___make___ inside the terminal on the project folder.


## How to test the PostgreSQL Database

a) run ___docker pull postgres___ to pull the latest PostgreSQL version to your machine.  
b) run ___docker exec -it my-db psql -U postgres___ to open an interactive terminal and conect to the database.  
c) run ___psql -h localhost -U postgres___ to make a connection with the database server.  
d) run \\___l___ to see the databases in your server. You should be able to see the 'Learn' database.  
e) run \\___c Learn___ to connect to the learn database.  
f) run \\___dp___. You should be able to see the 'Students' and the '__EFMigrationHistory' tables.  
g) You can test if the students data were succesfully seeded to the database by running ___SELECT * FROM "Students"___.
