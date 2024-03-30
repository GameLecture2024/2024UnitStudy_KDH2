using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("몬스터와 충돌하였음!");
            // 몬스터가 죽는다. HP가 깍인다. 몬스터 피격 효과가 발생한다.

            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage();
        }

        if (other.gameObject.CompareTag("Player")) 
        {
            PlayerHitController hitController = other.gameObject.GetComponent<PlayerHitController>();

            hitController.TakeDamage();
        }

    }

}
