# 🚀 SGE — Sistema de Gestión de Expedientes

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-Programming-239120?style=for-the-badge&logo=c-sharp)
![Architecture](https://img.shields.io/badge/Clean%20Architecture-Implemented-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-In%20Development-success?style=for-the-badge)

API REST desarrollada con **ASP.NET Core** siguiendo los principios de **Clean Architecture** para la administración de expedientes, trámites y usuarios. El sistema implementa autenticación mediante **JWT**, control de permisos y documentación interactiva utilizando **Scalar**.

</div>

---

# 📖 Descripción

El Sistema de Gestión de Expedientes (SGE) permite administrar expedientes y los trámites asociados a ellos de manera centralizada. La aplicación expone una API REST que facilita la gestión de usuarios, la autenticación y las operaciones sobre los expedientes, manteniendo una arquitectura desacoplada y fácil de mantener.

El proyecto fue desarrollado utilizando una arquitectura desacoplada y organizada en capas, permitiendo:

- Mayor mantenibilidad
- Escalabilidad
- Reutilización de código
- Separación clara de responsabilidades
- Facilidad para testing y evolución futura

El sistema implementa casos de uso independientes, repositorios y persistencia desacoplada mediante infraestructura propia.

---

## ✨ Funcionalidades 
- 🔐 Autenticación mediante JWT. 
- 👥 Gestión de usuarios. 
- 📁 Administración de expedientes. 
- 📄 Gestión de trámites asociados a un expediente. 
- 🛡️ Control de permisos según el usuario autenticado. 
- ✅ Validaciones de reglas de negocio. 
- 📚 Documentación interactiva de la API mediante Scalar.

## 🛠️ Tecnologías utilizadas 
- ASP.NET Core 
- C# 
- Minimal API 
- JWT Authentication 
- Scalar 
- Clean Architecture

# 🏗️ Arquitectura

El proyecto está organizado siguiendo el patrón **Clean Architecture**, separando las responsabilidades en cuatro capas principales:
```text 
SGE 
│
├── SGE.WebApi # Endpoints, configuración y autenticación 
├── SGE.Aplicacion # Casos de uso y lógica de aplicación 
├── SGE.Dominio # Entidades, reglas de negocio y contratos 
└── SGE.Infraestructura # Persistencia y acceso a datos 
```

Esta estructura permite desacoplar la lógica de negocio de la infraestructura, facilitando el mantenimiento, la escalabilidad y la incorporación de nuevas funcionalidades.

---

## 🚀 Ejecución del proyecto 

1. Clonar el repositorio. 
```bash
 git clone <https://github.com/EOA2020/SGE.git> 
``` 
 2. Restaurar las dependencias. 
 ```bash 
dotnet restore 
``` 
3. Ejecutar la aplicación. 
```bash 
dotnet run --project SGE.WebApi
```

---
# ✨ Características

## 📌 Funcionalidades actuales

- ✅ Gestión de expedientes
- ✅ Gestión de trámites
- ✅ Persistencia de datos
- ✅ Arquitectura por capas
- ✅ Casos de uso desacoplados
- ✅ Repository Pattern
- ✅ Validaciones de negocio
- ✅ Separación entre dominio e infraestructura
- ✅ Gestión de usuarios
- ✅ Minimal WebApi

---

# 🧠 Conceptos Aplicados

## 🏛️ Clean Architecture

El proyecto separa responsabilidades entre capas para lograr:

- Bajo acoplamiento
- Alta cohesión
- Escalabilidad
- Facilidad de testing
- Independencia de frameworks

---

## 📦 Repository Pattern

El proyecto implementa el patrón **Repository** para desacoplar la lógica de negocio del mecanismo de persistencia de datos. De esta manera, los casos de uso interactúan con interfaces en lugar de depender directamente de una base de datos o de una tecnología específica de almacenamiento.

Esta abstracción facilita el mantenimiento del código, mejora la capacidad de realizar pruebas y permite cambiar la implementación de la persistencia sin afectar la lógica de la aplicación.

### Interfaces principales

```csharp
IExpedienteRepository   // Gestión de expedientes
ITramiteRepository      // Gestión de trámites
IUsuariosRepository     // Gestión de usuarios
IUnidadDeTrabajo        // Coordinación de transacciones
ITimeProvider           // Obtención de fecha y hora desacoplada del sistema
```

Cada repositorio define únicamente las operaciones necesarias para su entidad, mientras que su implementación concreta se encuentra en la capa de **Infraestructura**.

---

## ⚡ Casos de Uso

La lógica de negocio de la aplicación se encuentra organizada en **casos de uso**, donde cada clase representa una acción específica que el sistema puede realizar. Esta organización permite mantener cada operación independiente, facilitando su mantenimiento, reutilización y prueba.

Los casos de uso se encargan de validar las reglas de negocio, interactuar con los repositorios necesarios y devolver el resultado de la operación, sin depender de detalles de infraestructura o de la capa de presentación.

### Ejemplos

```text
- CrearExpedienteUseCase
- EliminarExpedienteUseCase
- CrearTramiteUseCase
- RegistrarUsuarioUseCase
- LoginUseCase
```

Cada caso de uso se centra en una única responsabilidad, siguiendo el principio de responsabilidad única (**Single Responsibility Principle**) y favoreciendo una arquitectura modular y desacoplada.

---

## Abrir la solución

Abrir el proyecto utilizando:

- Visual Studio
- Rider
- VS Code + extensión C#

---

# 📚 Objetivos del Proyecto

Este proyecto busca aplicar:

- Arquitectura limpia
- Principios SOLID
- Programación orientada a objetos
- Separación de responsabilidades
- Modelado de dominio
- Persistencia desacoplada

---

# 👥 Equipo de Desarrollo

Este proyecto fue desarrollado por:

- 👨‍💻 [**Sergio Ariel Paredes**](https://github.com/EOA2020)
- 👩‍💻 [**Cristal Milagros Andrade**](https://github.com/andradecristal)
- 👨‍💻 [**Elias Nahuel Lopez**](https://github.com/nauelo)

Proyecto desarrollado con fines académicos y de aprendizaje.


## Repositorio oficial

👉 https://github.com/EOA2020/SGE.git

---

# 📄 Licencia

Proyecto de uso educativo y académico.

---

<div align="center">

### ⭐ Si te gusta el proyecto, podés darle una estrella al repositorio ⭐

</div>