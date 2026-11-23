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
- Potenciador de velocidad temporal con efecto visual Trail Renderer.
- Aparición aleatoria de un potenciador por nivel.

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
| `SpeedBoostPickup.cs` | Detecta la recolección del potenciador y activa el aumento temporal de velocidad. |

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

### Etapa 4. Iluminación y personalización visual

En esta etapa se implementaron mejoras en la iluminación y apariencia del videojuego **Mini Market Stories**, con el propósito de crear un entorno visual más atractivo y dinámico.

### Iluminación del escenario

Se configuró una luz direccional en tiempo real para mejorar la iluminación general del minimercado. También se ajustaron sus propiedades y los parámetros del entorno para mantener una apariencia visual agradable durante la partida.

### Cambio dinámico de ambientes

Se desarrolló el script `EnvironmentColorManager.cs`, que permite modificar el color de fondo de la cámara y la iluminación principal.

Se implementaron tres ambientes:

- **Mañana:** iluminación cálida.
- **Atardecer:** iluminación con tonalidades rosadas.
- **Noche:** iluminación con tonalidades azuladas.

Los ambientes cambian automáticamente cada dos niveles y se repiten de manera cíclica. También es posible alternarlos manualmente mediante la tecla **C** para realizar pruebas.

### Personalización del personaje

Se desarrolló el script `PlayerAppearance.cs`, que permite modificar las propiedades visuales del personaje.

Las opciones implementadas incluyen:

- Cambio entre tres colores.
- Ajuste de la propiedad *Metallic*.
- Ajuste de la propiedad *Smoothness*.
- Cambio manual de color mediante la tecla **V**.

### Mejoras visuales adicionales

Se revisaron los efectos de posprocesamiento del escenario y se desactivó el efecto *Bloom* para evitar un resplandor excesivo durante la partida.

### Resultado

Las mejoras implementadas permiten que el minimercado presente diferentes ambientes visuales conforme el jugador avanza de nivel, mientras que el personaje cuenta con opciones de personalización de color y apariencia.

Estas modificaciones se integraron sin alterar las mecánicas principales de recolección, entrega de productos y progresión de niveles.

## Etapa 5. Interfaces de usuario y navegación

En esta etapa se revisaron y validaron las interfaces de usuario del videojuego **Mini Market Stories**, con el objetivo de garantizar una navegación funcional entre las diferentes pantallas y permitir que el jugador interactúe correctamente con la aplicación.

### Menú principal

Se cuenta con una interfaz desarrollada mediante Canvas, que incluye el título del videojuego, un fondo visual y tres botones principales:

- **Jugar:** permite iniciar la partida y acceder al minimercado.
- **Cómo jugar:** muestra las instrucciones y mecánicas del videojuego.
- **Salir:** permite cerrar la aplicación.

### Navegación entre escenas

La navegación se administra mediante el script `MainMenuManager.cs`, utilizando la clase `SceneManager` de Unity.

Se implementaron y comprobaron las siguientes transiciones:

- `MainMenu → MiniMarket`
- `MainMenu → HowToPlay`
- `HowToPlay → MainMenu`
- `MiniMarket → MainMenu`

Estas transiciones permiten que el jugador se desplace entre las pantallas principales del videojuego.

### Interfaz durante la partida

La escena `MiniMarket` cuenta con elementos de interfaz que muestran información importante para el jugador, como el nivel actual, el tiempo disponible, los productos recolectados y la puntuación.

También incluye controles para interactuar con el personaje y opciones de navegación para regresar al menú principal.

### Progresión entre niveles

El videojuego permite completar niveles consecutivos mediante los sistemas `GameManager` y `LevelGenerator`.

Al completar los objetivos de un nivel, se genera el siguiente dentro de la misma escena, actualizando los productos y las condiciones de la partida.

### Pruebas de funcionamiento

Se realizaron pruebas de navegación entre las escenas, funcionamiento de los botones y progresión entre niveles consecutivos.

Durante las pruebas realizadas en el editor de Unity, las interfaces y transiciones funcionaron correctamente, sin presentar errores en la consola.

### Resultado

Las interfaces implementadas permiten que el jugador navegue entre el menú principal, las instrucciones y el escenario de juego de manera funcional.

Asimismo, el sistema de progresión permite continuar jugando durante varios niveles consecutivos, cumpliendo con los objetivos de navegación e interacción establecidos para esta etapa.

## ⚡ Etapa 6. Game Feel y potenciador de velocidad

En esta etapa se implementó una mecánica de *Game Feel* en **Mini Market Stories**, con el propósito de mejorar la respuesta visual del personaje y proporcionar una experiencia de juego más dinámica mediante un potenciador temporal de velocidad.

### Potenciador de velocidad (Speed Boost)

Se incorporó un objeto coleccionable representado por una esfera amarilla que permite aumentar temporalmente la velocidad de desplazamiento del personaje.

La activación ocurre automáticamente cuando el jugador entra en contacto con la esfera, mediante un Collider configurado como Trigger y el script `SpeedBoostPickup.cs`.

