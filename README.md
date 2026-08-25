*[Read this in English](README.en.md)*

# ASP.NET Core Web APIs

Web APIs REST hechas con ASP.NET Core y Entity Framework Core sobre .NET 10.
Son proyectos en desarrollo: cada uno arranca resolviendo un modelo simple y le
voy sumando funcionalidad.

Todas comparten la misma base: Entity Framework Core con migraciones, un
repositorio en memoria detrás de una interfaz, middlewares propios y atributos
de validación escritos a mano.

## Proyectos

### TareasAPI

Una **Lista** tiene muchas **Tareas**. Cada tarea puede tener **Notas**, que no
se persisten en la base y viven en un repositorio en memoria.

| Método | Ruta | Qué hace |
|---|---|---|
| GET | `api/listas` | Todas las listas |
| GET | `api/listas/{id}` | Una lista con sus tareas |
| GET | `api/listas/{nombre}` | Busca listas por nombre |
| POST | `api/listas` | Crea una lista |
| PUT | `api/listas/{id}` | Actualiza una lista |
| DELETE | `api/listas/{id}` | Borra una lista |
| GET | `api/tareas?completadas=&listaId=` | Tareas, con filtros opcionales |
| GET | `api/tareas/{id}` | Una tarea, con cabecera `incluir-lista` |
| GET | `api/tareas/{titulo}` | Busca tareas por título |
| POST | `api/tareas` | Crea una tarea |
| PUT | `api/tareas/{id}` | Actualiza una tarea |
| DELETE | `api/tareas/{id}` | Borra una tarea |
| GET | `api/notas` | Todas las notas |
| GET | `api/notas/{id}` | Una nota |
| GET | `api/notas/nota-tarea/{tareaId}` | Notas de una tarea |
| POST | `api/notas` | Crea una nota |
| PUT | `api/notas/{id}` | Actualiza una nota |
| DELETE | `api/notas/{id}` | Borra una nota |
| GET | `api/tiempos-de-vida` | Muestra la diferencia entre Transient, Scoped y Singleton |

### RecetasAPI

Una **Categoría** tiene muchas **Recetas**. Cada receta puede tener
**Comentarios**, que tampoco se persisten y viven en memoria. La estructura es
la misma que la anterior, resuelta sobre otro dominio.

| Método | Ruta | Qué hace |
|---|---|---|
| GET | `api/categorias` | Todas las categorías |
| GET | `api/categorias/{id}` | Una categoría |
| GET | `api/categorias/{texto}` | Busca categorías por nombre |
| POST | `api/categorias` | Crea una categoría |
| PUT | `api/categorias/{id}` | Actualiza una categoría |
| DELETE | `api/categorias/{id}` | Borra una categoría |
| GET | `api/recetas` | Todas las recetas |
| GET | `api/recetas/{id}` | Una receta |
| GET | `api/recetas/{texto}` | Busca recetas por título |
| POST | `api/recetas` | Crea una receta |
| PUT | `api/recetas/{id}` | Actualiza una receta |
| DELETE | `api/recetas/{id}` | Borra una receta |
| GET | `api/comentarios` | Todos los comentarios |
| GET | `api/comentarios/{id}` | Un comentario |
| GET | `api/comentarios/{texto}` | Busca comentarios por texto |
| POST | `api/comentarios` | Crea un comentario |
| PUT | `api/comentarios/{id}` | Actualiza un comentario |
| DELETE | `api/comentarios/{id}` | Borra un comentario |
| GET | `api/tiempos-de-vida` | Muestra la diferencia entre Transient, Scoped y Singleton |

## Requisitos Previos

Para abrir y ejecutar estos proyectos en tu entorno local, necesitas tener instalado:

* .NET 10 SDK
* Visual Studio 2026 o Visual Studio Code
* SQL Server LocalDB

## Cómo ejecutar los proyectos

1. Clona o descarga este repositorio en tu computadora.
2. Abre el archivo `.slnx` del proyecto que quieras probar.
3. Crea la base de datos aplicando las migraciones. Desde la consola del administrador de paquetes, `Update-Database`, o desde una terminal parada en la carpeta del proyecto, `dotnet ef database update`.
4. Presiona el botón **Iniciar** (o F5) para levantar la API.

Cada proyecto trae un archivo `.http` con peticiones de ejemplo listas para
mandar desde el editor.

La cadena de conexión apunta a `(localdb)\MSSQLLocalDB`. Si usás otro servidor,
cambiala en el archivo `appsettings.Development.json` del proyecto.

---

## Autor

**Leonel Maximiliano Blumberg**<br>
Desarrollador de Software | .NET · C# · ASP.NET · SQL | Estudiante de Ing. en Sistemas

[LinkedIn](https://www.linkedin.com/in/leonel-blumberg) · [GitHub](https://github.com/Leonel-Blumberg) · [Email](mailto:leonelblumberg.it@gmail.com)
