using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartButtonClick()
    {
        SceneManager.LoadScene(name="FirstStage");
    }

    public void QuitButtonClick()
    {

        UnityEditor.EditorApplication.isPlaying = false;
        // Application.Quit();
    }

    public void SettingButtonCLick()
    {
        Debug.Log("³­ ¸ô¶ó");
    }
}
