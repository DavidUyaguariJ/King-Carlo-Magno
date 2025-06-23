# Proyecto Unity – Ejercicios de Visual Scripting, Persistencia y Comunicación con Hardware

Este proyecto fue desarrollado utilizando **Unity** y contiene ejercicios relacionados con **Visual Scripting**, **persistencia de datos** mediante una **API externa**, y **computación ubicua** mediante sensores físicos (ESP8266 + IMU).

## 📁 Estructura de ramas del repositorio

- 🔷 **`main`**: Contiene el ejercicio realizado con **Visual Scripting**.
- 🔶 **`persistance-example`**: Contiene el ejercicio de **checkpoint persistente con variación dinámica**, con conexión a una **API externa**.
- 🟠 **`comp-ubicua`**: Contiene el ejercicio de **computación ubicua**, utilizando un **ESP8266** y un **sensor IMU** para controlar el movimiento del personaje en Unity.

---

## Instrucciones para probar los ejercicios

### 🔷 Visual Scripting (`main`)

1. Cambia a la rama `main`.
2. Abre el proyecto con Unity.
3. En el **Panel de Proyecto**, navega a: `Assets/Scenes/Exercise`
4. Abre la escena llamada: **GraphScriptingExample**
5. Ejecuta el proyecto.

---

### 🔶 Checkpoint con Persistencia y API (`persistance-example`)

1. Cambia a la rama `persistance-example`.
2. Realiza un `git pull`.
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
  [`https://api.chucknorris.io/jokes/random`](https://api.chucknorris.io/jokes/random)
- Se obtiene un **chiste aleatorio de Chuck Norris**, el cual se muestra dinámicamente en un **panel del UI** en pantalla.

---

### Control de personaje con IMU y ESP8266 (`comp-ubicua`)

1. Cambia a la rama `comp-ubicua`.
2. Realiza un `git pull`.
3. Abre el proyecto con Unity.
4. En el **Panel de Proyecto**, navega a: `Assets/Scenes/Exercise`
5. Abre la escena correspondiente al ejemplo de computación ubicua.
6. Ejecuta el proyecto.
7. Asegúrate de tener el **ESP8266** conectado a la red local y enviando los datos del **sensor IMU** a Unity (vía puerto serial o comunicación por sockets).

#### Descripción del funcionamiento:

- Se utiliza un **ESP8266** junto con un **sensor IMU (por ejemplo MPU6050)** para detectar el movimiento de la placa.
- Los valores de inclinación se capturan y se envían a Unity.
- Unity interpreta esos datos para **mover un personaje en la escena** según la inclinación o aceleración detectada.

---

