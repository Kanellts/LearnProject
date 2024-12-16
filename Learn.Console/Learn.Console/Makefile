initializeDatabase: 
	docker run --name my-db -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_DB=Learn -d -p 5432:5432 postgres && dotnet ef migrations add InitialCreate && dotnet ef migrations script && dotnet ef database update && dotnet run
