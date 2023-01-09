Refactoring of the project from Jonas Fagerberg's book
The entire project structure has been overhauled to get rid of Repo / Unit of Work Pattern. https://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework-part-2/

Using the ideas from Jon Smith's book the Read part now implements Query Object pattern, 
the Admin database write service is being refactored to implement direct use of Entity Framework/ 
Identity authorisation service for RazorPages Admin has been corrected and now uses Claims based authorisation, instead of Role-based. 
Seeding user data is implement using rough EF instead of manager, which drasticaly improves seeding time on real world projects. 
DDD branch has been abandoned for the time being. 
In progress... 

