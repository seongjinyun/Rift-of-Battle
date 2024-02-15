using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireBallSkill : MonoBehaviour
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
                playerDie.PlayerTakeDamage(damageAmount);
                hasDamaged = true; // 데미지를 입힌 후에 변수를 true로 설정하여 다음에는 데미지를 입히지 않도록 함
            }
        }
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }
}
