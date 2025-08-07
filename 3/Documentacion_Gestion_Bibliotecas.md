# 📚 Library Management System API

Una aplicación desarrollada en **ASP.NET Core Web API (.NET 8)** para la gestión de bibliotecas, usuarios y libros. Este proyecto implementa principios de **Clean Architecture** y utiliza **Swagger** para pruebas y documentación de endpoints.

---

## 🚀 Características Principales

- Gestión de bibliotecas, usuarios y libros
- Operaciones CRUD completas
- Entrega y renta de libros
- Filtros dinámicos por entidad
- Datos almacenados en memoria (sin base de datos)
- Documentación interactiva con Swagger

---

## 🧱 Arquitectura del Proyecto

El proyecto sigue los principios de **Clean Architecture**, organizado en carpetas:

| Carpeta         | Descripción                                                                 |
|-----------------|------------------------------------------------------------------------------|
| `Application`   | Lógica de negocio y DTOs                                                    |
| `Controllers`   | Endpoints expuestos para cada entidad                                       |
| `Domine`        | Interfaces que definen los contratos de las clases                          |
| `Infrastructure`| Implementación de servicios y comunicación entre capas                      |

---

## 🧠 Patrón de Diseño

Se utiliza el **patrón Singleton** para manejar el almacenamiento en memoria, simulando una base de datos persistente durante la ejecución.

---

## 📡 Endpoints Disponibles

### 📘 Bibliotecas

- `POST /api/Library/add-library` – Crear nueva biblioteca
- `PUT /api/Library/update-library/{libraryId}` – Modificar biblioteca
- `DELETE /api/Library/delete-library/{libraryId}` – Eliminar biblioteca
- `GET /api/Library/get-libraries` – Consultar todas
- `POST /api/Library/get-library` – Consultar por filtro

### 👤 Usuarios

- `POST /api/User/add-user` – Crear usuario
- `PUT /api/User/update-user/{userId}` – Modificar usuario
- `PUT /api/User/update-user-library/{userId}/{libraryId}` – Trasladar usuario entre bibliotecas
- `DELETE /api/User/delete-user/{userId}` – Eliminar usuario
- `GET /api/User/get-users` – Consultar todos
- `POST /api/User/get-user-filter` – Consultar por filtro
- `GET /api/User/get-user-library/{libraryId}` – Consultar por biblioteca
- `GET /api/User/get-user-id/{userId}` – Consultar por ID

### 📚 Libros

- `POST /api/Book/add-book` – Crear libro
- `PUT /api/Book/update-book/{bookId}` – Modificar libro
- `DELETE /api/Book/delete-/{bookId}` – Eliminar libro
- `GET /api/Book/get-books` – Consultar todos
- `GET /api/Book/get-book-library/{libraryId}` – Consultar por biblioteca
- `GET /api/Book/get-book-id/{bookId}` – Consultar por ID
- `PUT /api/Book/rent-book-user/{userId}/{bookId}` – Entregar libro a usuario
- `PUT /api/Book/return-book-user/{bookId}` – Devolver libro a la biblioteca

---

## 🧪 Pruebas con Swagger

Swagger está habilitado para explorar y probar los endpoints fácilmente. Al ejecutar el proyecto, accede a: https://localhost:{puerto}/swagger


---

## ⚠️ Consideraciones

- El almacenamiento en memoria es ideal para pruebas, pero no recomendado para producción.
- Las validaciones generales están implementadas, pero se pueden mejorar los mensajes de error específicos.
- El proyecto puede evolucionar hacia una arquitectura con capas separadas en distintos proyectos.

---

## 🛠️ Requisitos

- .NET 8 SDK
- Visual Studio 2022 o superior
- Navegador web para acceder a Swagger

