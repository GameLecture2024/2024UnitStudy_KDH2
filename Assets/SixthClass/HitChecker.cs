using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("���Ϳ� �浹�Ͽ���!");
            // ���Ͱ� �״´�. HP�� ���δ�. ���� �ǰ� ȿ���� �߻��Ѵ�.
            Destroy(other.gameObject);
        }
    }

}
