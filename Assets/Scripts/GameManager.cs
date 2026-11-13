using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Interfaz")]
    public TextMeshProUGUI textProgress;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTimer;

[Header("Pantalla de derrota")]
public GameObject gameOverPanel;

    [Header("Generación dinámica de niveles")]
public LevelGenerator levelGenerator;

[Header("Ambientes dinámicos")]
public EnvironmentColorManager environmentManager;

[Header("Anaqueles del minimercado")]
public ProductDelivery[] deliveryShelves;

[Header("Jugador")]
public ProductCollector playerCollector;

[Header("Temporizador del nivel")]
[Min(10f)] public float initialLevelTime = 90f;

[Min(10f)] public float minimumLevelTime = 30f;

[Min(0f)] public float timeReductionPerLevel = 5f;

public float remainingTime;

public bool isGameOver = false;


    [Header("Configuración de niveles")]
    public string[] productNames =
    {
        "LATAS",
        "BOTELLAS DE AGUA",
        "JUGOS",
        "LECHE",
        "GALLETAS"
    };

    public int[] productQuantities = { 3, 5, 7, 10, 12 };

    [Header("Progreso")]
    public int currentLevel = 1;
    public int collectedProducts = 0;
    public int totalScore = 0;

    private void Start()
{
    // Ocultar la pantalla de derrota al iniciar.
if (gameOverPanel != null)
{
    gameOverPanel.SetActive(false);
}

    // Recuperar el progreso guardado.
    LoadProgress();

    // Actualizar la interfaz.
    UpdateUI();

    if (environmentManager != null)
    {
    environmentManager.SetEnvironmentByLevel(currentLevel);
    }

    // Generar los productos del nivel actual.
    if (levelGenerator != null)
    {
        levelGenerator.GenerateLevel(currentLevel);
    }
    else
    {
        Debug.LogWarning(
            "No se ha asignado LevelGenerator en GameManager."
        );
    }

    // Iniciar el temporizador del nivel.
    StartLevelTimer();
}

private void Update()
{
    // No descontar tiempo si la partida terminó.
    if (isGameOver)
        return;

    // Descontar el tiempo transcurrido.
    if (remainingTime > 0f)
    {
        remainingTime -= Time.deltaTime;

        // Evitar que el temporizador tenga valores negativos.
        remainingTime = Mathf.Max(0f, remainingTime);
    }

    // Detectar cuando se agota el tiempo.
    if (remainingTime <= 0f)
{
    GameOver();
}

    // Actualizar el temporizador en pantalla.
    UpdateTimerUI();
}

    public void ProductDelivered()
{
    if (levelGenerator == null)
    {
        Debug.LogWarning("LevelGenerator no está asignado.");
        return;
    }

    collectedProducts++;
    totalScore += 10;

    int requiredProducts =
        levelGenerator.GetProductCount(currentLevel);

    if (collectedProducts >= requiredProducts)
    {
        NextLevel();
    }
    else
    {
        SaveProgress();
        UpdateUI();
    }
}

private void ResetDeliveryShelves()
{
    if (deliveryShelves == null)
        return;

    foreach (ProductDelivery shelf in deliveryShelves)
    {
        if (shelf != null)
        {
            shelf.ResetShelf();
        }
    }
}

private void StartLevelTimer()
{
    // Calcular el tiempo disponible según el nivel.
    float calculatedTime =
        initialLevelTime -
        ((currentLevel - 1) * timeReductionPerLevel);

    // Evitar que el tiempo sea menor al mínimo configurado.
    remainingTime = Mathf.Max(
        minimumLevelTime,
        calculatedTime
    );

    // Indicar que la partida está activa.
    isGameOver = false;

    UpdateTimerUI();

    Debug.Log(
        "Temporizador iniciado. Nivel " +
        currentLevel +
        " | Tiempo disponible: " +
        remainingTime + " segundos."
    );
}

    private void NextLevel()
{
    // Avanzar al siguiente nivel.
    currentLevel++;

    // Reiniciar el contador de productos entregados.
    collectedProducts = 0;

    if (environmentManager != null)
    {
    environmentManager.SetEnvironmentByLevel(currentLevel);
    }

    // Reiniciar los contadores de ambos anaqueles.
    ResetDeliveryShelves();

    // Guardar el progreso.
    SaveProgress();

    // Actualizar la interfaz.
    UpdateUI();

    // Generar los productos del siguiente nivel.
    if (levelGenerator != null)
    {
        levelGenerator.GenerateLevel(currentLevel);
    }

    // Reiniciar el temporizador para el nuevo nivel.
StartLevelTimer();

    Debug.Log("¡Nivel " + currentLevel + " iniciado!");
}

