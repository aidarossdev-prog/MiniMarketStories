using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAppearance : MonoBehaviour
{
    [Header("Renderizadores del robot")]
    public Renderer[] robotRenderers;

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

    private Material[] robotMaterials;
    private int currentColorIndex = 0;

    private void Start()
    {
        if (robotRenderers == null || robotRenderers.Length == 0)
        {
            Debug.LogWarning(
                "No se asignaron renderizadores al robot."
            );
            return;
        }

        robotMaterials = new Material[robotRenderers.Length];

        for (int i = 0; i < robotRenderers.Length; i++)
        {
            if (robotRenderers[i] != null)
            {
                // Instancia independiente para no modificar
                // los materiales originales del proyecto.
                robotMaterials[i] = robotRenderers[i].material;
            }
        }

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
        if (robotMaterials == null ||
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
        if (robotMaterials == null ||
            playerColors == null ||
            playerColors.Length == 0)
        {
            return;
        }

        foreach (Material material in robotMaterials)
        {
            if (material == null)
                continue;

            material.SetColor(
                "_BaseColor",
                playerColors[currentColorIndex]
            );

            material.SetFloat("_Metallic", metallic);
            material.SetFloat("_Smoothness", smoothness);
        }

        Debug.Log(
            "Apariencia del robot actualizada. Color: " +
            currentColorIndex
        );
    }

    private void OnDestroy()
    {
        if (robotMaterials == null)
            return;

        foreach (Material material in robotMaterials)
        {
            if (material != null)
            {
                Destroy(material);
            }
        }
    }
}