Al recoger el potenciador, el objeto desaparece y se activa el aumento de velocidad.

### Configuración del potenciador

| Parámetro | Valor |
|---|---:|
| Velocidad normal | 5 |
| Velocidad con potenciador | 8.5 |
| Duración del efecto | 2.5 segundos |
| Duración máxima permitida | 3 segundos |
| Potenciadores por nivel | 1 |
| Puntos de aparición disponibles | 4 |

Los parámetros de velocidad y duración pueden modificarse desde el Inspector de Unity.

### Efecto visual mediante Trail Renderer

Para reforzar la percepción del aumento de velocidad, se incorporó un componente `Trail Renderer` al personaje.

Este componente genera una estela de colores rosa y violeta mientras el potenciador permanece activo.

Se creó el material `MAT_SpeedTrail`, utilizando un shader compatible con Universal Render Pipeline (URP), para conservar los colores del degradado y mejorar la visibilidad del efecto.

La estela se activa al recoger el potenciador y deja de generarse cuando termina el aumento de velocidad.

### Generación aleatoria del potenciador

Se integró el Speed Boost al sistema de generación dinámica mediante modificaciones en `LevelGenerator.cs`.

Al comenzar cada nivel, el sistema selecciona aleatoriamente uno de cuatro puntos de aparición distribuidos dentro del minimercado.

Estos puntos son independientes de las posiciones utilizadas para generar los productos, lo que permite conservar la cantidad máxima de productos y evitar interferencias entre los objetos.

El sistema genera únicamente un potenciador por nivel. Si el jugador no lo recoge, se elimina al comenzar el siguiente nivel y se genera uno nuevo.

### Scripts utilizados

- **`PlayerMovement.cs`:** administra el aumento temporal de velocidad, su duración y la activación del efecto visual.
- **`SpeedBoostPickup.cs`:** detecta el contacto del personaje con el potenciador y activa la mecánica.
- **`LevelGenerator.cs`:** controla la generación aleatoria del potenciador y elimina los objetos correspondientes al nivel anterior.

### Pruebas de funcionamiento

Se realizaron pruebas dentro del editor de Unity para comprobar la activación del potenciador, el incremento de velocidad, la visualización de la estela y la recuperación de los valores normales al finalizar el efecto.

También se verificó que el Speed Boost apareciera en diferentes posiciones durante la progresión entre niveles, manteniendo un solo potenciador activo por nivel.

### Resultado

La implementación del Speed Boost permite que el jugador experimente un cambio temporal en el desplazamiento del personaje, acompañado de una respuesta visual que facilita identificar cuándo se encuentra activo el potenciador.

Esta mecánica complementa el sistema de niveles y aporta mayor variedad a la experiencia de juego, manteniendo el funcionamiento de la recolección y entrega de productos.

## Mejoras adicionales: personaje robótico y optimización de anaqueles

Como parte de las mejoras finales de **Mini Market Stories**, se realizaron ajustes visuales y funcionales para fortalecer la identidad del videojuego y mejorar la experiencia del jugador.

### 🤖 Implementación de un personaje robótico

Se reemplazó la apariencia de cápsula del personaje principal por un pequeño robot flotante de estilo low-poly, diseñado directamente en Unity mediante objetos tridimensionales básicos.

El robot cuenta con los siguientes elementos:

- Cabeza rectangular con dos ojos violetas.
- Cuerpo compacto con brazos laterales.
- Propulsor inferior que simula un sistema de flotación.
- Materiales personalizados en tonos azul grisáceo y violeta.
- Diseño visual adaptado a la estética del minimercado.

Para su implementación, se creó el objeto `RobotVisual` como hijo de `Player`, conservando la cápsula original como estructura física invisible. Esto permitió mantener los sistemas de movimiento, colisiones, recolección de productos y Speed Boost sin modificar sus mecánicas.

También se actualizó el script `PlayerAppearance.cs` para permitir que el jugador continúe personalizando el personaje mediante la tecla **V**, alternando entre tres colores: rosa pastel, lavanda y azul cielo.

### 🛒 Optimización de los anaqueles

Se realizaron ajustes al sistema de entrega y colocación de productos para mejorar su organización visual y evitar interferencias con el desplazamiento del personaje.

Las mejoras implementadas incluyen:

- Reducción de la separación entre productos a `0.3` unidades.
- Límite de seis productos visibles por anaquel.
- Registro de entregas adicionales sin necesidad de mostrar más productos.
- Desactivación de las colisiones de los productos entregados.
- Reinicio de los contadores de colocación al comenzar un nuevo nivel.

Estas modificaciones permiten mantener los productos dentro del espacio disponible y evitar que las entregas de niveles anteriores afecten su distribución.

### ✅ Pruebas de funcionamiento

Después de implementar las mejoras, se realizaron pruebas para comprobar:

