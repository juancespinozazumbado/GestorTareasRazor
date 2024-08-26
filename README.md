# Gestor de Tareas Personales Web

## Principales funcionalidades:
### Autenticacion
- Login
- Registro
### Tareas
- Lista de tareas
- Crear una Tarea
- Editar una Tarea
- Eliminar una Tatrea
- Marcar una Tarea como completa o pendiente
- Buscar una tarea segun su Titulo o Descripcion.
 ##
[Live demo: Gestor de tareas en Azure](https://gestortareas.azurewebsites.net/)


 ###  Instalacion

 ### Uso de SQL Server como base de datos 
 El projecto usa una base de datos en Memoria. Para usar SQL Server 
 
indicarlo en el contenedor de dependencias en la clase `Program.cs`

 ```csharp
 
builder.Services.AgregarDbContext(builder.Configuration, useSqlite: false);

 ```

 en caso de desplegar en un ambiente prodictivo puede agregar la variable de entorno 
 para el connectionString o bien pude configurarrlo en el archivo ` appsettings.json`. 

 ```json
    "ConnectionStrings": {
    "SQLITECONNECTIONSTRING": "<Tu_Cadena_de_Connecion>"
  }
 ```

 asegurar camabiar la forma en la que injecta el connectionString segun casa caso de uso 

 ```
  var connectionString = configuration.GetConnectionString("SQLITECONNECTIONSTRING");
 ```
 
 ```
  var connectionString = Environment.GetEnvironmentVariable("SQLITECONNECTIONSTRING", EnvironmentVariableTarget.User);
  ```
 
 Si se va a ejecutar en modo debug puede descomentar la funcion 
 para efectuar una migracion de la base de datos usando Code-first.

 ```csharp
 if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
    ServicioMigracionData.InicializarMigracionDeDatos(app);
}
 ```
De lo contrario debe efectuar las migraciones antes de probar la aplicacion
para efectuar las migraciones primero debe descargar el componente ef-tool
```
```

luego ejecutar en la carpeta del projecto `src/GestorDeTareas.Web/ `
```
dotnet restore
dotnet tool restore
dotnet ef database update 
````

 #### To install client

 ```
   powershell -ex AllSigned -c "Invoke-RestMethod 'https://aka.ms/install-azd.ps1' | Invoke-Expression"

 ```


 ## (opcional) despliege usando docker y docker-compose

 Se dedea installar la aplicacion, una opcion muy util tanto para debug como para 
 despliegue local es usar ` Docker`, tanto en Windows como en Linux. 

 para desplegar la aplicacion en un contenedor de docker-compose
 asi como una base de datos SQL Server. 

pasos para el depliegue: 
crear un archivo `.env` en la raiz del projecto : 

```
DB_PASSWD={tu_Contraseña_SQLServer}
SQLSERVERCONNECTION="Server= SqlServer;Initial Catalog= GestorTareas; User=sa; Password=${DB_PASSWD};TrustServerCertificate=true"
SQLITECONNECTIONSTRING="Data Source=./wwwroot/Data/SQLITEDATABASE.sqlite"
```

Reemplazar los valores para `tu_Contraseña_SQLServer` y  `${DB_PASSWD}` con sus credenciales y guardarlos de manera segura. 

Ubiarse en la carpeta raiz del projecto y correr el projecto usando docker-compose

```
 docker-compose build
 docker-compose up
```



#### Tecnologias : 
 C# / .NET8
 SQL Server/SQLlite
 Entity Framework Core
 Identity
 Razor Pages
 Bootstrap, JQuery, AJAX
 Docker / Docker compose (para puruebas y despliegue local)
 Azure App Service - despligue on cloud
 GitHub Actions. 
