using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAppearance : MonoBehaviour
{
    [Header("Renderizador del personaje")]
    public Renderer playerRenderer;

    [Header("Colores disponibles")]
    public Color[] playerColors =
    {
        new Color(0.85f, 0.65f, 0.80f), // Rosa pastel
        new Color(0.70f, 0.65f, 0.90f), // Lavanda
        new Color(0.60f, 0.80f, 0.90f)  // Azul cielo
    };

    [Header("Propiedades visuales")]
    [Range(0f, 1f)]
    public float metallic = 0f;

    [Range(0f, 1f)]
    public float smoothness = 0.5f;

    private Material playerMaterial;
    private int currentColorIndex = 0;

    private void Start()
    {
        if (playerRenderer == null)
        {
            playerRenderer = GetComponent<Renderer>();
        }

        if (playerRenderer == null)
        {
            Debug.LogWarning("No se encontró el Renderer del personaje.");
            return;
        }

        // Crea una instancia del material para no modificar
        // permanentemente el material original MAT_Player.
        playerMaterial = playerRenderer.material;

        ApplyAppearance();
    }

    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.vKey.wasPressedThisFrame)
        {
            NextPlayerColor();
        }
    }

    public void NextPlayerColor()
    {
        if (playerMaterial == null ||
            playerColors == null ||
            playerColors.Length == 0)
        {
            return;
        }

        currentColorIndex =
            (currentColorIndex + 1) % playerColors.Length;

        ApplyAppearance();
    }

    public void ApplyAppearance()
    {
        if (playerMaterial == null ||
            playerColors == null ||
            playerColors.Length == 0)
        {
            return;
        }

        playerMaterial.SetColor(
            "_BaseColor",
            playerColors[currentColorIndex]
        );

        playerMaterial.SetFloat("_Metallic", metallic);
        playerMaterial.SetFloat("_Smoothness", smoothness);

        Debug.Log(
            "Apariencia del personaje actualizada. Color: " +
            currentColorIndex +
            " | Metallic: " + metallic +
            " | Smoothness: " + smoothness
        );
    }

    private void OnDestroy()
    {
        if (playerMaterial != null)
        {
            Destroy(playerMaterial);
        }
    }
}