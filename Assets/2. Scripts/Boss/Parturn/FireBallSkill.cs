using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireBallSkill : MonoBehaviour
{
    public int damageAmount = 1; // ���� ������ ��
    bool hasDamaged = false; // �� ���� �������� ������ ���� ����

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player") && !hasDamaged) // �� ���� �������� �������� ���� �߰�
        {
            PlayerDie playerDie = other.GetComponent<PlayerDie>();
            if (playerDie != null)
            {
                playerDie.PlayerTakeDamage(damageAmount);
                hasDamaged = true; // �������� ���� �Ŀ� ������ true�� �����Ͽ� �������� �������� ������ �ʵ��� ��
            }
        }
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }
}
