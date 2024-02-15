using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamMeteo : MonoBehaviour
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
                StartCoroutine(ApplyDamageWithDelay(playerDie)); // �����̿� �Բ� �������� ������ �ڷ�ƾ ����
            }
        }
    }

    private IEnumerator ApplyDamageWithDelay(PlayerDie playerDie)
    {
        hasDamaged = true; // �������� ���� �Ŀ� ������ true�� �����Ͽ� �������� �������� ������ �ʵ��� ��
        playerDie.PlayerTakeDamage(damageAmount);
        yield return new WaitForSeconds(0.5f); // 0.5�� ������
        hasDamaged = false; // ������ �Ŀ� �ٽ� �������� ���� �� �ֵ��� ������ false�� ����
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