private void GameOver()
{
    // Evitar ejecutar la derrota más de una vez.
    if (isGameOver)
        return;

    // Marcar la partida como terminada.
    isGameOver = true;
    remainingTime = 0f;

    // Actualizar el contador a 00:00.
    UpdateTimerUI();

    // Mostrar la pantalla de derrota.
    if (gameOverPanel != null)
    {
        gameOverPanel.SetActive(true);
    }
    else
    {
        Debug.LogWarning("GameOverPanel no está asignado.");
    }

    Debug.Log("¡Juego terminado! Se agotó el tiempo.");
}
private void UpdateTimerUI()
{
    if (textTimer == null)
        return;

    // Convertir el tiempo restante a minutos y segundos.
    int totalSeconds = Mathf.CeilToInt(remainingTime);

    int minutes = totalSeconds / 60;
    int seconds = totalSeconds % 60;

    // Actualizar el texto del temporizador.
    textTimer.text = string.Format(
        "TIEMPO: {0:00}:{1:00}",
        minutes,
        seconds
    );
}

    private void UpdateUI()
{
    // Actualizar la puntuación acumulada.
    if (textScore != null)
    {
        textScore.text = "PUNTOS: " + totalScore;
    }

    if (textProgress == null || levelGenerator == null)
        return;

        if (levelGenerator.productPrefabs == null ||
    levelGenerator.productPrefabs.Length == 0)
{
    textProgress.text = "CONFIGURACIÓN DE PRODUCTOS PENDIENTE";
    return;
}

    // Obtener la cantidad de productos del nivel actual.
    int requiredProducts =
        levelGenerator.GetProductCount(currentLevel);

    // Determinar el producto correspondiente al nivel.
    string productName;

    int productType = (currentLevel - 1) %
                      levelGenerator.productPrefabs.Length;

    if (productType == 0)
    {
        productName = "LATAS";
    }
    else
    {
        productName = "BOTELLAS DE AGUA";
    }

    // Actualizar el contador de progreso.
    textProgress.text =
        "NIVEL " + currentLevel +
        "  |  " + productName +
        ": " + collectedProducts +
        "/" + requiredProducts;
}

    private void SaveProgress()
{
    // Guardar el nivel alcanzado.
    PlayerPrefs.SetInt(
        "Dynamic_CurrentLevel",
        currentLevel
    );

    // Guardar la puntuación acumulada.
    PlayerPrefs.SetInt(
        "Dynamic_TotalScore",
        totalScore
    );

    // Confirmar el guardado de los datos.
    PlayerPrefs.Save();

    Debug.Log(
        "Progreso guardado. Nivel: " +
        currentLevel +
        " | Puntos: " +
        totalScore
    );
}

    private void LoadProgress()
{
    // Recuperar el progreso del nuevo sistema dinámico.
    currentLevel = PlayerPrefs.GetInt("Dynamic_CurrentLevel", 1);

    totalScore = PlayerPrefs.GetInt("Dynamic_TotalScore", 0);

    // Al regresar al juego, reiniciar las entregas
    // pendientes del nivel actual.
    collectedProducts = 0;

    Debug.Log(
        "Progreso cargado. Nivel: " +
        currentLevel +
        " | Puntos: " +
        totalScore
    );
}

[ContextMenu("Reiniciar progreso")]
public void ResetProgress()
{
    // Eliminar los datos guardados del sistema dinámico.
    PlayerPrefs.DeleteKey("Dynamic_CurrentLevel");
    PlayerPrefs.DeleteKey("Dynamic_TotalScore");

    PlayerPrefs.Save();

    // Reiniciar los valores de la partida.
    currentLevel = 1;
    collectedProducts = 0;
    totalScore = 0;

    if (environmentManager != null)
    {
    environmentManager.SetEnvironmentByLevel(currentLevel);
    }

// Eliminar cualquier producto que transporte el jugador.
if (playerCollector != null)
{
    playerCollector.ClearCarriedProduct();
}


    // Reiniciar los contadores de los anaqueles.
    ResetDeliveryShelves();

    // Generar nuevamente el primer nivel.
    if (levelGenerator != null)
    {
        levelGenerator.GenerateLevel(currentLevel);
    }

    // Actualizar la interfaz.
    UpdateUI();

    // Reiniciar el temporizador de la nueva partida.
StartLevelTimer();

    Debug.Log("¡Partida reiniciada desde el nivel 1!");
}

public void RetryGame()
{
    // Ocultar la pantalla de derrota.
    if (gameOverPanel != null)
    {
        gameOverPanel.SetActive(false);
    }

    // Reiniciar todo el progreso desde el nivel 1.
    ResetProgress();

    // Reiniciar el temporizador.
    StartLevelTimer();

    Debug.Log("¡Nueva partida iniciada desde el nivel 1!");
}

public void ReturnToMainMenu()
{
    // Asegurar que el juego no esté pausado.
    Time.timeScale = 1f;

    // Regresar al menú principal.
    SceneManager.LoadScene("MainMenu");
}

}