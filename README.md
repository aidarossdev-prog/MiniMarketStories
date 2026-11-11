# 🛒 Mini Market Stories

**Mini Market Stories** es un videojuego tridimensional desarrollado en Unity, ambientado en un pequeño minimercado con una estética *cozy*, colores pastel y elementos visuales de estilo *low-poly*.

El objetivo del jugador es recolectar productos distribuidos por la tienda y acomodarlos en los anaqueles correspondientes, completando diferentes niveles y acumulando puntos.

A medida que avanza la partida, aumenta la cantidad de productos que deben entregarse y disminuye el tiempo disponible, por lo que el jugador deberá completar cada pedido antes de que termine la cuenta regresiva.

Este proyecto se desarrolla como parte de las actividades académicas de la **Licenciatura en Desarrollo de Sistemas Web de la Universidad de Guadalajara**.

## 🎮 Características principales

- Entorno tridimensional con un minimercado interactivo.
- Personaje controlable mediante teclado o joystick virtual.
- Movimiento en los ejes X y Z, con salto en el eje Y.
- Cámara que sigue automáticamente al personaje.
- Menú principal con opciones para jugar, consultar instrucciones y salir.
- Recolección automática de productos mediante proximidad.
- Transporte de un producto a la vez.
- Entrega automática de productos en los anaqueles correspondientes.
- Generación dinámica de niveles mediante prefabs.
- Aparición aleatoria de productos en puntos predefinidos del escenario.
- Sistema de niveles con dificultad progresiva.
- Alternancia entre latas y botellas de agua según el nivel.
- Incremento progresivo de la cantidad de productos.
- Temporizador con reducción progresiva del tiempo disponible.
- Contador de productos entregados.
- Sistema de puntuación acumulativa.
- Condición de derrota cuando se agota el tiempo.
- Pantalla Game Over con opciones para reintentar y regresar al menú principal.
- Reinicio de partida desde el nivel 1.
- Almacenamiento local del nivel y la puntuación mediante PlayerPrefs.
- Interfaz adaptada a orientación horizontal para dispositivos móviles.

## 🏪 Mecánica de juego

El jugador debe recorrer el minimercado para localizar los productos correspondientes al nivel actual.

Al acercarse a un producto, el personaje lo recoge automáticamente y puede transportarlo hasta el anaquel asignado.

Cuando ingresa a la zona de entrega, el producto se coloca sobre el anaquel, se actualiza el contador y se agregan **10 puntos** a la puntuación total.

El jugador únicamente puede transportar un producto a la vez.

Para completar un nivel, deberá entregar todos los productos solicitados antes de que termine el tiempo disponible.

Cuando completa el pedido, se genera automáticamente el siguiente nivel, con una nueva distribución de productos y una dificultad mayor.

Si el temporizador llega a cero, aparece la pantalla **Game Over**, desde la cual el jugador puede reiniciar la partida desde el primer nivel o regresar al menú principal.

## 🔄 Generación dinámica de niveles

El videojuego incorpora un sistema de generación dinámica que permite crear nuevos niveles durante la ejecución, sin necesidad de diseñar una escena diferente para cada uno.

Este sistema utiliza prefabs de productos y puntos de aparición previamente distribuidos dentro del minimercado.

Al comenzar cada nivel:

1. Se eliminan los productos generados del nivel anterior.
2. Se calcula la cantidad de productos correspondiente al nuevo nivel.
3. Se selecciona el tipo de producto que deberá recolectarse.
4. Se eligen aleatoriamente los puntos de aparición disponibles.
5. Se generan los productos en el escenario.
6. Se reinicia el contador de entregas y se asigna el tiempo disponible.

Actualmente, el sistema cuenta con **12 puntos de aparición** y utiliza dos tipos de productos:

- **Latas:** niveles impares.
- **Botellas de agua:** niveles pares.

Los productos se generan en posiciones aleatorias, evitando utilizar dos veces el mismo punto de aparición dentro de un mismo nivel.

La generación se realiza durante la ejecución mediante scripts desarrollados en C#, lo que permite continuar creando niveles sin establecer un número máximo de etapas.

## 📈 Sistema de dificultad progresiva

La dificultad aumenta mediante dos parámetros principales:

- **Cantidad de productos:** aumenta conforme avanza el jugador.
- **Tiempo disponible:** disminuye progresivamente en cada nivel.

