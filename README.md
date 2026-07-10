
# Control de incidencias internas

Prueba tecnica para NuCore


## Requisitos

.Net 8

MySql


## Correr la aplicación

El primer paso es correr el script "sql nucore.sql"

Es necesario clonar el repositorio en Visual Studio 2022, actualizar la cadena de conexión 

```
"DefaultConnection": "Server={tu_servidor};Database=IncidenciasDB;User Id={tu_usuario};Password={tu_contraseña};"
```

Y finalmente correr la aplicacion
## Arquitectura
Para esta prueba técnica, se utiliza una versión sencilla de Clean Architecture, compuesto por Domain, Services, Infraestructure y la aplicación web funcionando como Presentation
## Comentarios
En esta prueba técnica al ser un MVP, no se contempla la autenticación, además de utilizarse el DbContext inyectado directamente en los servicios para simplificar ciertos componentes



