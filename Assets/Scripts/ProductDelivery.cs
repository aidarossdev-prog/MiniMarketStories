using UnityEngine;

public class ProductDelivery : MonoBehaviour
{
    [Header("Configuración")]
    public GameManager gameManager;

[Header("Nivel correspondiente")]
public int deliveryLevel = 1;

    [Header("Posición de los productos")]
    public Transform shelfPlacement;

    [Header("Organización del anaquel")]
public float productSpacing = 0.5f;

[Min(1)]
public int maximumVisibleProducts = 6;

private int deliveredCount = 0;


private void Start()
{
    // Comprobar las referencias necesarias.
    if (gameManager == null || shelfPlacement == null)
    {
        Debug.LogWarning(
            "Faltan referencias en ProductDelivery: " +
            gameObject.name
        );

        return;
    }

    // Inicializar el contador del anaquel.
    deliveredCount = 0;

    Debug.Log(
        "Anaquel preparado para niveles dinámicos: " +
        gameObject.name
    );
}

    private void OnTriggerEnter(Collider other)
{
    // Verificar que sea el jugador.
    if (!other.CompareTag("Player"))
        return;

    // Comprobar que el jugador transporte un producto.
    ProductCollector collector =
        other.GetComponent<ProductCollector>();

    if (collector == null || !collector.HasProduct())
        return;

    // Verificar las referencias necesarias.
    if (gameManager == null || shelfPlacement == null)
        return;

    GameObject product = collector.carriedProduct;

    // Obtener la información del producto generado.
    GeneratedProduct generatedProduct =
        product.GetComponent<GeneratedProduct>();

    if (generatedProduct == null)
        return;

    // Verificar que el producto pertenezca al nivel actual.
    if (generatedProduct.level != gameManager.currentLevel)
        return;

    // Verificar que este anaquel corresponda al nivel.
    // Alternamos entre los dos anaqueles disponibles.
    int expectedDeliveryLevel =
        ((gameManager.currentLevel - 1) % 2) + 1;

    if (deliveryLevel != expectedDeliveryLevel)
        return;

    // Evitar entregar nuevamente el mismo producto.
    if (generatedProduct.isDelivered)
        return;

    // Colocar únicamente los primeros productos en el anaquel.
product.transform.SetParent(null);

if (deliveredCount < maximumVisibleProducts)
{
    Vector3 newPosition =
        shelfPlacement.position +
        shelfPlacement.right * (deliveredCount * productSpacing);

    product.transform.position = newPosition;
    product.transform.rotation = shelfPlacement.rotation;

    // Desactivar las colisiones del producto entregado.
    Collider[] productColliders =
        product.GetComponentsInChildren<Collider>();

    foreach (Collider productCollider in productColliders)
    {
        productCollider.enabled = false;
    }
}
else
{
    // Evitar acumular productos fuera del anaquel.
    product.SetActive(false);
    Destroy(product);
}

   // Registrar la entrega.
generatedProduct.MarkAsDelivered();

// Liberar el producto de las manos del jugador.
collector.carriedProduct = null;

// Actualizar el contador del anaquel.
deliveredCount++;

// Registrar la entrega antes de cambiar de nivel.
Debug.Log(
    "Producto entregado en nivel " +
    generatedProduct.level
);

// Actualizar la puntuación y comprobar
// si corresponde avanzar al siguiente nivel.
gameManager.ProductDelivered();
}

public void ResetShelf()
{
    // Reiniciar el contador de productos colocados.
    deliveredCount = 0;

    Debug.Log(
        "Anaquel reiniciado: " + gameObject.name
    );
}


}