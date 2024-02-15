using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    public static int currentHealth;
    private Animator animator;
    public GameObject damParticle;
    public Transform body; // 파티클 위치
    public AudioClip damSound; // 플레이어 피격 사운드

    public GameObject diePanel; // 사망패널 활성화

    public static bool playerDie = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Unit.playerHp;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerAbility.Skill6 == true)
        {
            currentHealth = Unit.playerHp;
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        animator.SetTrigger("Damage");
        SoundManager.instance.PlayAtkSound(damSound);
        GameObject parti = Instantiate(damParticle, body.position, Quaternion.identity);
        Destroy(parti, 0.5f);
        currentHealth -= damage;
        Debug.Log(gameObject.name + currentHealth);
        //animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            playerDie = true;
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        // 사망 처리 및 몬스터 오브젝트 제거 등을 진행
        Debug.Log("플레이어 사망");
        StartCoroutine(StartS());
    }
    IEnumerator StartS()
    {
        yield return new WaitForSeconds(2f);
        diePanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("StartScene");

    }
}
