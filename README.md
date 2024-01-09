# JAMK Course Review App

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![TypeScript](https://img.shields.io/badge/typescript-%23007ACC.svg?style=for-the-badge&logo=typescript&logoColor=white)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

## Description

This application (still in development), is a full stack web application that allows users to review courses they have taken at my university, JAMK University of Applied Sciences. The initial version of the application will be based on Bachelor-level ICT courses, but the application will be expanded to include all courses offered by the university.

## Planned features

-   User authentication and registration
-   Course reviews + ratings
-   Academic Wishlist (list of courses the user is interested in taking)
-   Course search, filtering by ratings, teachers and other criteria
-   Detailed course statistics (average rating, number of reviews, etc.)
-   Course information (ECTS credits, course description, etc.)
-   Course schedule (when the course is offered, which teachers teach it, etc.)
-   Course materials (links to course materials, if available)

### Current Phase of Development

The application is currently in the early stages of development, with the backend being developed using C# and ASP.NET Core, using Microsoft SQL Server for the database.

#### Progress:

**Database**:

-   [x] Set up Microsoft SQL Server database locally, running in Docker container
-   [x] Design and implement database schema
-   [x] Scrape the required course information, clean it, and add it to the database [cleaning_and_preparation.ipynb](./data/cleaning_and_preparation.ipynb)

**Backend**:

-   [x] Add functionality for user authentication and registration, using Entity Framework Core Identity
-   [x] Add functionality for getting all courses, as well as a single course by course code
-   [x] Add functionality for "Academic Wishlist"
-   [x] Add functionality for course reviews
-   [x] Add automatic calculation of average rating for each course, and return that value when getting a single course
-   [ ] Add sign in with Microsoft account functionality - students can sign in straight with their student account

**Frontend**:

-   [x] Set up Angular project
-   [x] Add header and footer components
-   [x] Add searchbar component with mock data
-   [x] Modify searchbar component to fetch data from backend
-   [x] Fully restructure the app into modules, shared and core + implement lazy loading
-   [x] Add login and functionality + auth service
-   [x] Add course details page
-   [ ] Add reviews to course details page

### Database initialization and population

**Will be improved later, this is just for development purposes**

1. Clone the repository

2. Navigate to the [`Docker/database`](./Docker/database/) folder

3. Run `docker-compose up -d` to start the database container and make sure it's running with `docker ps`

4. Navigate to the [`JAMKCourseReviewAPI`](./JAMKCourseReviewAPI/) folder

5. Add the connection string to the `appsettings.Development.json` file:

```json
"ConnectionStrings": {
    "TestDB": "Server=localhost,1433;Database=TestDB;User Id=sa;Password=YourStrong@Passw0rd;"
}
```

6. Run `dotnet ef database update` to make the migrations

7. Create a `.env` file in the root directory of the project and add the following:

```
DB_SERVER=localhost,1433
DB_DATABASE=TestDB
DB_USERNAME=SA
DB_PASSWORD=YourStrong@Passw0rd
```

8. Run the [course population script](./scripts/populate_courses.py) - you have to install `sqlalchemy`, `pandas` and `python-dotenv` first.

9. Now you can run the API with `dotnet run`

## Contributors

-   Taavi Kalaluka
