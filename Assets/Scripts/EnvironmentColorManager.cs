using UnityEngine;
using UnityEngine.InputSystem;

public class EnvironmentColorManager : MonoBehaviour
{
    [Header("Cámara principal")]
    public Camera mainCamera;

    [Header("Iluminación principal")]
    public Light directionalLight;

    [Header("Colores de iluminación")]
    public Color[] lightColors =
    {
    new Color(1f, 0.85f, 0.65f),   // Mañana: amarillo cálido
    new Color(1f, 0.55f, 0.65f),   // Atardecer: rosa cálido
    new Color(0.45f, 0.60f, 1f)   // Noche: azul suave
    };


    [Header("Colores del entorno")]
    public Color[] backgroundColors =
    {
        new Color(1f, 0.90f, 0.82f),    // Durazno pastel
        new Color(1f, 0.82f, 0.88f),    // Rosa pastel
        new Color(0.78f, 0.80f, 1f)    // Lavanda
    };

    private int currentColorIndex = 0;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        ChangeBackgroundColor();
    }

    private void Update()
{
    if (Keyboard.current != null &&
        Keyboard.current.cKey.wasPressedThisFrame)
    {
        NextBackgroundColor();
    }
}

    public void NextBackgroundColor()
    {
        if (backgroundColors == null || backgroundColors.Length == 0)
        {
            return;
        }

        currentColorIndex =
            (currentColorIndex + 1) % backgroundColors.Length;

        ChangeBackgroundColor();
    }

    public void SetEnvironmentByLevel(int level)
{
    if (backgroundColors == null || backgroundColors.Length == 0)
    {
        return;
    }

    // Cambia de ambiente cada dos niveles.
    int environmentIndex = (level - 1) / 2;

    // Repite los ambientes cuando llega al último.
    currentColorIndex = environmentIndex % backgroundColors.Length;

    // Aplica el fondo y la iluminación correspondiente.
    ChangeBackgroundColor();

    Debug.Log(
        "Nivel " + level +
        " | Ambiente automático: " + currentColorIndex
    );
}

    private void ChangeBackgroundColor()
{
    if (mainCamera == null ||
        backgroundColors == null ||
        backgroundColors.Length == 0)
    {
        return;
    }

    // Cambiar el color de fondo.
    mainCamera.backgroundColor =
        backgroundColors[currentColorIndex];

    // Cambiar el color de la iluminación principal.
    if (directionalLight != null &&
        lightColors != null &&
        lightColors.Length > 0)
    {
        int lightIndex = currentColorIndex % lightColors.Length;

        directionalLight.color = lightColors[lightIndex];
    }

    Debug.Log(
        "Ambiente actualizado: " + currentColorIndex
    );
}
}