Refactoring of the project from Jonas Fagerberg's book
The entire project structure has been overhauled to get rid of Repo / Unit of Work Pattern. https://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework-part-2/

Using the ideas from Jon Smith's book the User/Client part now implements Query Object pattern. To demonstrate the capabilities of QueryObject I abandoned dashboard UI in user part in favor of simple List based UI, which allows to sort Courses by the length of their videos. Added some JS code to utilize that feature (sort courses by video length) in UI.
The Admin database write service is being refactored to implement direct use of Entity Framework/ 
Identity authorisation service for RazorPages Admin has been corrected and now uses Claims based authorisation, instead of Role-based. 
Seeding user data is implement using rough EF instead of manager, which drasticaly improves seeding time on real world projects. 
DDD branch has been abandoned for the time being. 
Added 20 tests to dest db layer. Added integration tests to test service layer, wrote the prototype for testing Authorisation (Razor Pages).
Direct use of EF leads to substitution of Unit Tests for integration Tests against a productionDb as recomended by Microsoft and Jon Smith (EF Core in Action). 
Added Swagger, made changes to API Controller to utilize it. 
In progress... 