Ambos parámetros pueden modificarse desde el Inspector de Unity.

### Configuración actual

| Parámetro | Valor |
|---|---:|
| Productos iniciales | 3 |
| Incremento de productos por nivel | 2 |
| Máximo de productos simultáneos | 12 |
| Tiempo inicial | 90 segundos |
| Reducción de tiempo por nivel | 5 segundos |
| Tiempo mínimo | 30 segundos |
| Puntos por producto entregado | 10 |

### Ejemplo de progresión

| Nivel | Producto | Cantidad | Tiempo disponible | Puntos posibles |
|---|---|---:|---:|---:|
| 1 | Latas | 3 | 90 s | 30 |
| 2 | Botellas de agua | 5 | 85 s | 50 |
| 3 | Latas | 7 | 80 s | 70 |
| 4 | Botellas de agua | 9 | 75 s | 90 |
| 5 | Latas | 11 | 70 s | 110 |
| 6 | Botellas de agua | 12 | 65 s | 120 |
| 7 | Latas | 12 | 60 s | 120 |
| 8 | Botellas de agua | 12 | 55 s | 120 |

A partir del nivel 6, la cantidad de productos se mantiene en un máximo de 12, debido al número de puntos de aparición disponibles.

Sin embargo, el tiempo continúa disminuyendo hasta alcanzar el mínimo configurado de 30 segundos.

Esto permite mantener una dificultad progresiva sin aumentar indefinidamente la cantidad de objetos presentes en el escenario.

## ⏱️ Temporizador y condición de derrota

Cada nivel cuenta con un temporizador que muestra el tiempo restante en formato de minutos y segundos.

El tiempo disponible se calcula mediante la siguiente fórmula:

**Tiempo = Tiempo inicial − ((Nivel − 1) × Reducción por nivel)**

Para evitar que los niveles sean imposibles de completar por una reducción indefinida del tiempo, se establece un límite mínimo configurable.

El temporizador utiliza `Time.deltaTime`, lo que permite realizar la cuenta regresiva independientemente de la cantidad de fotogramas por segundo.

Cuando el tiempo llega a cero:

- Se activa la condición de derrota.
- El temporizador se detiene.
- Se muestra la pantalla **¡TIEMPO AGOTADO!**
- El jugador puede seleccionar **REINTENTAR** o **MENÚ PRINCIPAL**.

### Reinicio de partida

La opción **REINTENTAR** permite comenzar nuevamente desde el nivel 1.

Al reiniciar:

- Se restablece el nivel inicial.
- La puntuación vuelve a cero.
- Se reinicia el contador de productos entregados.
- Se eliminan los productos generados anteriormente.
- Se generan nuevos productos.
- Se restablece el temporizador a 90 segundos.
- Se oculta la pantalla de derrota.

## 💾 Persistencia de datos

El proyecto utiliza **PlayerPrefs** de Unity para almacenar información básica del progreso del jugador.

Actualmente, el sistema dinámico guarda:

- Nivel alcanzado.
- Puntuación acumulada.

Al iniciar nuevamente el juego, estos valores pueden recuperarse y se genera el nivel correspondiente con sus productos iniciales.

La cantidad de productos entregados dentro de un nivel no se conserva al cerrar el juego, por lo que el pedido comienza nuevamente desde cero.

También se implementó una función para reiniciar el progreso almacenado y comenzar una nueva partida desde el nivel 1.

**Nota:** El sistema de persistencia individual de productos desarrollado en una etapa anterior permanece como parte del proyecto, pero la generación dinámica actual utiliza un sistema de administración de productos diferente.

## 🕹️ Controles

| Acción | Computadora | Dispositivo móvil |
|---|---|---|
| Mover personaje | W, A, S, D o flechas | Joystick virtual |
| Saltar | Barra espaciadora | Botón SALTAR |
| Recoger productos | Acercarse al producto | Acercarse al producto |
| Entregar productos | Acercarse al anaquel | Acercarse al anaquel |
| Regresar al menú | Botón MENÚ | Botón MENÚ |
| Reintentar partida | Botón REINTENTAR | Botón REINTENTAR |

## 🧩 Escenas del proyecto

### MainMenu

Pantalla principal del videojuego. Permite iniciar la partida, consultar las instrucciones y salir.

### HowToPlay

Pantalla informativa que explica los controles y las acciones básicas del jugador.

