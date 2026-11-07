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

    private int deliveredCount = 0;

[Header("Productos del nivel 1")]
public GameObject[] levelProducts;

private void Start()
{
    if (gameManager == null || shelfPlacement == null)
    {
        Debug.LogWarning("Faltan referencias en ProductDelivery.");
        return;
    }

    // Comenzar sin posiciones ocupadas
    deliveredCount = 0;

    // Restaurar las latas que ya fueron entregadas
    foreach (GameObject product in levelProducts)
    {
        if (product == null)
            continue;

        ProductPersistence persistence =
            product.GetComponent<ProductPersistence>();

        if (persistence == null)
            continue;

        if (persistence.WasDelivered())
        {
            // Separar la lata de cualquier objeto anterior
            product.transform.SetParent(null);

            // Colocarla en la siguiente posición disponible
            product.transform.position =
                shelfPlacement.position +
                shelfPlacement.right * (deliveredCount * productSpacing);

            product.transform.rotation = shelfPlacement.rotation;

            // Desactivar su Collider
            Collider productCollider =
                product.GetComponent<Collider>();

            if (productCollider != null)
                productCollider.enabled = false;

            product.SetActive(true);

            deliveredCount++;
        }
        else
        {
            // Las latas pendientes permanecen disponibles
            product.SetActive(true);

            Collider productCollider =
                product.GetComponent<Collider>();

            if (productCollider != null)
            {
                productCollider.enabled = true;
                productCollider.isTrigger = true;
            }
        }
    }

    Debug.Log("Latas restauradas en el anaquel: " + deliveredCount);
}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ProductCollector collector = other.GetComponent<ProductCollector>();

        if (collector == null || !collector.HasProduct())
            return;

        GameObject product = collector.carriedProduct;

// Verificar que exista el administrador del juego
if (gameManager == null)
    return;

// Verificar que estamos en el nivel correspondiente
if (gameManager.currentLevel != deliveryLevel)
    return;

// Verificar que todavía faltan productos por entregar
int requiredProducts =
    gameManager.productQuantities[deliveryLevel - 1];

if (gameManager.collectedProducts >= requiredProducts)
    return;

// Verificar que el producto pertenece al nivel actual
bool validProduct = false;

foreach (GameObject levelProduct in levelProducts)
{
    if (levelProduct == product)
    {
        validProduct = true;
        break;
    }
}

if (!validProduct)
    return;

// Separar el producto del personaje
product.transform.SetParent(null);

        // Colocar cada producto en una posición diferente
        if (shelfPlacement != null)
        {
            Vector3 newPosition = shelfPlacement.position
                + shelfPlacement.right * (deliveredCount * productSpacing);

            product.transform.position = newPosition;
            product.transform.rotation = shelfPlacement.rotation;
        }

        // Desactivar el Collider para mantener el producto fijo
        Collider productCollider = product.GetComponent<Collider>();

        if (productCollider != null)
        {
            productCollider.enabled = false;
        }

        // Guardar el identificador del producto entregado
ProductPersistence persistence =
    product.GetComponent<ProductPersistence>();

if (persistence != null)
{
    persistence.MarkAsDelivered();
}

// Liberar al personaje
collector.carriedProduct = null;

// Registrar la entrega
deliveredCount++;

        // Actualizar el contador del juego
        if (gameManager != null)
        {
            gameManager.ProductDelivered();
        }

        Debug.Log("¡Producto entregado! Total en anaquel: " + deliveredCount);
    }
}