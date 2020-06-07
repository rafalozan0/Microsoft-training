Requisitos previos:
Creación de una aplicación de función para hospedar nuestra función 1. Inicie sesión en Azure Portal con la misma cuenta con la que ha activado el espacio aislado.

2. En el menú de Azure Portal o la página Inicio, seleccione Crear un recurso y luego Proceso > Aplicación de funciones.

3. Escriba la configuración de la aplicación de funciones como se especifica en la tabla siguiente.

TABLA 1

Configuración Valor Descripción

Nombre de la

aplicación Nombre único globalmente Nombre que identifica la nueva aplicación de función. Los caracteres válidos son a-z, 0-9 y -.

Suscripción Suscripción de Concierge Suscripción en la que se creará esta nueva aplicación de función.

Grupo de recursos [nombre del grupo de recursos del espacio aislado] Nombre del grupo de recursos en el que se va a crear la aplicación de función. Asegúrese de seleccionar Usar existente y utilice el grupo de recursos del ejercicio anterior. De esta forma, todos los recursos que hemos creado en este módulo se conservan juntos.

SO Windows Sistema operativo que hospeda la aplicación de función.

Plan de hospedaje Plan de consumo Plan de hospedaje que define cómo se asignan los recursos a la Function App. En el Plan de consumo predeterminado, los recursos se agregan dinámicamente según los requisitos de las funciones. En este hospedaje sin servidor, solo paga por el tiempo durante el cual se ejecutan las funciones.

Ubicación Seleccione la misma ubicación que usó anteriormente. Elija una región cerca de usted o cerca de otros servicios a los que tienen acceso las funciones. Seleccione la misma región que ha usado al crear la cuenta de Text Analytics API en el último ejercicio.

TABLA 1

Configuración Valor Descripción

Pila en tiempo de

ejecución Node.js El código de ejemplo de este módulo está escrito en JavaScript.

Almacenamiento Nombre único global Nombre de la nueva cuenta de almacenamiento usada por la aplicación de función. Los nombres de las cuentas de almacenamiento deben tener entre 3 y 24 caracteres y solo pueden incluir números y letras en minúscula. Este cuadro de diálogo rellena el campo con un nombre único derivado del nombre que asignó a la aplicación. Sin embargo, no dude en usar otro nombre o incluso una cuenta existente.

4. Haga clic en Crear para aprovisionar e implementar la aplicación de función.

5. Haga clic en el icono de notificación de la esquina superior derecha del portal y observe el mensaje Implementación en curso.

6. La implementación puede tardar un tiempo. Por tanto, permanezca en el Centro de notificaciones y esté atento para ver un mensaje Implementación correcta.

7. Una vez implementada la aplicación de función, vaya a Todos los recursos en el portal. Se muestra la aplicación de función con el tipo App Service y el nombre que le asignó. Para abrir la aplicación de función, selecciónela en la lista.

Enhorabuena. Ha creado e implementado la aplicación de función.

Sugerencia

¿Tiene dificultades para encontrar las aplicaciones de función en el portal? Intente agregar aplicaciones de función a sus favoritos en Azure Portal.

Creación de una función que ejecute nuestra lógica

Ahora que tenemos una aplicación de función, es el momento de crear una función. Se activa una función a través de un desencadenador. En este módulo, usaremos un desencadenador de colas. El runtime sondea una cola e inicia esta función para procesar un mensaje nuevo.

1. Seleccione el botón Agregar (+) situado junto a Funciones. Esta acción inicia el proceso de creación de funciones.

2. En la página Azure Functions for JavaScript - getting started (Azure Functions para JavaScript: introducción), seleccione En el portal y luego Continuar.

3. En el paso Crear una función, seleccione Más plantillas... y luego Finish and view templates (Plantillas Finalizar y ver).

4. En la lista de plantillas disponibles para esta aplicación de función, seleccione Desencadenador de Azure Queue Storage.

5. Si ve un mensaje que indica Extensiones no instaladas, seleccione Instalar. La instalación de dependencias puede llevar un par de minutos. Espere hasta que termine la instalación antes de continuar.

6. En el cuadro de diálogo Nueva función que aparece, especifique los siguientes valores.

TABLA 2

Propiedad Valor

Nombre discover-sentiment-function

Nombre de cola new-feedback-q

Conexión de cuenta de Storage AzureWebJobsStorage

7. Haga clic en Crear para iniciar el proceso de creación de la función.

8. Se crea una función en el lenguaje elegido mediante la plantilla de funciones del desencadenador de colas. Aunque en este módulo vamos a implementar la función en JavaScript, puede crear una función en cualquier lenguaje compatible.

Una vez completado el proceso de creación, el editor de código se abre en el portal y carga la página index.js. Este archivo es el archivo de código en el que escribimos nuestra lógica de la función.

Pruébelo

Vamos a probar lo que tenemos hasta ahora. Aún no hemos escrito ningún código, por lo que esta prueba es para asegurarnos de que lo que hemos configurado hasta ahora, funciona.

1. Haga clic en Ejecutar en la parte superior del editor de código.

2. Observe la pestaña Registros que se abre en la parte inferior de la pantalla. Si todo funciona según lo previsto, verá un mensaje similar al siguiente.

El botón Ejecutar inició nuestra función y pasó los datos de cola de ejemplo, el texto predeterminado de la ventana de solicitud Prueba de nuestra función.

Sugerencia

Si se agota el tiempo de espera de la función o no devuelve un valor correctamente, pruebe a reiniciar la aplicación de función. Seleccione la aplicación de función en el menú de la izquierda y, a continuación, seleccione Reiniciar en el panel Introducción. Espere a que la aplicación de función se reinicie y, a continuación, pruebe a ejecutar de nuevo la función.

Buen trabajo. Ha agregado correctamente una función desencadenada por colas a su aplicación de función y la ha probado para asegurarse de que funciona según lo previsto. Vamos a agregar más funcionalidad a la función en el ejercicio siguiente.

Echemos un vistazo rápido al otro archivo de la función, el archivo de configuración function.json.
Como puede ver, esta función tiene un enlace de desencadenador que se llama myQueueItem del tipo queueTrigger. Si llega un mensaje nuevo a la cola que hemos denominado new-feedback-q, se llama a la función. Hacemos referencia al nuevo mensaje mediante el parámetro de enlace myQueueItem. Los enlaces se ocupan realmente de parte del trabajo pesado por nosotros.

En el paso siguiente, vamos a agregar código para llamar al servicio Text Analytics API.

Sugerencia

Puede ver index.js y function.json expandiendo el menú Ver archivos en la parte derecha del panel de función de Azure Portal.

En este ejercicio se explicó como poner en funcionamiento nuestra infraestructura de Azure Functions. Tenemos una función en marcha hospedada en una aplicación de función que se ejecuta cuando llega un mensaje nuevo a la cola. La hemos llamado new-feedback-q. Lo divertido empieza en el siguiente ejercicio, en el que vamos a agregar código para llamar a Azure Cognitive Services a fin de realizar análisis de opiniones.

LLAMADA A TEXT ANALYTICS API DESDE UNA APLICACIÓN

Vamos a actualizar la implementación de la función para llamar al servicio Text Analytics API y obtener una puntuación de opinión.

1. Seleccione la función discover-sentiment-function, en nuestra aplicación de función en el portal.

2. Expanda el menú Ver archivos a la derecha de la pantalla.

3. En la pestaña Ver archivos, seleccione index.js para abrir el archivo de código en el editor y copie el contenido del archivo index.js

