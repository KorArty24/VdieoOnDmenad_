Refactoring of the project from Jonas Fagerberg's book
The entire project structure has been overhauled to get rid of Repo / Unit of Work Pattern.
Using the ideas from Jon Smith's book the Read part now implements Query Object pattern, 
the Admin database write service is being refactored to implement direct use of Entity Framework, thus 
violating the DRY principle. 
The second branch is being written to rebuild the app according to DDD principles. 
To login to the admin section use the credentials of the first user from /VOD.Database/Migrations/SampleUserData
In progress... 

