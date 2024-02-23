using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static string previousScene;

    public void PlayButton()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("level_temple");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("menu_settings");
    }

    public void MainMenuButton()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("Previous scene not set.");
            // Optionally, you can load a default scene or handle it in another way.
        }
    }
}
