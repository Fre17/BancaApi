# BancaApi
Este proyecto desarrollado en C# con Visual Studio tiene como objetivo crear una solución completa que incluye varios componentes esenciales: una API RESTful, una base de datos robusta, un conjunto de pruebas automatizadas y un worker para tareas en segundo plano. La API, construida con ASP.NET Core, permite interacciones eficientes mediante solicitudes HTTP. La base de datos, manejada mediante consultas SQL directas, garantiza un control detallado sobre las operaciones de datos. Las pruebas de unidad y de integración, implementadas con xUnit, aseguran la calidad y fiabilidad del código. Además, un worker gestiona tareas asíncronas o programadas. La estructura del proyecto se organiza en controladores, servicios y repositorios, con un enfoque riguroso en el manejo de errores, asegurando una operación estable y mantenible.
## Api
La API está diseñada con una estructura detallada para mejorar la seguridad, la estandarización, la limpieza del código y un modelado óptimo. Está compuesta por controladores, servicios y repositorios, cada uno con su correspondiente interfaz e inyección de dependencias configurada en la clase Program para una mayor utilidad a lo largo del proyecto. Cada uno de estos componentes incluye métodos tanto para consultas como para operaciones de mantenimiento, utilizando procedimientos almacenados en la base de datos según las necesidades específicas de cada operación, lo cual se explicará con más detalle en la sección dedicada a la base de datos. Los controladores emplean el método HtmlSanitizer.Sanitize para prevenir ataques XSS al revisar las entidades recibidas como parámetros del usuario.
### Controladores
CuentaBancariaController
* ConsultaCuentaBancaria: endpoint que permite consultar las distintas cuentas bancarias presentes en la base de datos. Utilizando la opción 0, se pueden dar consultas generales donde se consultan todas las cuentas. Aunque también, se pueden aplicar ciertos filtros, como lo son: por Pk_Tbl_Cuenta_Bancaria, Fk_Tbl_Usuario.Pk_Tbl_Banca_Usuario y Fk_Tbl_Usuario.Identificacion.
* MantenimientoCuentaBancaria: endpoint que permite dar mantenimiento directamente a la tabla de cuenta bancaria con la opción 0. Permite crear una cuenta bancaria, la cual está ligada a un usuario, o bien darle mantenimiento/actualización a alguna cuenta ya creada.

TransaccionController
* ConsultaTransaccionBancaria: endpoint que realiza consulta, empleando la opción 0, a la tabla de transacciones. Donde igualmente podrá consultar por todos los registros, o bien crear filtros con los atributos: Pk_Tbl_Banca_Transacciones, Fk_Tbl_Banca_Tipo_Transaccion.Pk_Tbl_Banca_Tipo_Transaccion, Fk_Tbl_Banca_Cuenta_Bancaria_Origen.Pk_Tbl_Cuenta_Bancaria y Fk_Tbl_Banca_Cuenta_Bancaria_Destino.Pk_Tbl_Cuenta_Bancaria.
* MantenimientoTransaccionBancaria: dicho endpoint permite darle mantenimiento a las transacciones del sistema, en el cual la opción 0 permite el update/insert en la tabla. Por su parte la opción 1, es la encargada de crear las recargas en la cuenta origen. Seguidamente la opción 2 es la encargada de registrar los retiros de fondos de las cuentas. La opción 3 es la que permite transferir fondos de la cuenta origen a la cuenta destino. Finalmente la opción 4, se encarga del registro de los intereses ganados en las cuentas. Dichos últimos movimientos quedan registrados en el historial de transacciones.
## Modelado .NET
* Arquitectura: Repository
* Patrón de diseño: Dependency Injection
* Framework de consulta de datos: Directo
## Seguriad
Se han adoptado medidas como HtmlSanitizer para proteger contra ataques XSS y se han empleado procedimientos almacenados con parámetros para prevenir la inyección de SQL.
# Modelado Base de datos
La base de datos cuenta con una estandarización en cuanto a la nomenclatura, tanto en el nombre de las tablas, columnas y los procedimientos almacenados. Cada procedimiento almacenado cuenta con su try-catch y una posible inserción en la tabla de excepciones para el manejo de errores. Dicho proceso es importante para comprender las fallas en el sistema y una pronta respuesta.
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
El segundo proyecto que aparece en la solución son pruebas unitarias y de integración a los distintos procesos que realizan los controladores. Las primeras dos clases son pruebas unitarias a los controladores y sus respectivos endpoints, así como a cada una de sus opciones. Seguidamente aparece una clase que permite probar en conjunto varios procesos, como la creación y consulta de cuenta, así como la consulta de una cuenta, recargar, retirar y transferir fondos.
# Worker 
Por su parte, el worker se ejecutará cada 24 horas. El cual hará un llamado a la API, puntualmente al endpoint de mantenimiento de transacción, el cual utiliza la opción 4 para la generación de los intereses en las cuentas y su posterior registro en la tabla del histórico de transacciones.
# Coleccion de postman 
La colección de Postman permite realizar pruebas a los distintos endpoints, así como a sus opciones. La colección cuenta con una variable, la cual permite digitar una única vez el puerto de localhost o la dirección donde se encuentra la API publicada, lo cual permitirá su ejecución de una forma más sencilla.
# Considereciones 
* Restablecer el archivo .bak con la base de datos.
* Cambiar la cadena de conexión a la base de datos en el archivo appsettings.json, en el cual se deberá cambiar el nombre del servidor, así como el usuario y la contraseña de acceso.
* Cambiar el puerto de consulta de la API en el worker.
