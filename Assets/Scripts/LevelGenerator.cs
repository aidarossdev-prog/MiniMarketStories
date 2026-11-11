using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs de productos")]
    public GameObject[] productPrefabs;

    [Header("Puntos de aparición")]
    public Transform[] spawnPoints;

    [Header("Configuración de dificultad")]
    [Min(1)] public int initialProductCount = 3;
    [Min(0)] public int productsIncreasePerLevel = 2;
    [Min(1)] public int maximumSimultaneousProducts = 12;

    [Header("Productos generados")]
    public Transform productsContainer;

    private readonly List<GameObject> activeProducts = new List<GameObject>();

    public int GetProductCount(int level)
{
    // Calcular la cantidad según la dificultad.
    int productCount =
        initialProductCount +
        (level - 1) * productsIncreasePerLevel;

    // Respetar el máximo configurado.
    productCount = Mathf.Min(
        productCount,
        maximumSimultaneousProducts
    );

    // No solicitar más productos que puntos disponibles.
    int availableSpawnPoints =
        spawnPoints != null ? spawnPoints.Length : 0;

    productCount = Mathf.Min(
        productCount,
        availableSpawnPoints
    );

    return productCount;
}

    public void GenerateLevel(int level)
    {
        if (productPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Faltan Prefabs o puntos de aparición.");
            return;
        }

        ClearLevel();

        int productCount = GetProductCount(level);

        List<Transform> availablePoints =
            new List<Transform>(spawnPoints);

        for (int i = 0; i < productCount; i++)
        {
            if (availablePoints.Count == 0)
                break;

            int randomIndex = Random.Range(0, availablePoints.Count);

            Transform spawnPoint = availablePoints[randomIndex];

            availablePoints.RemoveAt(randomIndex);

            int prefabIndex = (level - 1) % productPrefabs.Length;

            GameObject newProduct = Instantiate(
    productPrefabs[prefabIndex],
    spawnPoint.position,
    spawnPoint.rotation,
    productsContainer
);

// Asignar información del nivel al producto generado.
GeneratedProduct generatedProduct =
    newProduct.GetComponent<GeneratedProduct>();

if (generatedProduct == null)
{
    generatedProduct = newProduct.AddComponent<GeneratedProduct>();
}

generatedProduct.Initialize(level, i);

// Registrar el producto generado.
activeProducts.Add(newProduct);

        }

        Debug.Log(
            "Nivel " + level +
            " generado con " + activeProducts.Count + " productos."
        );
    }

    public void ClearLevel()
{
    // Recorrer los productos generados anteriormente.
    foreach (GameObject product in activeProducts)
    {
        if (product == null)
            continue;

        // Desactivar inmediatamente el producto.
        product.SetActive(false);

        // Programar su eliminación.
        Destroy(product);
    }

    // Limpiar las referencias de productos anteriores.
    activeProducts.Clear();

    Debug.Log("Productos del nivel anterior limpiados.");
}
}