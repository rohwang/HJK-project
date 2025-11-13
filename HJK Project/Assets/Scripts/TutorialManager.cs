using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] GameObject LRMove;
    [SerializeField] GameObject Jump;


    void Start()
    {
        Jump.SetActive(true);
        LRMove.SetActive(true);
    }

    public void Jump_UnUsed()
    {
        Jump.SetActive(true);
    }
    public void WASD_UnUsed()
    {
        LRMove.SetActive(true);
    }
    void WASD_USED()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) LRMove.SetActive(false);
    }
    void Jump_Used()
    {
        if (Input.GetKey(KeyCode.Space)) Jump.SetActive(false);
    }
    void Update()
    {
        WASD_USED();
        Jump_Used();
    }
}
