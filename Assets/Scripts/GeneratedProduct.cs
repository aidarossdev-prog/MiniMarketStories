using UnityEngine;

public class GeneratedProduct : MonoBehaviour
{
    [Header("Información del producto")]
    public int level;
    public int productIndex;

    [Header("Estado")]
    public bool isDelivered = false;

    public void Initialize(int assignedLevel, int assignedIndex)
    {
        level = assignedLevel;
        productIndex = assignedIndex;
        isDelivered = false;
    }

    public void MarkAsDelivered()
    {
        isDelivered = true;
    }
}