# Comments by Rohit

1. Run docker-compose-db.yml to start the database server using the command docker-compose -f docker-compose-db.yml up -d
2. Connect to the database server (host.docker.internal\db-functionapptest,1433 or localhost,1433) using SQL server management studio using the credentials sa/password123! and execute the sql script dotnet-test-one\source\ProductCatalogue\db-table-create.sql
3. Run the function app from visual studio using Dockerfile profile 
4. Use the url as shown in the console to test functions. In my case I used POST to http://localhost:34847/api/AddProduct  and json body {   "productName": "Orange"    }

I wish I could have made the function app accesible from docker-compose and initialiesd the DB with tables


