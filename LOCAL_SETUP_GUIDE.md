# Guía para ejecutar la aplicación en entorno local

## Pasos previos
Antes de comenzar, asegúrese de tener instalados los siguientes elementos:
- **VS Code** (o el editor de su preferencia)
- **Git**
- **.NET SDK**
- **Node.js**

## Clonar el repositorio
Ejecute el siguiente comando en la ruta de su preferencia:

git clone https://github.com/omazing-dev/properties-app.git


## Backend
1. Abra **VS Code** y abra la carpeta del repositorio clonado.
2. En la terminal integrada, desplácese al directorio `src/backend`:
 
   cd src/backend
 
3. Restaure los paquetes de .NET:

   dotnet restore
  
4. Ahora desplacese a la carpeta `PropertyApp.Web`:
  
   cd PropertyApp.Web
   
5. Ejecute el backend:
   
   dotnet run
   

### Nota importante sobre la base de datos
En caso de **no tener acceso a la base de datos/clúster de MongoDB existente**, deberá crear una nueva instancia y modificar los parámetros de conexión en el archivo `appsettings.json` del proyecto `PropertyApp.Web`.  
Cambie:
- Usuario
- Contraseña
- Nombre de la base de datos

## Frontend
1. En **VS Code**, abra la carpeta del repositorio.
2. En la terminal integrada, desplácese al directorio `src/frontend/property-ui`:
   
   cd src/frontend/property-ui
   
3. Instale las dependencias de npm:
   
   npm install
  
4. Instale Vite como dependencia de desarrollo:
  
   npm install vite --save-dev
  
5. Ejecute el frontend:
  
   npm run dev
   


Con esto, tendrá tanto el **backend** como el **frontend** corriendo en su entorno local.