### MiniMarket

Escenario principal donde se encuentran el personaje, los anaqueles y los productos.

En esta escena se ejecutan las mecánicas de recolección, entrega, generación dinámica de niveles, puntuación, temporizador y condición de derrota.

## ⚙️ Tecnologías utilizadas

- **Motor de desarrollo:** Unity 6 (6000.5.9f1).
- **Lenguaje de programación:** C#.
- **Renderizado:** Universal Render Pipeline (URP).
- **Interfaz:** Unity UI y TextMeshPro.
- **Controles:** Input System y joystick virtual.
- **Persistencia:** PlayerPrefs.
- **Control de versiones:** Git y GitHub.
- **Plataforma objetivo:** Android.

## 📂 Scripts principales

| Script | Función |
|---|---|
| `MainMenuManager.cs` | Controla la navegación entre escenas. |
| `PlayerMovement.cs` | Gestiona el desplazamiento y salto del personaje. |
| `CameraFollow.cs` | Permite que la cámara siga al personaje. |
| `VirtualJoystick.cs` | Gestiona el movimiento mediante joystick virtual. |
| `GameManager.cs` | Administra los niveles, la puntuación, el temporizador, la condición de derrota y el reinicio de partida. |
| `LevelGenerator.cs` | Genera dinámicamente los productos y calcula la cantidad correspondiente a cada nivel. |
| `GeneratedProduct.cs` | Identifica los productos generados y registra su nivel y estado de entrega. |
| `ProductCollector.cs` | Controla la recolección y el transporte de productos. |
| `ProductDelivery.cs` | Gestiona la entrega y colocación de productos en los anaqueles correspondientes. |
| `ProductPersistence.cs` | Script de persistencia individual utilizado durante la segunda etapa del desarrollo. |

## 🚀 Ejecución del proyecto

1. Clonar o descargar el repositorio.
2. Abrir Unity Hub.
3. Seleccionar **Add project from disk**.
4. Elegir la carpeta del proyecto.
5. Abrirlo utilizando Unity 6.
6. Abrir la escena `MainMenu` desde `Assets/Scenes`.
7. Presionar **Play** para iniciar el videojuego.

## 📌 Estado del desarrollo

### Primera etapa: Interfaz y movimiento 3D

- [x] Creación del escenario tridimensional.
- [x] Implementación de menús.
- [x] Movimiento del personaje.
- [x] Seguimiento de cámara.
- [x] Controles mediante teclado y joystick.
- [x] Botón de salto.
- [x] Navegación entre escenas.

### Segunda etapa: Dinámicas y persistencia

- [x] Recolección de productos.
- [x] Entrega automática en anaqueles.
- [x] Sistema de puntuación.
- [x] Contador de progreso.
- [x] Implementación de dos niveles iniciales.
- [x] Guardado mediante PlayerPrefs.
- [x] Recuperación del progreso.
- [x] Persistencia individual de productos en la implementación inicial.

### Tercera etapa: Generación dinámica y ciclo de juego

- [x] Creación de prefabs de productos.
- [x] Configuración de 12 puntos de aparición.
- [x] Generación dinámica de productos durante la ejecución.
- [x] Selección aleatoria de posiciones.
- [x] Alternancia de productos según el nivel.
- [x] Generación continua de nuevos niveles.
- [x] Incremento progresivo de productos.
- [x] Configuración de parámetros de dificultad desde el Inspector.
- [x] Implementación de temporizador por nivel.
- [x] Reducción progresiva del tiempo disponible.
- [x] Visualización de cuenta regresiva.
- [x] Implementación de condición de derrota.
- [x] Creación de pantalla Game Over.
- [x] Opción para reiniciar la partida desde el nivel 1.
- [x] Opción para regresar al menú principal.

### Próximas etapas

- [ ] Incorporación de nuevos tipos de productos.
- [ ] Ampliación de las dinámicas de juego.
- [ ] Mejoras visuales y de interacción.
- [ ] Optimización para dispositivos móviles.
- [ ] Ajustes adicionales al sistema de progreso y reinicio.
- [ ] Pruebas de jugabilidad y rendimiento.

## 👩‍💻 Autora

**Aida Rosas Romero**  
Diseño de Juegos II  
Licenciatura en Desarrollo de Sistemas Web  
Universidad de Guadalajara

**Proyecto académico desarrollado con Unity y C#.**