# SC-701-Proyecto

## 👥 Colaboradores

| Nombre |
|---------|
| Alejandro Arguedas Araya |
|Alexander Torres Lopez |
| Brandon Aguirre Ortiz |
| Jose Daniel Hernandez Ureña |

1. **Clona el repositorio**
   
| https://github.com/AlexanderTL0343/TareasProgaAvanzadaWeb.git |

# 🧩 Especificación Básica del Proyecto — ControlClientes

## 🏗️ a. Arquitectura del Proyecto

El sistema **ControlClientes** está desarrollado bajo una arquitectura en **capas**, que promueve la separación de responsabilidades y facilita la escalabilidad y el mantenimiento del código.

**Estructura principal del proyecto:**
- **ControlClientes.Presentacion (MVC)**  
  - Proyecto principal en **ASP.NET Core MVC** encargado de las vistas, controladores y manejo de rutas.
  - Contiene las vistas Razor y archivos estáticos (CSS, JS, etc.).
  
- **ControlClientes.LogicaNegocio (LN)**  
  - Implementa las reglas de negocio y la validación de datos antes de acceder a la capa de datos.  
  - Interactúa con la capa de datos a través de interfaces y modelos DTO.

- **ControlClientes.AccesoDatos (DA)**  
  - Se encarga de la gestión de datos, simulando o conectándose a una base de datos.  
  - Contiene las operaciones CRUD básicas (crear, leer, actualizar, eliminar).

- **ControlClientes.Abstracciones (DTO / Interfaces)**  
  - Define las interfaces, modelos de transferencia de datos (DTO) y contratos de las capas.  
  - Facilita el acoplamiento débil entre las capas.

---

## 📦 b. Libraries / Paquetes NuGet Utilizados

| Paquete | Descripción |
|----------|-------------|
| **Microsoft.AspNetCore.Mvc** | Framework principal para el manejo del patrón MVC en ASP.NET Core. |
| **Microsoft.EntityFrameworkCore** | ORM utilizado para el acceso y manejo de datos (opcional, según implementación). |
| **AutoMapper** | Facilita la conversión entre entidades y DTOs. |
| **Swashbuckle.AspNetCore** | Generación automática de documentación Swagger para APIs (si aplica). |
| **Microsoft.Extensions.DependencyInjection** | Inyección de dependencias para la arquitectura en capas. |
| **Newtonsoft.Json** | Serialización y deserialización de objetos JSON. |

---

## 🧠 c. Principios SOLID y Patrones de Diseño Utilizados

### 🧱 Principios SOLID

1. **S — Single Responsibility Principle (SRP)**  
   Cada clase tiene una única responsabilidad.  
   Ejemplo: `ClienteDA` solo maneja el acceso a datos, mientras que `ClienteLN` gestiona la lógica de negocio.

2. **O — Open/Closed Principle (OCP)**  
   Las clases están abiertas para extensión pero cerradas para modificación.  
   Ejemplo: las operaciones de clientes se pueden extender creando nuevas implementaciones sin modificar el código existente.

3. **L — Liskov Substitution Principle (LSP)**  
   Las clases derivadas pueden reemplazar a sus clases base sin alterar el comportamiento del programa.

4. **I — Interface Segregation Principle (ISP)**  
   Se crean interfaces específicas y enfocadas, evitando depender de métodos que no se utilizan.

5. **D — Dependency Inversion Principle (DIP)**  
   Las capas de alto nivel no dependen de las de bajo nivel; ambas dependen de abstracciones.  
   Ejemplo: `ClienteLN` depende de una interfaz `IClienteDA` y no de una implementación concreta.

---

### ⚙️ Patrones de Diseño Implementados

| Patrón | Descripción |
|--------|-------------|
| **Repository Pattern** | Separa la lógica de acceso a datos de la lógica de negocio, permitiendo intercambiar fuentes de datos fácilmente. |
| **Dependency Injection (DI)** | Facilita la inversión de dependencias y el acoplamiento débil entre las capas. |
| **DTO (Data Transfer Object)** | Transfiere datos entre capas sin exponer las entidades internas. |
| **Factory (opcional)** | Puede emplearse para crear instancias de objetos complejos en la capa de lógica de negocio. |

---
