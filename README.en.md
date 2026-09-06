*[Leer en español](README.md)*

# ASP.NET Core Web APIs

REST Web APIs built with ASP.NET Core and Entity Framework Core on .NET 10.

They all share the same foundation: Entity Framework Core with migrations, an
in-memory repository behind an interface, custom middleware, and validation
attributes written by hand.

## Projects

### TareasAPI

A **List** has many **Tasks**. Each task can have **Notes**, which are not
persisted to the database and live in an in-memory repository.

| Method | Route | What it does |
|---|---|---|
| GET | `api/listas` | All lists |
| GET | `api/listas/{id}` | One list with its tasks |
| GET | `api/listas/{nombre}` | Search lists by name |
| POST | `api/listas` | Create a list |
| PUT | `api/listas/{id}` | Update a list |
| DELETE | `api/listas/{id}` | Delete a list |
| GET | `api/tareas?completadas=&listaId=` | Tasks, with optional filters |
| GET | `api/tareas/{id}` | One task, with the `incluir-lista` header |
| GET | `api/tareas/{titulo}` | Search tasks by title |
| POST | `api/tareas` | Create a task |
| PUT | `api/tareas/{id}` | Update a task |
| DELETE | `api/tareas/{id}` | Delete a task |
| GET | `api/notas` | All notes |
| GET | `api/notas/{id}` | One note |
| GET | `api/notas/nota-tarea/{tareaId}` | Notes belonging to a task |
| POST | `api/notas` | Create a note |
| PUT | `api/notas/{id}` | Update a note |
| DELETE | `api/notas/{id}` | Delete a note |
| GET | `api/tiempos-de-vida` | Shows the difference between Transient, Scoped, and Singleton |

### RecetasAPI

A **Category** has many **Recipes**. Each recipe can have **Comments**, which
are also kept in memory rather than persisted. The structure mirrors the
previous project, solved over a different domain.

| Method | Route | What it does |
|---|---|---|
| GET | `api/categorias` | All categories |
| GET | `api/categorias/{id}` | One category |
| GET | `api/categorias/{texto}` | Search categories by name |
| POST | `api/categorias` | Create a category |
| PUT | `api/categorias/{id}` | Update a category |
| DELETE | `api/categorias/{id}` | Delete a category |
| GET | `api/recetas` | All recipes |
| GET | `api/recetas/{id}` | One recipe |
| GET | `api/recetas/{texto}` | Search recipes by title |
| POST | `api/recetas` | Create a recipe |
| PUT | `api/recetas/{id}` | Update a recipe |
| DELETE | `api/recetas/{id}` | Delete a recipe |
| GET | `api/comentarios` | All comments |
| GET | `api/comentarios/{id}` | One comment |
| GET | `api/comentarios/{texto}` | Search comments by text |
| POST | `api/comentarios` | Create a comment |
| PUT | `api/comentarios/{id}` | Update a comment |
| DELETE | `api/comentarios/{id}` | Delete a comment |
| GET | `api/tiempos-de-vida` | Shows the difference between Transient, Scoped, and Singleton |

### GimnasioAPI

A gym runs **Classes**. Each class belongs to a **Discipline** and is taught by
one or more **Instructors**. Each instructor's role, lead or substitute, belongs
to that pairing rather than to the instructor, so the many-to-many relationship
has its own join entity. Students leave **Reviews**, identified by `Guid`, which
live in memory and never reach the database.

On top of the shared foundation, this project adds DTOs mapped with AutoMapper,
so that no entity ever enters or leaves through the API, and partial updates
with `JsonPatchDocument`.

| Method | Route | What it does |
|---|---|---|
| GET | `api/disciplinas` | All disciplines |
| GET | `api/disciplinas/{id}` | One discipline with its classes |
| POST | `api/disciplinas` | Create a discipline |
| PUT | `api/disciplinas/{id}` | Update a discipline |
| PATCH | `api/disciplinas/{id}` | Partially update a discipline |
| DELETE | `api/disciplinas/{id}` | Delete a discipline, if it has no classes |
| GET | `api/instructores` | All instructors |
| GET | `api/instructores/{id}` | One instructor with their classes |
| POST | `api/instructores` | Create an instructor |
| PUT | `api/instructores/{id}` | Update an instructor |
| PATCH | `api/instructores/{id}` | Partially update an instructor |
| DELETE | `api/instructores/{id}` | Delete an instructor |
| GET | `api/instructores-coleccion/{ids}` | Several instructors by comma-separated ids |
| POST | `api/instructores-coleccion` | Create several instructors at once |
| GET | `api/clases?disciplinaId=&activa=` | Classes, with optional filters |
| GET | `api/clases/{id}` | One class, with the `incluir-disciplina` header |
| GET | `api/clases/{nombre}` | Search classes by name |
| POST | `api/clases` | Create a class with its instructors and roles |
| PUT | `api/clases/{id}` | Update a class and replace its instructors |
| PATCH | `api/clases/{id}` | Partially update a class |
| DELETE | `api/clases/{id}` | Delete a class |
| GET | `api/clases/{claseId}/resenas` | Reviews of a class |
| GET | `api/clases/{claseId}/resenas/{guid}` | One review |
| POST | `api/clases/{claseId}/resenas` | Create a review |
| GET | `api/tiempos-de-vida` | Shows the difference between Transient, Scoped, and Singleton |

## Prerequisites

To open and run these projects in your local environment, you need the following installed:

* .NET 10 SDK
* Visual Studio 2026 or Visual Studio Code
* SQL Server LocalDB

## How to run the projects

1. Clone or download this repository to your computer.
2. Open the `.slnx` file of the project you want to try.
3. Create the database by applying the migrations. From the Package Manager Console, `Update-Database`, or from a terminal in the project folder, `dotnet ef database update`.
4. Press **Start** (or F5) to run the API.

Each project ships an `.http` file with sample requests ready to send from the
editor.

The connection string points to `(localdb)\MSSQLLocalDB`. If you use a different
server, change it in the project's `appsettings.Development.json` file.

---

## Author

**Leonel Maximiliano Blumberg**<br>
Software Developer | .NET · C# · ASP.NET · SQL | Systems Engineering Student

[LinkedIn](https://www.linkedin.com/in/leonel-blumberg) · [GitHub](https://github.com/Leonel-Blumberg) · [Email](mailto:leonelblumberg.it@gmail.com)
