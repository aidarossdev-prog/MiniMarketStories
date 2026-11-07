using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Abre la tienda.
    public void PlayGame()
    {
        SceneManager.LoadScene("MiniMarket");
    }

    // Abre la pantalla de instrucciones.
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    // Regresa al menú principal.
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Cierra el videojuego.
    public void ExitGame()
    {
        Debug.Log("Saliendo de Mini Market Stories...");
        Application.Quit();
    }
}