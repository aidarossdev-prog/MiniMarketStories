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
    // Evitar recoger más de un producto.
    if (HasProduct())
        return;

    // Comprobar que el objeto sea un producto.
    if (!other.CompareTag("Product"))
        return;

    // Verificar la referencia al administrador del juego.
    if (gameManager == null)
        return;

    // Obtener la información del producto generado.
    GeneratedProduct generatedProduct =
        other.GetComponent<GeneratedProduct>();

    // Ignorar productos que no pertenezcan al sistema dinámico.
    if (generatedProduct == null)
        return;

    // Comprobar que el producto corresponda al nivel actual.
    if (generatedProduct.level != gameManager.currentLevel)
        return;

    // Evitar recoger productos ya entregados.
    if (generatedProduct.isDelivered)
        return;

    // Recoger el producto.
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

    public void ClearCarriedProduct()
{
    // Comprobar si el jugador transporta un producto.
    if (carriedProduct == null)
        return;

    // Guardar la referencia al producto.
    GameObject product = carriedProduct;

    // Liberar la referencia del jugador.
    carriedProduct = null;

    // Desactivar el producto inmediatamente.
    product.SetActive(false);

    // Eliminar el producto de la escena.
    Destroy(product);

    Debug.Log("Producto transportado eliminado correctamente.");
}


}