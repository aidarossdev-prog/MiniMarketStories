using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedBoostPickup : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        // Evita activar el potenciador más de una vez.
        if (collected)
            return;

        // Comprueba si el objeto que entró es el jugador.
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

        if (player != null)
        {
            collected = true;

            // Activa el aumento temporal de velocidad.
            player.ActivateSpeedBoost();

            Debug.Log("¡Speed Boost recogido!");

            // Elimina el potenciador después de recogerlo.
            Destroy(gameObject);
        }
    }
}