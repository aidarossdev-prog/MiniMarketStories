using UnityEngine;

public class ProductCollector : MonoBehaviour
{
    [Header("Producto transportado")]
    public GameObject carriedProduct;

    [Header("Control de niveles")]
    public GameManager gameManager;

    [Header("Posición del producto")]
    public Vector3 carryOffset = new Vector3(0f, 1.2f, 0.6f);

    public bool HasProduct()
    {
        return carriedProduct != null;
    }

    private void OnTriggerEnter(Collider other)
{
    // No recoger otro producto si ya transportamos uno
    if (HasProduct())
        return;

    // Comprobar que sea un producto
    if (!other.CompareTag("Product"))
        return;

    // Verificar que exista GameManager
    if (gameManager == null)
        return;

    // Identificar el producto
    ProductPersistence persistence =
        other.GetComponent<ProductPersistence>();

    if (persistence == null)
        return;

    // Comprobar a qué nivel pertenece
    int productLevel = 0;

    if (persistence.productID >= 1 && persistence.productID <= 3)
        productLevel = 1;
    else if (persistence.productID >= 4 && persistence.productID <= 8)
        productLevel = 2;

    // Recoger únicamente productos del nivel actual
    if (productLevel != gameManager.currentLevel)
        return;

    // Evitar recoger productos que ya fueron entregados
    if (persistence.WasDelivered())
        return;

    PickUpProduct(other.gameObject);
}

    private void PickUpProduct(GameObject product)
    {
        carriedProduct = product;

        Collider productCollider = product.GetComponent<Collider>();

        if (productCollider != null)
            productCollider.enabled = false;

        product.transform.SetParent(transform);
        product.transform.localPosition = carryOffset;
        product.transform.localRotation = Quaternion.identity;

        Debug.Log("Producto recogido: " + product.name);
    }
}