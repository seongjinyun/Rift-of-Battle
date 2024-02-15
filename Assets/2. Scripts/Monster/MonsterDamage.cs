using System.Collections;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isPoisoned = false; // �ߵ� ���� ���θ� �����ϴ� ����

    private Animator animator;

    public GameObject poisonEff;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (isPoisoned)
        {
            poisonEff.SetActive(true);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
        Debug.Log(gameObject.name + currentHealth);
        //animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyPoison()
    {
        if (!isPoisoned)
        {
            isPoisoned = true;
            StartCoroutine(ApplyPoisonDamage());
        }
    }

    private IEnumerator ApplyPoisonDamage()
    {
        while (isPoisoned)
        {
            TakeDamage(1);
            // �ߵ��� ���� �������� ó���ϰų� ���¸� ������Ʈ�ϴ� ���� �۾��� ����
            yield return new WaitForSeconds(1f); // �ߵ� ���� ���� �ð� (��: 1�ʸ��� ������Ʈ)
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        // ��� ó�� �� ���� ������Ʈ ���� ���� ����
        Destroy(gameObject, 1.5f);
    }
}
