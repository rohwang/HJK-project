using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class backgroundControl : MonoBehaviour
{
    [Header("설정 배경")]
    [SerializeField] GameObject SettBG;

    [Header("데스 배경")]
    [SerializeField] GameObject DeathBG;

    [SerializeField] ButtonManager buttonManager;

    private void Start()
    {
        SettbgOff();
        DeathbgOff();
    }
    public void SettbgOn()
    {
        SettBG.SetActive(true);
    }
    public void SettbgOff()
    {
        SettBG.SetActive(false);
    }
    public void DeathbgOn()
    {
        DeathBG.SetActive(true);
    }
    public void DeathbgOff()
    {
        DeathBG.SetActive(false);
    }   
}
