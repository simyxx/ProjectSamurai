using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("level_temple");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("menu_settings");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("menu_main");
    }
}
