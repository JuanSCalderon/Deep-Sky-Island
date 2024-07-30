using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject candyTextUI;
    public TextMeshProUGUI candyCountGameOverText;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);
        if (candyTextUI != null) // Asegúrate de que la referencia no sea nula
        {
            candyTextUI.SetActive(false); // Oculta el texto de los dulces
        }
        
        // Mostrar la cantidad de dulces recogidos en el Game Over
        if (candyCountGameOverText != null) // Asegúrate de que la referencia no sea nula
        {
            candyCountGameOverText.text = HUD.candyTotal.ToString() + " candies";
            candyCountGameOverText.gameObject.SetActive(true); // Asegúrate de que el texto sea visible
        }
    }

    public void RestartGame()
    {
        HUD.ResetCandyCount();
        PlayerControllerX playerController = FindObjectOfType<PlayerControllerX>();
        if (playerController != null)
        {
            Physics.gravity = playerController.originalGravity;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}