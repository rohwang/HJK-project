using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [Header("ü�� ��")]
    [SerializeField] Slider hp;

    [Header("ü�� ������ ��")]
    [SerializeField] Slider hpD;

    [Header("���¹̳� ��")]
    [SerializeField] Slider stamina;

    [Header("���¹̳� ������ ��")]
    [SerializeField] Slider staminaD;

    [Header("�뽬")]
    [SerializeField] playerDash dash;

    [Header("����")]
    [SerializeField] playerJump jump;

    [Header("�̵�")]
    [SerializeField] playerLRMove move;

    public bool canRecover => Time.time >= recoveryCooldown;
    public float recoveryCooldown = 0f;

    public bool isGameOver = false; 
    void Start()
    {
        hp.maxValue = 100;
        hpD.maxValue = 100;
        hp.value = hp.maxValue;
        hpD.value = hpD.maxValue;

        stamina.maxValue = 100;
        staminaD.maxValue = 100;
        stamina.value = stamina.maxValue;
        staminaD.value = staminaD.maxValue;
    }

    void RestrictDashbyStamina()  //  ���¹̳� �뽬 ����
    {
        if (stamina.value <= 20)
        {
            dash.canDash2 = false;
        }
        else
        {
            dash.canDash2 = true;
        }
    }

    void RestrictJumpbyStamina()    // ���¹̳� ���� ����
    {
        if (stamina.value <= 20)
        {
            jump.canJump2 = false;
        }
        else
        {
            jump.canJump2 = true;
        }
    }

    public void RestrictMovebyStamina() //  ���¹̳� �̵� ����
    {
        if (stamina.value <= 20)
        {
            move.moveSpeed = 2f;
        }
        else
        {
            move.moveSpeed = 3f;
        }
    }


    IEnumerator DelayHP(float Rcur, float Dcur)    //  HP ������ �� ���� �ڷ�ƾ
     {
        if (Rcur < Dcur)
        {
            yield return new WaitForSeconds(0.5f);
            hpD.value -= 3 * Time.deltaTime;
        }
        else
        {
            hpD.value = Rcur;
        }
    }

    public void Dash()
    {
        stamina.value -= 20;
    }

    public void Jump()
    {
        stamina.value -= 10;
    }

    // 0���� 1������ ��ġ�� �غ��ϰ� 0 �̸��̸� 0���� ����, �뽬���� 0���� ���� �� 1�� �� 1�� ����,
    // ������ ������ 1�� ����Ǵ� ������ ���ľ� ��. ���� ��� 3�� �뽬���� �� ù �뽬���� 1�� �� 1�� �����Ǹ� �� ��

    IEnumerator RecoverStamina()
    {
        if(dash.isDash)
        {
            yield break;
        }
            if (dash.canDash && stamina.value < stamina.maxValue && canRecover)
            {
            yield return new WaitForSeconds(1f);
            stamina.value += 10 * Time.deltaTime;
            }
    }
    IEnumerator DelayStamina(float Rcur, float Dcur)
    {
        if (Rcur < Dcur)
        {
            yield return new WaitForSeconds(0.3f);
            staminaD.value -= 5 * Time.deltaTime;
        }
        else
        {
            staminaD.value = Rcur;
        }
    }

    void Update()
    {
        if (isGameOver) //  ���� ���� �� �����̴� ����
        {
            return;
        }
        else if (hp.value <= 0) //  ü�� 0�� �� ���� ����
        {
            isGameOver = true;
            return;
        }
        else if (stamina.value < 0) //  ���¹̳� 0���� ������ 0���� ������
        {
            stamina.value = 0;
        }
        else
        {
            RestrictDashbyStamina();
            RestrictJumpbyStamina();
            RestrictMovebyStamina();

            StartCoroutine(DelayHP(hp.value, hpD.value));
            StartCoroutine(DelayStamina(stamina.value, staminaD.value));
            StartCoroutine(RecoverStamina());
        }
    }
}
