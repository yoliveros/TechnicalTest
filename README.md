
### Prueba tecnica del mini inventario

.NET 10 SDK
SQL Server (por defecto: localhost\SQLEXPRESS con autenticación de Windows)
Herramienta global de EF Core: dotnet tool install --global dotnet-ef

La base de datos NO se crea sola. Antes de arrancar, ejecuta una vez:

	`dotnet ef database update --project Infrastructure --startup-project WebAPI`

Con esto se crea la BD InventoryDB y se aplican las migraciones de Infrastructure/Migrations.

Dentro de la carpeta Infrastructure hay una carpeta DatabaseScript que tiene el SP que calcula el valor del inventario agrupado por categoria.

Si no usas SQL server express, puedes cambiar la cadena de conexión en el archivo appsettings.json del proyecto WebAPI.

Nota: El llamado al SP se puede hacer desde el scalar, esa api no requiere autenticacion en la api InventoryTotalByCategory
