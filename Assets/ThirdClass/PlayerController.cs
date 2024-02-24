using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 우리가 선택한 애니메이션 움직임 대로 캐릭터가 움직이도록 하는 기능을 구현할 겁니다.

    Animator animator;
    public enum PlayerState { Idle, Run, Death}
    PlayerState playerstate;

    public bool IsPlayerDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerDeath == true) return;

        SetPlayerState();
        SetPlayerAnimation();
    }


    // 한번만 실행하면 되는 기능을 구현하는 함수이다.
    void Initialize()
    {
        // Animator 클래스에 접근을 할 수 있게 됩니다.
        animator = GetComponentInChildren<Animator>();
    }


    public void PlayerDeath()
    {
        if (IsPlayerDeath == true) return;

        //if (animator == null) return;
        //animator.SetTrigger("Death");
        animator?.SetTrigger("Death");
        IsPlayerDeath = true;
    }

    public void PlayerMove()
    {
        animator.SetBool("IsRun", true);
    }

    public void PlayerIdle()
    {
        animator.SetBool("IsRun", false);
    }

    // 게임에서 애니메이션을 실행시키기 위해 Update문 선언할 함수이다.
    // 플레이어의 상태에 따라 다른 애니메이션을 실행해야하는대
    // 그 조건을 판단해주는 함수입니다.
    private void SetPlayerAnimation()
    {
        if(playerstate == PlayerState.Idle)
        {
            PlayerIdle();
        }
        else if(playerstate == PlayerState.Run)
        {
            PlayerMove();
        }
    }
    // 현재 나의 상태를 판별해주는 함수
    private void SetPlayerState()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v != 0 || h != 0)
        {
            playerstate = PlayerState.Run;
        }
        else
        {
            playerstate = PlayerState.Idle;
        }
    }
}
