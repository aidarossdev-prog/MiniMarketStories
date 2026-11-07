using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Interfaz")]
    public TextMeshProUGUI textProgress;
    public TextMeshProUGUI textScore;

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
        LoadProgress();
        UpdateUI();
    }

    public void ProductDelivered()
    {
        if (currentLevel > productQuantities.Length)
            return;

        collectedProducts++;
        totalScore += 10;

        int requiredProducts = productQuantities[currentLevel - 1];

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

    private void NextLevel()
    {
        currentLevel++;
        collectedProducts = 0;

        SaveProgress();
        UpdateUI();

        Debug.Log("¡Nivel completado!");
    }

    private void UpdateUI()
{
    // Actualizar la puntuación
    if (textScore != null)
    {
        textScore.text = "PUNTOS: " + totalScore;
    }

    // Actualizar el progreso del nivel
    if (textProgress == null)
        return;

    if (currentLevel > productQuantities.Length)
    {
        textProgress.text = "¡TIENDA COMPLETADA!";
        return;
    }

    string productName = productNames[currentLevel - 1];
    int requiredProducts = productQuantities[currentLevel - 1];

    textProgress.text =
        "NIVEL " + currentLevel +
        "  |  " + productName +
        ": " + collectedProducts +
        "/" + requiredProducts;
}

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CollectedProducts", collectedProducts);
        PlayerPrefs.SetInt("TotalScore", totalScore);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        collectedProducts = PlayerPrefs.GetInt("CollectedProducts", 0);
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        
    }

    [ContextMenu("Reiniciar progreso")]
public void ResetProgress()
{
    // Borrar el progreso general
    PlayerPrefs.DeleteKey("CurrentLevel");
    PlayerPrefs.DeleteKey("CollectedProducts");
    PlayerPrefs.DeleteKey("TotalScore");

    // Borrar los registros individuales de las latas
    for (int i = 1; i <= 3; i++)
    {
        PlayerPrefs.DeleteKey("Product_" + i);
    }

    PlayerPrefs.Save();

    // Restablecer los valores iniciales
    currentLevel = 1;
    collectedProducts = 0;
    totalScore = 0;

    UpdateUI();

    Debug.Log("Progreso reiniciado correctamente.");
}

}