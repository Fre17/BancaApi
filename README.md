# BancaApi
Este proyecto desarrollado en C# con Visual Studio tiene como objetivo crear una solución completa que incluye varios componentes esenciales: una API RESTful, una base de datos robusta, un conjunto de pruebas automatizadas y un worker para tareas en segundo plano. La API, construida con ASP.NET Core, permite interacciones eficientes mediante solicitudes HTTP. La base de datos, manejada mediante consultas SQL directas, garantiza un control detallado sobre las operaciones de datos. Las pruebas de unidad y de integración, implementadas con xUnit, aseguran la calidad y fiabilidad del código. Además, un worker que gestiona tareas asíncronas o programadas. La estructura del proyecto se organiza en controladores, servicios y repositorioss, con un enfoque riguroso en el manejo de errores, asegurando una operación estable y mantenible.
## Api
La API está diseñada con una estructura detallada para mejorar la seguridad, la estandarización, la limpieza del código y un modelado óptimo. Está compuesta por controladores, servicios y repositorios, cada uno con su correspondiente interfaz e inyección de dependencias configurada en la clase Program para una mayor utilidad a lo largo del proyecto. Cada uno de estos componentes incluye métodos tanto para consultas como para operaciones de mantenimiento, utilizando procedimientos almacenados en la base de datos según las necesidades específicas de cada operación, lo cual se explicará con más detalle en la sección dedicada a la base de datos. Los controladores emplean el método htmlSanitizer.Sanitize para prevenir ataques XSS al revisar las entidades recibidas como parámetros del usuario.
### Controladores
CuentaBancariaController
* ConsultaCuentaBancaria: endpoint que permite consultar las distintas cuentas bancarias presentes en la base de datos. Utilizando la opcion 0, se pueden dar consultas generales donde se consultan todas las cuentas. Aunque tambien, se pueden aplicar ciertos filtros, como lo son: por Pk_Tbl_Cuenta_Bancaria, Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario y Fk_Tbl_Usuario.Identificacion.
* MantenimientoCuentaBancaria: endpoint que permite dar mantenimiento directamente a la tabla de cuenta bancaria con la opcion 0. Permite crear una cuenta bancaria, la cual esta ligada a un usuario, o bien darle mantenimiento/actualización alguna cuenta ya creada.

TransaccionController
* ConsultaTransaccionBancaria: endpoint que realiza consulta, empleando la opcion 0, a la tabla de transacciones. Donde igualmente podra consultar por todos los registros, o bien crear filtros con los atributos: Pk_Tbl_Banca_Transacciones, Fk_Tbl_Banca_Tipo_Transaccion.Pk_Tbl_Banca_Tipo_Transaccion, Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Pk_Tbl_Cuenta_Bancaria y Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Pk_Tbl_Cuenta_Bancaria.
* MantenimientoTransaccionBancaria: dicho endpoint permite darle mantenimiento a las transacciones del sistema, en el cual la opción 0 permite el update/insert en la tabla. Por su parte la opcion 1, es la encargada de crear las recargas en la cuenta origen. Seguidamente la opción 2 es la encargada de registrar los retiros de fondos de las cuentas. La opción 3 es la que permite transferir fondos de la cuenta origen a la cuenta destino. Finalmente la opcion 4, se encarga del registro de los intereses ganados en las cuentas. Dichos ultimos movimientos quedan registrados en el historial de transacciones.
## Modelado .NET
Arquitectura: Repository
Patrón de diseño: Dependency injection
Framework de consulta de datos: directo
## Seguriad
Se han adoptado medidas como HtmlSanitizer para proteger contra ataques XSS, y se han empleado procedimientos almacenados con parámetros para prevenir la inyección de SQL.
# Modelado Base de datos
La base de datos cuenta con una estandarizacion en cuanto a la nomenclatura, tanto en el nombre de las talas, columnas y los procesos almacenados. Cada proceso almacenado cuenta con su try catch, y una posible insercion en la tabla de excepcion para el manejo de errores. Dicho proceso es importante para comprender la falla en el sistema y una pronta respuesta.
## Tablas
* TBL_BANCA_CUENTA_BANCARIA
* TBL_BANCA_EXCEPCION
 *TBL_BANCA_TASA
* TBL_BANCA_TIPO_TRANSACCION
* TBL_BANCA_TRANSACCIONES
* TBL_BANCA_USUARIOS
## Procesos almacenados
* PA_CON_TBL_BANCA_CUENTA_BANCARIA
* PA_CON_TBL_BANCA_TRANSACCIONES
* PA_MAN_TBL_BANCA_CUENTA_BANCARIA
* PA_MAN_TBL_BANCA_EXCEPCION
* PA_MAN_TBL_BANCA_TRANSACCIONES
# Pruebas unitarias y de integracion
El segundo proyecto que aparece en la solucion, son pruebas unitarias y de integracion a los distintos procesos que realizan los controladores. Las primeras dos clases, son pruebas unitarias a los controladores y sus respectivos endpoints, asi como a cada una de sus opciones. Seguidamente aparece una clase que permite probar en conjuntos varios procesos, como la creacion y consulta de cuenta, asi como la consulta de una cuenta, recargar, retirar y transferir fondos.
# Worker 
Por su parte el worker se ejecutara cada 24 horas. El cual hara un llamado al api, puntualmente al endpoint de mantenimiento de transaccion. El cual utiliza la opcion 4, para la generacion de los intereses en las cuentas y su posterior registro en la tabla del historico de transacciones.
# Coleccion de postman 
La coleccion de postman permite realizar pruebas a los distintos endpoints, asi como a sus opciones. La coleccion cuenta con una variable, la cual permite digitar una unica vez el puerto de localhost o la direccion donde se encuentra el api publicado, lo cual permitira su ejecucion de una forma mas sencilla.
# Considereciones 
* Restablecer el archivo .back con la base de datos
* Cambiar la cadena de conexion a la base de datos en el archivo appsettings.json, el cual se debera cambiar el nombre del servidos, asi como el usuario y la contraseña de acceso.
* Cambiar el puerto de consulta del api en el worker.
