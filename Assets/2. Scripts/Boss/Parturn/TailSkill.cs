using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSkill : MonoBehaviour
{
    public int damageAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �� ���� �������� �������� ���� �߰�
        {
            PlayerDie playerDie = other.GetComponent<PlayerDie>();
            playerDie.PlayerTakeDamage(damageAmount);

        }
    }
}
