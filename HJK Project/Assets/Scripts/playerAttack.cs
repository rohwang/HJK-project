using UnityEngine;
using UnityEngine.UI;

public class playerAttack : MonoBehaviour
{
    [Header("Ã¼·Â ¹Ù")]
    [SerializeField] Slider hp;

    [SerializeField] SliderBar bar;
    [SerializeField] backgroundControl bgControl;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Death()
    {
        anim.SetBool("Death", true);
    }

    void Update()
    {
        if(hp.value <= 0)
        {
            bar.isGameOver = true;
            bgControl.DeathbgOn();
            Death();
        }
        
    }
}
