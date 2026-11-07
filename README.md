# 🛒 Mini Market Stories

**Mini Market Stories** es un videojuego tridimensional desarrollado en Unity, ambientado en un pequeño minimercado con una estética *cozy*, colores pastel y elementos visuales de estilo *low-poly*.

El objetivo del jugador es recolectar productos distribuidos por la tienda y acomodarlos en los anaqueles correspondientes, completando diferentes niveles y acumulando puntos.

Este proyecto se desarrolla como parte de las actividades académicas de la **Licenciatura en Desarrollo de Sistemas Web de la Universidad de Guadalajara**.

## 🎮 Características principales

- Entorno tridimensional con un minimercado interactivo.
- Personaje controlable mediante teclado o joystick virtual.
- Movimiento en los ejes X y Z, con salto en el eje Y.
- Cámara que sigue automáticamente al personaje.
- Menú principal con opciones para jugar, consultar instrucciones y salir.
- Recolección de productos mediante proximidad.
- Transporte de un producto a la vez.
- Entrega automática de productos en los anaqueles correspondientes.
- Sistema de niveles con dificultad progresiva.
- Contador de productos entregados.
- Sistema de puntuación acumulativa.
- Guardado y recuperación del progreso mediante PlayerPrefs.
- Interfaz adaptada a una orientación horizontal para dispositivos móviles.

## 🏪 Mecánica de juego

El jugador debe recorrer el minimercado para localizar los productos correspondientes al nivel actual.

Al acercarse a un producto, el personaje lo recoge automáticamente y puede transportarlo hasta el anaquel asignado.

Cuando ingresa a la zona de entrega, el producto se coloca sobre el anaquel, se actualiza el contador y se agregan **10 puntos** a la puntuación total.

El jugador únicamente puede transportar un producto a la vez.

## 📈 Sistema de niveles y dificultad progresiva

La dificultad aumenta mediante la cantidad de productos que deben recolectarse y su distribución dentro del escenario.

### Niveles implementados

| Nivel | Producto | Cantidad | Puntos posibles | Estado |
|---|---|---:|---:|---|
| 1 | Latas | 3 | 30 | Implementado |
| 2 | Botellas de agua | 5 | 50 | Implementado |

Al completar ambos niveles, el jugador acumula **80 puntos**.

### Niveles contemplados para futuras etapas

| Nivel | Producto | Cantidad prevista |
|---|---|---:|
| 3 | Jugos | 7 |
| 4 | Leche | 10 |
| 5 | Galletas | 12 |

El sistema está preparado para ampliar las dinámicas de juego y agregar nuevos productos, escenarios y funcionalidades en futuras entregas.

**Nota:** Actualmente, los niveles 1 y 2 cuentan con productos y mecánicas funcionales. Los niveles posteriores se encuentran configurados como parte de la planificación del proyecto, pero todavía no tienen implementados sus productos.

## 💾 Persistencia de datos

Se implementó un sistema de almacenamiento local utilizando **PlayerPrefs** de Unity.

Los datos almacenados incluyen:

- Nivel actual del jugador.
- Cantidad de productos entregados.
- Puntuación acumulada.
- Estado individual de los productos entregados.

Al reiniciar el juego, se recuperan los datos almacenados y se restauran los productos previamente acomodados en los anaqueles.

Esto permite continuar la partida sin perder el progreso alcanzado.

## 🕹️ Controles

| Acción | Computadora | Dispositivo móvil |
|---|---|---|
| Mover personaje | W, A, S, D o flechas | Joystick virtual |
| Saltar | Barra espaciadora | Botón SALTAR |
| Recoger productos | Acercarse al producto | Acercarse al producto |
| Entregar productos | Acercarse al anaquel | Acercarse al anaquel |
| Regresar al menú | Botón MENÚ | Botón MENÚ |

## 🧩 Escenas del proyecto

**MainMenu**

Pantalla principal del videojuego. Permite iniciar la partida, consultar las instrucciones y salir.

**HowToPlay**

Pantalla informativa que explica los controles y las acciones básicas del jugador.

**MiniMarket**

Escenario principal donde se encuentran el personaje, los anaqueles, los productos y las dinámicas de recolección y entrega.

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
| `GameManager.cs` | Administra niveles, puntuación y progreso. |
| `ProductCollector.cs` | Controla la recolección de productos. |
| `ProductDelivery.cs` | Gestiona la entrega y colocación de productos en los anaqueles. |
| `ProductPersistence.cs` | Almacena y consulta el estado individual de los productos. |

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
- [x] Dificultad progresiva.
- [x] Dos niveles funcionales.
- [x] Guardado mediante PlayerPrefs.
- [x] Recuperación del progreso.

### Próximas etapas
- [ ] Incorporación de nuevos productos.
- [ ] Implementación de niveles adicionales.
- [ ] Ampliación de las dinámicas de juego.
- [ ] Mejoras visuales y de interacción.
- [ ] Optimización para dispositivos móviles.

## 👩‍💻 Autora

**Aida Rosas Romero**  
Diseño de Juegos II
Licenciatura en Desarrollo de Sistemas Web  
Universidad de Guadalajara

**Proyecto académico desarrollado con Unity y C#.**