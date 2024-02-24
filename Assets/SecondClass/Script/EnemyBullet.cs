using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // "������ ��" �÷��̾� ��ġ�� �����ؼ� �ش� �������� �Ѿ��� �ӵ��� �߻�ȴ�.
    // �� �Ѿ��� �ٸ� �繰�� �浹���� �� �̺�Ʈ�� �߻��ϰ� �� ���� ������Ʈ�� �����Ѵ�.

    // (1) �÷��̾� ��ġ �����ϴ� ��� 
    public Transform PlayerTransform;

    // (2) �Ѿ��� �̵� �ӵ� x �������� �Ѿ��� �������� �����ϱ�
    public float bulletSpeed;
    // (3) �浹 �̺�Ʈ�� ���� 

    // (4) �Ѿ��� ������ ���ؼ� ������ �ش��ϴ� �κ��� �����Ұ̴ϴ�. �Ѿ� ��

    // Start is called before the first frame update
    // �ѹ��� �����Ѵ�.


    Vector3 caulateDirection;

    void Start() // ����Ƽ �����ϴ� �޼ҵ�
    {
        Initialize();
    }

    // Update is called once per frame
    // ��� �ϴ� �ݺ��ϴ� ���
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        transform.position += bulletSpeed * caulateDirection * Time.deltaTime;
        //transform.Translate(bulletSpeed * playerDirection * Time.deltaTime);
    }

    // �����ϴ�
    public void Initialize()
    {
        // �÷��̾��� ��ġ�� �޾ƿ����� �׽�Ʈ �غ���. 
        // �÷��̾��� ��ġ�� ã�Ƽ� �ش� ��ġ�� �Ѿ��� �߻��ϴ� ���.

        // ���� ������ �Ѿ��� �÷��̾ ����ؼ� �Ѿư��� ������
        // ó�� �÷��̾� ��ġ�� ���� ������ �� �������� �Ѿ��� �߻��ϴ� ����� ���� �̴ϴ�.

        // ������ �� �ѹ��� �÷��̾��� ��ġ�� �����ϰ�, �� ������ ��ġ�θ� �Ѿ��� ���� �մϴ�.

        Debug.Log($"���� �÷��̾��� ��ġ : {PlayerTransform}");
        PlayerTransform = GameObject.Find("Player").transform;
        Vector3 playerDirection = new Vector3(PlayerTransform.position.x, 0, PlayerTransform.position.z);
        caulateDirection = (playerDirection - transform.position).normalized;

        Destroy(gameObject, 3f);
    }

    // �Ѿ��� �ı��Ǿ�� �� ��ġ�� �� �Լ��� ��������ָ� �˴ϴ�.
    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    

    public void Test()
    {
        Debug.Log("�Ѿ��� �߻�Ǿ���");
    }

    // �Ѿ��� �浹���� �� �浹�� ������Ʈ�� ��ȣ�ۿ��� �� �� �ִ� ����Դϴ�.
    // �������� �浹�� ���� ���� ��ȣ�ۿ��� �Ͼ�� �̺�Ʈ�̱���.
    // Rigidbody, Collider�� �ݵ�� �ϳ� �̻��� ������Ʈ�� �����ؾ� �۵��Ѵ�.
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �÷��̾ �浹 ���� �� �÷��̾��� ���� �ִϸ��̼��� �����Ű�� ����� �����.

        // ���ӿ�����Ʈ Tag ���. 
        // Tag�� ������Ʈ�� �з������ν� ���� �浹�ϰ� ���� ������Ʈ�� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"�浹�� ���� ������Ʈ�� �̸� {collision.gameObject.name}");
            // �÷��̾��� ü���� ����߸��� ���.
            // �Ѿ��� �¾��� �� �ٷ� ���ӿ��� ���.
            collision.gameObject.GetComponent<PlayerController>().PlayerDeath();

            OnDestroy();
        }
    }
}
