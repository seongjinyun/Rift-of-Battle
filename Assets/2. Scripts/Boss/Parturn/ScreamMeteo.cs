using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamMeteo : MonoBehaviour
{
    public int damageAmount = 1; // 입힐 데미지 양
    bool hasDamaged = false; // 한 번만 데미지를 입히기 위한 변수

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player") && !hasDamaged) // 한 번만 데미지를 입히도록 조건 추가
        {
            PlayerDie playerDie = other.GetComponent<PlayerDie>();
            if (playerDie != null)
            {
                StartCoroutine(ApplyDamageWithDelay(playerDie)); // 딜레이와 함께 데미지를 입히는 코루틴 시작
            }
        }
    }

    private IEnumerator ApplyDamageWithDelay(PlayerDie playerDie)
    {
        hasDamaged = true; // 데미지를 입힌 후에 변수를 true로 설정하여 다음에는 데미지를 입히지 않도록 함
        playerDie.PlayerTakeDamage(damageAmount);
        yield return new WaitForSeconds(0.5f); // 0.5초 딜레이
        hasDamaged = false; // 딜레이 후에 다시 데미지를 입힐 수 있도록 변수를 false로 설정
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
