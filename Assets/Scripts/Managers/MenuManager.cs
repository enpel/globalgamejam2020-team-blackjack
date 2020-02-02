using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public GameObject menu;

    public void DisplayMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
