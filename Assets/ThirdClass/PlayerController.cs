using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �츮�� ������ �ִϸ��̼� ������ ��� ĳ���Ͱ� �����̵��� �ϴ� ����� ������ �̴ϴ�.

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


    // �ѹ��� �����ϸ� �Ǵ� ����� �����ϴ� �Լ��̴�.
    void Initialize()
    {
        // Animator Ŭ������ ������ �� �� �ְ� �˴ϴ�.
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

    // ���ӿ��� �ִϸ��̼��� �����Ű�� ���� Update�� ������ �Լ��̴�.
    // �÷��̾��� ���¿� ���� �ٸ� �ִϸ��̼��� �����ؾ��ϴ´�
    // �� ������ �Ǵ����ִ� �Լ��Դϴ�.
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
    // ���� ���� ���¸� �Ǻ����ִ� �Լ�
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
