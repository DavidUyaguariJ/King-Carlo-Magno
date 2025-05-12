# Proyecto Unity – Ejercicios de Visual Scripting y Persistencia

Este proyecto fue desarrollado utilizando **Unity**.

## Estructura de ramas del repositorio

- La rama **`main`** contiene el ejercicio realizado con **Visual Scripting**.
- La rama **`persistance-example`** contiene el ejercicio de **Checkpoint persistente con variación dinámica** mediante conexión a una **API externa**.

## Instrucciones para probar los ejercicios

### 🔷 Visual Scripting (rama `main`)

1. Cambia a la rama `main`.
2. Abre el proyecto con Unity.
3. En el **Panel de Proyecto**, navega a: `Assets/Scenes/Exercise`
4. Abre la escena llamada: **GraphScriptingExample**
5. Ejecuta el proyecto.

### 🔶 Checkpoint con Persistencia y API (rama `persistance-example`)

1. Cambia a la rama `persistance-example`.
2. Realizar un git pull
3. Abre el proyecto con Unity.
4. En el **Panel de Proyecto**, navega a: `Assets/Scenes/Exercise`
5. Abre la escena correspondiente al ejemplo de persistencia.
6. Ejecuta el proyecto.

#### Descripción del funcionamiento:

- Existen **dos objetos** en la escena que permiten **guardar el estado del jugador y la cámara**.
- Al guardar, se almacena:
  - La posición del jugador.
  - La posición de la cámara.
  - La hora de guardado.
- Después de guardar, el sistema realiza una solicitud a la **API pública**:
https://api.chucknorris.io/jokes/random

- Se obtiene un **chiste aleatorio de Chuck Norris**, el cual se muestra dinámicamente en un **panel del UI** en pantalla.

---
