using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // "생성될 때" 플레이어 위치를 감지해서 해당 방향으로 총알의 속도로 발사된다.
    // 이 총알은 다른 사물과 충돌했을 때 이벤트가 발생하고 이 게임 오브젝트를 제거한다.

    // (1) 플레이어 위치 감지하는 기능 
    public Transform PlayerTransform;

    // (2) 총알의 이동 속도 x 방향으로 총알의 움직임을 구현하기
    public float bulletSpeed;
    // (3) 충돌 이벤트를 구현 

    // (4) 총알의 생명을 다해서 죽음에 해당하는 부분을 구현할겁니다. 총알 삭

    // Start is called before the first frame update
    // 한번만 실행한다.


    Vector3 caulateDirection;

    void Start() // 유니티 제공하는 메소드
    {
        Initialize();
    }

    // Update is called once per frame
    // 계속 하는 반복하는 기능
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        transform.position += bulletSpeed * caulateDirection * Time.deltaTime;
        //transform.Translate(bulletSpeed * playerDirection * Time.deltaTime);
    }

    // 시작하다
    public void Initialize()
    {
        // 플레이어의 위치를 받아오는지 테스트 해본다. 
        // 플레이어의 위치를 찾아서 해당 위치로 총알을 발사하는 기능.

        // 전에 구현한 총알은 플레이어를 계속해서 쫓아가는 문제점
        // 처음 플레이어 위치를 받은 다음에 그 방향으로 총알을 발사하는 기능을 만들 겁니다.

        // 시작할 때 한번만 플레이어의 위치를 저장하고, 그 저장한 위치로만 총알을 쏴야 합니다.

        Debug.Log($"현재 플레이어의 위치 : {PlayerTransform}");
        PlayerTransform = GameObject.Find("Player").transform;
        Vector3 playerDirection = new Vector3(PlayerTransform.position.x, 0, PlayerTransform.position.z);
        caulateDirection = (playerDirection - transform.position).normalized;

        Destroy(gameObject, 3f);
    }

    // 총알이 파괴되어야 할 위치에 이 함수를 실행시켜주면 됩니다.
    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    

    public void Test()
    {
        Debug.Log("총알이 발사되었음");
    }

    // 총알이 충돌했을 때 충돌한 오브젝트랑 상호작용을 할 수 있는 기능입니다.
    // 물리적인 충돌이 있을 때만 상호작용이 일어나는 이벤트이구요.
    // Rigidbody, Collider가 반드시 하나 이상의 오브젝트에 존재해야 작동한다.
    private void OnCollisionEnter(Collision collision)
    {
        // 총이 플레이어에 충돌 했을 때 플레이어의 죽음 애니메이션을 실행시키는 기능을 만든다.

        // 게임오브젝트 Tag 기능. 
        // Tag에 오브젝트를 분류함으로써 내가 충돌하고 싶은 오브젝트만 선정
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"충돌한 게임 오브젝트의 이름 {collision.gameObject.name}");
            // 플레이어의 체력을 떨어뜨리는 기능.
            // 총알을 맞았을 때 바로 게임오버 기능.
            collision.gameObject.GetComponent<PlayerController>().PlayerDeath();

            OnDestroy();
        }
    }
}
