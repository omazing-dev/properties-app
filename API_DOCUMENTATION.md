# API de Propiedades

Este documento describe los endpoints disponibles para la gestión de propiedades en la API.

##  Base URL
http://dominio-o-localhost/api/properties


## **1. Obtener todas las propiedades**
**GET** `/api/properties`

### Descripción
Obtiene la lista completa de propiedades registradas.  
Permite aplicar filtros opcionales.


Los filtros pueden combinarse.

### Ejemplo de request

GET /api/properties?name=casa&minPrice=100000&maxPrice=300000


### Ejemplo de respuesta
{
  "items": [
    {
      "id": "689d0bb43036eaf6d7f82b76",
      "name": "Propiedad 1",
      "address": "Av. Principal 5, Ciudad 2",
      "price": 456919,
      "idOwner": "689d0bb43036eaf6d7f82b6b",
      "image": "https://images.unsplash.com/photo-1570129477492-45c003edd2be?auto=format&fit=crop&w=1200&q=60"
    }
  ],
  "total": 15,
  "page": 1,
  "pageSize": 1
}
## **2. Obtener propiedad por ID**
**GET** `/api/properties/{id}`

### Descripción
Obtiene el detalle completo de una propiedad específica.


### Ejemplo de request

GET /api/properties/689cb527e66cf44e157896d2

### Ejemplo de respuesta
{
  "id": "689d0bb43036eaf6d7f82b76",
  "name": "Propiedad 1",
  "address": "Av. Principal 5, Ciudad 2",
  "price": 456919,
  "year": 2017,
  "codeInternal": "PR-001",
  "ownerName": "Owner 5",
  "mainImage": "https://images.unsplash.com/photo-1570129477492-45c003edd2be?auto=format&fit=crop&w=1200&q=60"
}