- Movimiento y desplazamiento del nuevo personaje.
- Funcionamiento del sistema de personalización de colores.
- Recolección y entrega de productos.
- Activación del Speed Boost y su efecto visual.
- Distribución y reinicio de los productos en los anaqueles.

Los resultados fueron satisfactorios, manteniendo las funcionalidades previamente desarrolladas.

### Resultado

Estas mejoras permiten que **Mini Market Stories** cuente con un personaje principal más representativo de su identidad visual, además de un sistema de entrega de productos mejor organizado. La implementación se realizó aprovechando la estructura y programación existentes, evitando modificaciones innecesarias en las mecánicas del videojuego.

## Etapa 7. Configuración, compilación y distribución del videojuego

### Descripción

Como parte de la etapa final de desarrollo de **Mini Market Stories**, se realizaron las configuraciones necesarias para exportar el videojuego como una aplicación independiente, permitiendo su ejecución fuera del entorno de desarrollo de Unity.

Para facilitar su distribución y evaluación, se generaron dos versiones del proyecto: una destinada a dispositivos móviles Android y otra compatible con computadoras Windows de 64 bits.

### Configuración para Android

Se configuró el perfil de compilación de Android mediante la herramienta **Build Profiles de Unity 6.2**, estableciendo los parámetros técnicos necesarios para generar el archivo APK.

Las principales configuraciones fueron:

| Parámetro | Configuración |
|---|---|
| Nombre de la aplicación | Mini Market Stories |
| Desarrollador | Ross Games |
| Versión | 1.0 |
| Identificador del paquete | `com.rossgames.minimarketstories` |
| Versión mínima de Android | Android 8.0 (API 26) |
| API de destino | Automática (nivel más alto instalado) |
| Arquitectura | ARM64 |
| Scripting Backend | IL2CPP |
| Orientación de pantalla | Horizontal (Landscape Left) |
| Formato de distribución | APK |
| Firma digital | Clave de depuración |

También se diseñó e implementó un ícono adaptativo para Android, compuesto por imágenes independientes de fondo y primer plano, configuradas en las resoluciones correspondientes.

Para la compilación se utilizaron los módulos oficiales de Unity: Android Build Support, Android SDK & NDK Tools y OpenJDK.

**Archivo generado:** `MiniMarketStories_v1.0.apk`

### Configuración para Windows

Adicionalmente, se creó un perfil de compilación para Windows con arquitectura Intel de 64 bits.

Esta versión permite ejecutar el videojuego directamente en una computadora compatible, sin necesidad de instalar Unity.

La compilación incluye el archivo ejecutable y sus dependencias, organizadas en una carpeta que puede distribuirse mediante un archivo ZIP.

**Archivo de distribución previsto:** `MiniMarketStories_Windows_v1.0.zip`

### Escenas incluidas

Ambas compilaciones incorporan las tres escenas principales del videojuego:

| Escena | Descripción |
|---|---|
| `MainMenu` | Pantalla principal y acceso al videojuego. |
| `MiniMarket` | Entorno tridimensional donde se desarrollan las mecánicas de juego. |
| `HowToPlay` | Pantalla de instrucciones para el jugador. |

La escena `MainMenu` se estableció como punto de inicio de la aplicación.

### Pruebas de funcionamiento

Se completó satisfactoriamente la compilación del proyecto para Android y Windows.

La versión de Windows se ejecutó fuera del editor de Unity, comprobando el funcionamiento de la aplicación y su navegación principal.

La versión Android se generó correctamente en formato APK. Sin embargo, su funcionamiento en un dispositivo físico permanece pendiente de verificación.

### Distribución del videojuego

Para ejecutar la versión de Windows:

1. Descargar el archivo ZIP de distribución.
2. Extraer completamente su contenido.
3. Abrir el archivo `Mini Market Stories.exe`.
4. Utilizar los controles disponibles para interactuar con el videojuego.

Para instalar la versión Android:

1. Transferir el archivo APK a un dispositivo Android compatible.
2. Abrir el archivo desde el dispositivo.
3. Autorizar la instalación desde esa fuente cuando corresponda.
4. Instalar y ejecutar Mini Market Stories.

La versión Android requiere un dispositivo compatible con la arquitectura ARM64 y Android 8.0 o posterior.

### Resultado de la etapa

La configuración y compilación de **Mini Market Stories** permitió transformar el proyecto desarrollado en Unity en dos versiones independientes para su distribución.

Con ello, se completa el proceso de desarrollo del videojuego, desde la implementación de sus mecánicas y escenarios tridimensionales hasta la preparación de archivos ejecutables para diferentes plataformas.

Esta etapa también permitió comprender la importancia de configurar correctamente los parámetros técnicos de cada sistema operativo y realizar pruebas de funcionamiento antes de distribuir una aplicación.

## 👩‍💻 Autora

**Aida Rosas Romero**  
Diseño de Juegos II  
Licenciatura en Desarrollo de Sistemas Web  
Universidad de Guadalajara

**Proyecto académico desarrollado con Unity y C#.**