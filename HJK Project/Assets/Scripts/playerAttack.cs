using UnityEngine;
using UnityEngine.UI;

public class playerAttack : MonoBehaviour
{
    [Header("Ã¼·Â ¹Ù")]
    [SerializeField] Slider hp;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Death()
    {

    }

    void Update()
    {
        
    }
}
