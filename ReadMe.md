# VideoGame metadata CRUD operations using REST API 
---------------------------------------------------------------
  
## Requirements 

Using Microsoft's .NET framework and C#, build an API project that will host your endpoints. 
Endpoints must start with "/api/".swagger 

Your API must include endpoints to facilitate CRUD operations, including: 

    1.  Get a collection of game metadata objects. 

	2.  This endpoint should support an optional filter parameter to get all games for certain category. 

	3.  This endpoint should support an optional order parameter to sort all games by name or release year. 

	4.  Get a specific game metadata object. 

	5.  Create a game metadata object. 

	6.  Update a game metadata object. 

	7.  Delete a game metadata object. 

	8.  At the very least, protect your endpoints with basic authorization. 

	9.  Be sure to program the appropriate response codes for unexpected conditions. 

	10. Be sure to include proper testing for you code. 

	11. Include a README.md file in the root of your project that explains how to run your code. 

Guidelines 

        Feel free to load the data blob into memory and use in-memory storage for your operations. 
It's good practice to validate your request input models. 
Basic auth username can be "admin" and password can be hardcoded. 


## Implementation 


The implementation is organized in the following projects: 

## 1. Project Name: VideoGame.RESTAPI 

    Below are the details of folders and files of this project- 

	1.1 Controllers  
	VideoGameController.cs contains CRUD operations on the data provided by Json.data file. 
	CRUD operations are exposed by REST Api endpoints. 
	All operations handles response codes for unexpected conditions. 
	It also validates request input models. 

	1.2 Helper->BasicAuth 
	Basic Authentication allows browsers or other user agents to request resources using credentials consisting of a username and a password. 
	BasicAuthAttribute.cs defines [BasicAuth] custom attribute. Attribute checks the HTTP authentication header, thus preventing undesired accesses. 
	BasicAuthFilter.cs fetch the credentials from request header and compares them with hardcoded username and password. 

	1.3 Services 
	The UserService is registered in the dependency injection (DI) container as a scoped service.  
	This UserService validates usernames and passwords provided by user. 

## 2. Project Name: VideoGame.DAL 

	2.1 Entities  
	Below are the details of folders and files of this project. 
	Entities represents the data structure of data source, data.json and are used in repository for data operations. 

	2.2 Repository 
	contains business logic, CRUD data operations. 

	2.3 Data 
	Data.json file contains videogame metadata in json format and used in project to do CRUD operations in project. 

	2.4 Models 
	GamesDto.cs is a class used for data transfer. 

## How to Run code 

Below are required tools to run this solution  

	- Visual Studio 2019 

	- Postman or alternatively use swagger in a browser. 

Download the project and run in visual studio 2019. 

I have included VideoGame API Test Cases excel file covering some of the scenarios.

Refer below demos on how to use Postman and Swagger to access endpoints 

Postman Demo 

	![screen-gif](https://github.com/jyotipawar/VideoGameRESTAPI/blob/master/Postman.gif) 

Swagger Demo 

	![screen-gif](https://github.com/jyotipawar/VideoGameRESTAPI/blob/master/Swagger.gif) 
 
Credential used for the basic authentication

	Username = 'admin'  
	passwords = 'password' 

Below are list of REST Api endpoints used in demo. 

	https://localhost:44301/api/VideoGames 

	https://localhost:44301/api/VideoGames?cat_name=Maze&orderby=name dsc 

	https://localhost:44301/api/VideoGames/DeleteGame?name=Pac-Man 

Below are the paramter values are used while ordering the list

	- name

	 https://localhost:44301/api/VideoGames?cat_name=Maze&orderby=name

	- name dsc

	https://localhost:44301/api/VideoGames?cat_name=Maze&orderby=name dsc

	- year 

	https://localhost:44301/api/VideoGames?cat_name=Maze&orderby=year

	- year dsc

	https://localhost:44301/api/VideoGames?cat_name=Maze&orderby=year dsc


## Important Notes 

	Browser caches credentials. Sometimes clearing the cache doesn't help. The only reliable way how to fake it is use Chrome's incognito window (Ctrl+N). But one prompt per one incognito window. So, you need new incognito window when you want to enter them again. 
So, in order to test properly user postman. 

## References: 

	https://codeburst.io/adding-basic-authentication-to-an-asp-net-core-web-api-project-5439c4cf78ee 
