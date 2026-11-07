using UnityEngine;

public class ProductPersistence : MonoBehaviour
{
    [Header("Identificador del producto")]
    public int productID;

    public bool WasDelivered()
    {
        return PlayerPrefs.GetInt("Product_" + productID, 0) == 1;
    }

    public void MarkAsDelivered()
    {
        PlayerPrefs.SetInt("Product_" + productID, 1);
        PlayerPrefs.Save();

        Debug.Log("Producto guardado: " + productID);
    }
}