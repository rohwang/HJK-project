using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class backgroundControl : MonoBehaviour
{
    [Header("UI ¹è°æ")]
    [SerializeField] Image UIbg;

    private void Start()
    {
        UIbgOff();
    }
    public void UIbgOn()
    {
        UIbg.enabled = true;
    }
    public void UIbgOff()
    {
        UIbg.enabled = false;
    }   
}
