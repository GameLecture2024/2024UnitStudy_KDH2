using Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("�� �ǰ� �ִϸ��̼� ���� ����")]
    public float hitBackTime = 0.5f;       // �ǰ� ȿ�� �߻� �� ���� ���·� ���ư��� �ð�
    public int hitCount = 0;               // �� �� ���ݹ޾Ѵ��� �����ϴ� ����
    public int currentHP;                  // ���� ������ ü��
    public int maxHP = 3;                  // ������ �ִ� ü��
    private SkinnedMeshRenderer skinMeshRenderer; // �ǰ� �� ������ �������ֱ� ���� ���� ������ �����ϴ� ����
    private bool isDeath;                  // true �׾����ϱ� �ٸ� �ൿ ������!
    private NavMeshAgent agent;            // ��ã�� �븮�� Ŭ����(navMeshAgent)�����ϴ� ����

    [Header("�� ã�� ���� ����")]
    public Transform target;

    [Header("������ �ൿ ���� ����")]
    public float findDistance;              // Ÿ���� Ž�� �����ϴ� �ִ� �Ÿ�. �ִ� �Ÿ� �ۿ� �ִ� Ÿ���� ���� �ʴ´�/
    public float attackRange;               // ���� ���� ���� Ÿ���� ������ ������ �Ѵ�.

    [Header("������ ���� ���� ����")]
    public bool isEnemyAttackEnable;        // ���� �����ȿ� �÷��̾ ������ True, False ��ȯ�Ѵ�.
    public bool isAttack;                  // ������ ���� ���� �� True, ������ ������ false�� ��ȯ�Ѵ�.
    public float attackCoolTime;            // ��Ÿ���� �ִ� ���ȿ��� ���� ������ ���Ѵ�.            
    private float attackCheckTime;          // ��Ÿ���� �����ϴ� ����

    // ���Ͱ� �÷��̾�� ���ݹ޾��� �� ������ �Դ� �ִϸ��̼� ����
    [Header("�ִϸ��̼� ������ ���� �ʿ��� ����")]
    private Animator anim;

    // �ִϸ��̼� �̸� ���� : Parameter�̸��� �ٲ� �� readonly�� �ִ� �̸��� �ٲٸ� ��� ������ �ȴ�.

    private readonly string takeDamageAnimationName = "IsHit";  
    private readonly string DeathAnimationName = "doDeath";

    private void Awake()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        // �ʱ�ȭ : ������ �����͸� ó�� �Ҵ� ���ִ� ��
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        // �÷��̾ Ž�� �ϴ� ��� - Ž�� �ִ� �Ÿ� ���� �÷��̾ �ִ°�?

        target = FindObjectOfType<PlayerControllerSample>().gameObject.transform;

        if (findDistance >= Vector3.Distance(transform.position, target.position) && !isAttack && !isEnemyAttackEnable) // Ÿ�ٰ��� �Ÿ��� ����ϴ� ��
        {
            Debug.Log(Vector3.Distance(transform.position, target.position));

            agent.SetDestination(target.position);   // �÷��̾ �Ѵ� ���
         
        }

        // Ž���� �÷��̾ �����ϴ� ���

        attackCheckTime += Time.deltaTime;    // attackCheckTime�� attackCoolTime��Ÿ�Ӻ��� Ŀ���� �����϶�

        if (attackRange >= Vector3.Distance(transform.position, target.position) && !isAttack)
        {
            // ���� �÷��̾ ���� ���� �ȿ� �ִ��� üũ �ϴ� ���� ( True / False ) -> Bool
            isEnemyAttackEnable = true;
            // ���� ��Ÿ���� ����� �ð� ���� -> float

            // ������ ���� ���� ����Ѵ�.
            if(attackCheckTime >= attackCoolTime)
            {
                // �����Ѵ�.
                isAttack = true;
                anim.CrossFade("Attack01", 0.2f);                  // �ڵ尡 ����Ǹ� �����ϴ� �ִϸ��̼��� ����Ǵ´�.
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
        // ���� ���¶�� ������ �� TakeDamage �Լ��� �����Ű�� �ʰڴ�.
        if (isDeath) return;

        hitCount++;                                                   // �ǰݵ� ���� 1 ����
        anim.SetBool(takeDamageAnimationName, true);                  // �ǰ� �ִϸ��̼� ����
        StartCoroutine(TakeDamageEffect());

        if(hitCount >= maxHP)
        {
            hitCount = 0;
            OnDeath();
        }
    }

    IEnumerator TakeDamageEffect()
    {
        // �������� �Ծ��� �� ȿ�� ���� �κ�
        ShakeCamera.Instance.OnShakeCamera(0.1f, 0.15f);
        skinMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(hitBackTime);

        anim.SetBool(takeDamageAnimationName, false);

        // ������ ���Ḧ �˸��� �ڵ� ���� �κ�
    }

    private void OnDeath()
    {
        isDeath = true;
        anim.SetTrigger(DeathAnimationName);
    }

    public void DestroyGameObject()         // �ִϸ��̼� �̺�Ʈ �Լ��� ����Ѵ�. �ִϸ��̼� Ŭ���� Ư�� ��ġ���� ȣ���� �� �ִ�.
    {
        Destroy(gameObject);
    }
}
