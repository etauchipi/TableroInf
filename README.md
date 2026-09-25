# TableroInf (Tablero de Información Hospitalario)

## Descripción
**TableroInf** es una aplicación web ASP.NET (Web Forms) desarrollada en VB.NET que funciona como un tablero de control e información hospitalaria. La solución está diseñada para monitorear el estado de los pacientes en diferentes áreas de urgencias (tales como Urgencias Ginecoobstétricas, Urgencias Pediátricas y Urgencias de Adultos). Se integra con un servicio WCF externo (`wsServinte`) para consultar, importar y actualizar en tiempo real los datos del paciente, estados de interconsulta, resultados de laboratorio clínico, imagenología y el destino final del paciente.

La interfaz proporciona un tablero visual donde se destacan, mediante códigos de color (Rojo, Verde, Amarillo, etc.), el estado de diferentes exámenes médicos y procesos, permitiendo a los profesionales de salud tomar decisiones rápidas.

## Estructura del Proyecto

El repositorio sigue la estructura clásica de un proyecto Web Forms en ASP.NET. A continuación, las carpetas y archivos más relevantes:

- **`/App_Data/`**: Contiene bibliotecas compiladas (`.dll`) externas necesarias para el proyecto (`Compresion.dll` y el proxy `wsServinteProxy.dll`).
- **`/App_Start/`**: Contiene la configuración de inicialización (como `BundleConfig.vb` y `RouteConfig.vb` para las rutas amigables y optimización de recursos).
- **`/Content/`**: Archivos de estilos (CSS) incluyendo el framework Bootstrap para el diseño responsivo de la interfaz.
- **`/Scripts/`**: Archivos de JavaScript, incluyendo librerías de cliente como jQuery, Modernizr y scripts del sistema ASP.NET Web Forms.
- **`/fonts/`**: Fuentes para los iconos de Bootstrap (Glyphicons).
- **`/Default*.aspx`**: Archivos de las páginas principales del tablero. Cada versión representa distintas vistas y actualizaciones de la información hospitalaria:
  - `Default.aspx`: Vista principal general.
  - `DefaultUG.aspx`: Tablero de Urgencias Ginecoobstétricas.
  - `DefaultUP.aspx`: Tablero de Urgencias Pediátricas.
  - `DefaultUR.aspx`: Tablero de Urgencias Adultos.
  - `DefaultCrg.aspx`: Vista de carga o proceso batch para actualizar y exportar datos.
- **`Web.config`**: Archivo de configuración principal de ASP.NET, que contiene configuraciones de compilación, enrutamiento y especialmente los *endpoints* del servicio WCF (`wsServinte`).

## Prerrequisitos Técnicos

Para ejecutar, modificar y compilar este proyecto en un entorno local, se requieren las siguientes herramientas instaladas:

1. **IDE:** Microsoft Visual Studio (versión 2013, 2015, 2017, 2019 o 2022) con las cargas de trabajo de desarrollo de "ASP.NET y web".
2. **Framework:** .NET Framework 4.5.
3. **Servidor Web:** IIS Express (incluido con Visual Studio) o Internet Information Services (IIS) habilitado en el sistema operativo.
4. **Conexión de red:** Acceso a la red interna donde se aloja el servicio WCF (`wsServinte`).

## Instalación y Configuración Local

Sigue estos pasos para configurar el proyecto en tu entorno de desarrollo local:

1. **Clonar el repositorio:**
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd TableroInf
   ```

2. **Abrir la solución:**
   Abre el archivo `TableroInf.sln` utilizando Microsoft Visual Studio.

3. **Restaurar paquetes NuGet:**
   El proyecto utiliza paquetes NuGet definidos en el archivo `packages.config`.
   En Visual Studio, haz clic derecho sobre la Solución en el Explorador de Soluciones y selecciona **"Restaurar paquetes NuGet"**.

4. **Configuración de Variables de Entorno / Endpoints WCF:**
   Abre el archivo `TableroInf/Web.config`. Dirígete a la sección `<system.serviceModel> / <client>` y actualiza la URL del endpoint para apuntar a la dirección IP correcta de tu servidor WCF.
   Ejemplo:
   ```xml
   <endpoint address="http://TU_IP:PUERTO/wsservinte/wsservinte.svc"
             binding="basicHttpBinding" bindingConfiguration="MainBnd" contract="IwsServinte"
             name="Basic" />
   ```

## Ejecución del Proyecto

### Modo de Desarrollo
Puedes ejecutar el proyecto directamente desde Microsoft Visual Studio usando IIS Express:
1. Asegúrate de tener seleccionada la configuración `Debug` en Visual Studio.
2. Presiona `F5` o el botón "Iniciar" en la barra de herramientas. Visual Studio iniciará IIS Express y abrirá el proyecto en tu navegador web predeterminado.

### Modo de Producción
Para desplegar en un entorno de producción (servidor con IIS):
1. Selecciona la configuración `Release` en Visual Studio.
2. Haz clic derecho sobre el proyecto `TableroInf` (no la solución) y selecciona **Publicar**.
3. Selecciona tu método de publicación (Carpeta, Web Deploy, etc.).
4. Si lo publicas en una carpeta local, copia el contenido de esa carpeta al directorio base de un nuevo Sitio Web en el Administrador de IIS de tu servidor.
5. Asegúrate de que el "Application Pool" del sitio en IIS esté configurado con el **.NET Framework v4.0** (compatible con 4.5) y en modo Clásico/Integrado según tu configuración.

## Ejecución de Pruebas (Tests)

Actualmente, el repositorio no contiene un proyecto de pruebas unitarias asociado (`.Tests` o similar). Todo el flujo lógico está integrado directamente en el "code-behind" de las páginas ASPX (ej. `Default.aspx.vb`).

Para probar la lógica de las pantallas y la conexión, debes ejecutar el proyecto en "Modo de Desarrollo" (F5) y verificar el correcto renderizado de los datos cargados desde el servicio WCF.

*Si planeas agregar pruebas unitarias en un futuro, se recomienda extraer la lógica de datos (`Renderiza1()`) a una biblioteca de clases externa y utilizar herramientas de testing como MSTest, NUnit o xUnit.*