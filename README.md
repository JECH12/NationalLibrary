IMPORTANTE!!!

1- La cadena de conexion dada trabaja con el ambiente local de SQL Server Management Studio, 
de ser necesario editar la cadena de conexion, 
sobre todo si se tiene un usuario y contraseña establecidos.

2- De ser requerida la edicion de la cadena de conexion es importante actualizarla tanto en el appsettings.json 
como en el archivo LibraryContextFactory.cs que se encuentra en el proyecto Infrastructure dentro de la carpeta Context.

3- Es necesario restaurar los paquetes Nuget de la aplicacion siguiendo la siguiente ruta en visual studio una vez abierto el proyecto:
Tools > NuGet Package Manager > Restore Packages
Esto instalara las dependecias de los paguetes nugets que se utilizan en la aplicacion.

4- Es muy IMPORTANTE ejecutar el comando "dotnet ef database update" abriendo una terminal desde el proyecto Infrastructure. 
Esto ejecutara las migraciones lo cual creara la base de datos y las tablas necesarias para la correcta operacion de la aplicacion.
