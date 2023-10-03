# Web-scraper
An application that scrapes a web page, generates reports and exposes a RESTful web service for querying scraped data.

# Architecture
Built following Domain Driven Design and Clean Architecture.
  Application layer contains logic.\ operated on entities in the domain layer.
  Domain layer consists of entities relating to real life domain.
  Infrastructure layer consists of persistence and is used from the applicztion layer.


#Endpoints
  /api/Reports - Generate excel file from json input.
  /api/Reports/get-hotel-rates - Get hotel rates from json file.
  /api/scrape - Scrape web page and extract hotel date.
  
 # How to run
  Open project in Visual Studio and Run, project runs at http://localhost:5151/swagger/index.html showing a swagger page of the endpoints.
  
 # Suggestion 
 Reports can be generated periodically using a background job via Hangfire.
 
 # Extra
 Generated files appear inf 'files' folder in API folder
