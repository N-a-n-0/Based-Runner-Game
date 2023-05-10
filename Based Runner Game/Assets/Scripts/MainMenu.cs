using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
  public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
