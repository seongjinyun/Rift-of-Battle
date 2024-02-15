using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    public static int currentHealth;
    private Animator animator;
    public GameObject damParticle;
    public Transform body; // ��ƼŬ ��ġ
    public AudioClip damSound; // �÷��̾� �ǰ� ����

    public GameObject diePanel; // ����г� Ȱ��ȭ

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
        // ��� ó�� �� ���� ������Ʈ ���� ���� ����
        Debug.Log("�÷��̾� ���");
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
