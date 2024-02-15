using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSkill : MonoBehaviour
{
    public int damageAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 한 번만 데미지를 입히도록 조건 추가
        {
            PlayerDie playerDie = other.GetComponent<PlayerDie>();
            playerDie.PlayerTakeDamage(damageAmount);

        }
    }
}
