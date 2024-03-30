using Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("적 피격 애니메이션 제어 변수")]
    public float hitBackTime = 0.5f;       // 피격 효과 발생 후 원래 상태로 돌아가는 시간
    public int hitCount = 0;               // 몇 번 공격받앗는지 저장하는 변수
    public int currentHP;                  // 현재 몬스터의 체력
    public int maxHP = 3;                  // 몬스터의 최대 체력
    private SkinnedMeshRenderer skinMeshRenderer; // 피격 시 색상을 변경해주기 위한 재질 정보를 저장하는 변수
    private bool isDeath;                  // true 죽었으니깐 다른 행동 하지마!
    private NavMeshAgent agent;            // 길찾기 대리인 클래스(navMeshAgent)저장하는 변수

    [Header("길 찾기 제어 변수")]
    public Transform target;

    [Header("몬스터의 행동 제어 변수")]
    public float findDistance;              // 타겟을 탐색 시작하는 최대 거리. 최대 거리 밖에 있는 타겟은 쫓지 않는다/
    public float attackRange;               // 공격 범위 내에 타겟이 있으면 공격을 한다.

    [Header("몬스터의 공격 제어 변수")]
    public bool isEnemyAttackEnable;        // 공격 범위안에 플레이어가 들어오면 True, False 반환한다.
    public bool isAttack;                  // 공격을 실행 중일 때 True, 공격이 끝나면 false로 반환한다.
    public float attackCoolTime;            // 쿨타임이 있는 동안에는 적이 공격을 못한다.            
    private float attackCheckTime;          // 쿨타임을 제어하는 변수

    // 몬스터가 플레이어에게 공격받았을 때 데미지 입는 애니메이션 실행
    [Header("애니메이션 실행을 위해 필요한 변수")]
    private Animator anim;

    // 애니메이션 이름 정리 : Parameter이름이 바뀔 때 readonly에 있는 이름만 바꾸면 모두 적용이 된다.

    private readonly string takeDamageAnimationName = "IsHit";  
    private readonly string DeathAnimationName = "doDeath";

    private void Awake()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        // 초기화 : 변수에 데이터를 처음 할당 해주는 것
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        // 플레이어를 탐색 하는 기능 - 탐색 최대 거리 내에 플레이어가 있는가?

        target = FindObjectOfType<PlayerControllerSample>().gameObject.transform;

        if (findDistance >= Vector3.Distance(transform.position, target.position) && !isAttack && !isEnemyAttackEnable) // 타겟과의 거리를 계산하는 식
        {
            Debug.Log(Vector3.Distance(transform.position, target.position));

            agent.SetDestination(target.position);   // 플레이어를 쫓는 기능
         
        }

        // 탐색한 플레이어를 공격하는 기능

        attackCheckTime += Time.deltaTime;    // attackCheckTime이 attackCoolTime쿨타임보다 커지면 공격하라

        if (attackRange >= Vector3.Distance(transform.position, target.position) && !isAttack)
        {
            // 현재 플레이어가 공격 범위 안에 있는지 체크 하는 변수 ( True / False ) -> Bool
            isEnemyAttackEnable = true;
            // 공격 쿨타임을 계산할 시간 변수 -> float

            // 공격을 할지 말지 계산한다.
            if(attackCheckTime >= attackCoolTime)
            {
                // 공격한다.
                isAttack = true;
                anim.CrossFade("Attack01", 0.2f);                  // 코드가 실행되면 공격하는 애니메이션이 실행되는대.
                attackCheckTime = 0;
            }
        }
        else
        {
            isEnemyAttackEnable = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.position);

        Gizmos.DrawWireSphere(transform.position, findDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage()
    {
        // 죽음 상태라는 조건일 때 TakeDamage 함수를 실행시키지 않겠다.
        if (isDeath) return;

        hitCount++;                                                   // 피격된 수가 1 증가
        anim.SetBool(takeDamageAnimationName, true);                  // 피격 애니메이션 실행
        StartCoroutine(TakeDamageEffect());

        if(hitCount >= maxHP)
        {
            hitCount = 0;
            OnDeath();
        }
    }

    IEnumerator TakeDamageEffect()
    {
        // 데미지를 입었을 때 효과 구현 부분
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.15f);
        skinMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(hitBackTime);

        anim.SetBool(takeDamageAnimationName, false);

        // 데미지 종료를 알리는 코드 구현 부분
    }

    private void OnDeath()
    {
        isDeath = true;
        anim.SetTrigger(DeathAnimationName);
    }

    public void DestroyGameObject()         // 애니메이션 이벤트 함수로 사용한다. 애니메이션 클립에 특정 위치에서 호출할 수 있다.
    {
        Destroy(gameObject);
    }
}
