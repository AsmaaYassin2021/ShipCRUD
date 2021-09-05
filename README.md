# ShipCRUD
The application will allow the user to perform CRUD (Create, Read, Update &amp; Delete) operations on a ship.

The solution contains from
1- Client App ==> Two pages Home and ships

--Ship Form validation :

a-Code must be required, match the pattern " AAAA-1111-A1" and not used before

b-Name must be required, the minimum length is 2, the max length is 100  and not used before

c-Ship width has to be a number and I  supposed that the ship width must have validation on the value so I did the width  between 15 and 70

d-Ship length  has to be number  and I supposed that the ship length must have validation on the value so I did the length between 50 and 400

2- Common project contains the ship interface shared among all Backend projects

3- Data project handles getAll, Get by code, Add , update and delete operations by using  memory in database context.

4- Business project contains service middler layer between Data project and API project

5- API project handles All requests coming from Client app plus makes the same client App validations.

6- Tests project by xunit test and moq for getAll ,GetBycode, create ,update and delete

Logging by Using Serilog and the path ..\\Logs\\Ship_.log"


Instructions required to run your application locally 

1- git clone the project into ShipCURDOperations

2- run docker file to build backend projects  

ShipCURDOperations> docker build --pull --rm -f "Dockerfile" -t shipcurdoperationslib:latest .

ShipCURDOperations> docker run -p 5006:80 shipcurdoperationslib

and API project will run on 5006 port for example  http://localhost:5006/api/ships/create

3- run docker file for ClientApp 

ShipCURDOperations\ClientApp> docker build -f "Dockerfile" -t shipcurdoperationsclientapp:latest .

ShipCURDOperations\ClientApp> docker run --rm -it -p 4201:4200 shipcurdoperationsclientapp

and ClientApp will run on http://localhost:4201/










