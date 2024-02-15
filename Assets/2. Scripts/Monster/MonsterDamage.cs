using System.Collections;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isPoisoned = false; // 중독 상태 여부를 저장하는 변수

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
            // 중독에 따른 데미지를 처리하거나 상태를 업데이트하는 등의 작업을 수행
            yield return new WaitForSeconds(1f); // 중독 상태 지속 시간 (예: 1초마다 업데이트)
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        // 사망 처리 및 몬스터 오브젝트 제거 등을 진행
        Destroy(gameObject, 1.5f);
    }
}
