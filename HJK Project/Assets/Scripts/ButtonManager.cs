using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    bool bgon = false;

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

    public void SettingButtonClick()
    {
        BackGround.SetActive(true);
        bgon = true;
    }

    public void BackButtonClick()
    {
        BackGround.SetActive(false);
        bgon = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (bgon) BackButtonClick();
            else SettingButtonClick();
        }
    }

}
