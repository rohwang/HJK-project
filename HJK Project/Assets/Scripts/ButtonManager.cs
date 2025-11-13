using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] GameObject BackGround;
    void Start()
    {
        BackGround.SetActive(false);
    }
    public void StartButtonClick()
    {
        SceneManager.LoadScene(name="FirstStage");
    }

    public void QuitButtonClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;    //  에디터용
        // Application.Quit();  //  빌드파일용
    }

    public void SettingButtonCLick()
    {
        BackGround.SetActive(true);
    }

    public void BackButtonClick()
    {
        BackGround.SetActive(false);
    }

}
