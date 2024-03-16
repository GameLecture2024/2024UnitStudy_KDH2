using FirstClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class PlayerControllerSample : MonoBehaviour
    {
        PlayerMoveSample playerMove;
        PlayerRotationSample playerRotation;

        Animator animator;

        public enum PlayerState { Idle, Run, Death, Attack01  }

        PlayerState playerstate;
        public BoxCollider HitCheckBox;  // 공격 체크용 박스
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                Debug.Log("NPC와 충돌했습니다!");
                var trigger = other.GetComponent<SampleTextTrigger>();
                trigger.TriggerText();
            }
            else
            {
                Debug.Log("태그가 NPC가 아닙니다.");
            }
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            playerstate = PlayerState.Idle;
            playerMove = new PlayerMoveSample();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (GameManager.Instance.IsPlayerDeath) return;

           
            SetPlayerState();
            playerMove.MovePlayer(this.transform);
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(AttackCoroutine());
                
            }
            SetPlayerAnimation();
        }

        private void SetAttack()
        {
            playerstate = PlayerState.Attack01;
            HitCheckBox.enabled = true;
            Invoke("SetAttackOff", 0.5f);
        }

        private void SetAttackOff()
        {
            HitCheckBox.enabled = false;
        }

        IEnumerator AttackCoroutine()
        {
            playerstate = PlayerState.Attack01;
            HitCheckBox.enabled = true;

            yield return new WaitForSeconds(0.5f);
            HitCheckBox.enabled = false;
        }

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

        private void SetPlayerAnimation()
        {
            if (playerstate == PlayerState.Idle)
            {
                PlayerIdle();
            }
            else if (playerstate == PlayerState.Run)
            {
                PlayerMove();
            }
            else if(playerstate == PlayerState.Attack01)
            {
                animator.SetTrigger("doAttack");
            }
        }

        public void PlayerDeath()
        {
            if (GameManager.Instance.IsPlayerDeath) return;

            GameManager.Instance.GameOver();

            animator?.SetTrigger("Death");
            GameManager.Instance.IsPlayerDeath = true;
        }

        public void PlayerMove()
        {
            animator.SetBool("IsRun", true);
        }

        public void PlayerIdle()
        {
            animator.SetBool("IsRun", false);
        }

    } 
}
