using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [Header("체력 바")]
    [SerializeField] Slider hp;

    [Header("체력 딜레이 바")]
    [SerializeField] Slider hpD;

    [Header("스태미나 바")]
    [SerializeField] Slider stamina;

    [Header("스태미나 딜레이 바")]
    [SerializeField] Slider staminaD;

    [Header("대쉬")]
    [SerializeField] playerDash dash;

    [Header("점프")]
    [SerializeField] playerJump jump;

    [Header("이동")]
    [SerializeField] playerLRMove move;

    bool isRecovering = false;
    private float lastStaminaUsedTime;
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
    void OnStaminaUsed()
    {
        if (!isRecovering)
        {
            lastStaminaUsedTime = Time.time;
        }
    }
    void RestrictDashbyStamina()  //  스태미나 대쉬 제한
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

    void RestrictJumpbyStamina()    // 스태미나 점프 제한
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

    public void RestrictMovebyStamina() //  스태미나 이동 제한
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


    IEnumerator DelayHP(float Rcur, float Dcur)    //  HP 딜레이 바 감소 코루틴
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
    }// 스태미나 딜레이 바 감소 코루틴

    Coroutine staminaCoroutine;
    public void Dash()
    {
        stamina.value -= 20;
        
        OnStaminaUsed();
    }

    public void Jump()
    {
        stamina.value -= 10;

        OnStaminaUsed();
    }

    // 0부터 1까지의 수치를 준비하고 0 미만이면 0으로 설정, 대쉬마다 0으로 돌린 뒤 1초 뒤 1로 설정,
    // 하지만 잠깐잠깐 1로 변경되는 단점을 고쳐야 함. 예를 들어 3번 대쉬했을 때 첫 대쉬에서 1초 뒤 1로 설정되면 안 됨

    IEnumerator RecoverStamina()
    {

        // 1초 대기, 도중에 다시 스태미나 사용했으면 타이머 초기화
        while (Time.time - lastStaminaUsedTime < 2.5f)
        {
            yield return null;
        }

        isRecovering = true;

        while (stamina.value < stamina.maxValue)
        {
            // 도중에 대시나 점프하면 다시 초기화
            if (dash.isDash || jump.isJump)
            {
                isRecovering = false;
                yield break;
            }
            stamina.value += 0.03f * Time.deltaTime;
            yield return null;
        }
        isRecovering = false;
    }
    
    void Update()
    {
        if (isGameOver) //  게임 오버 시 슬라이더 정지
        {
            return;
        }
        else if (hp.value <= 0) //  체력 0일 시 게임 오버
        {
            isGameOver = true;
            return;
        }
        else if (stamina.value < 0) //  스태미나 0보다 작으면 0으로 돌리기
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
            if (!isRecovering)
            {
                StartCoroutine(RecoverStamina());
            }
        }
    }
}
