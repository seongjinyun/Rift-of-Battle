using System.Collections;
using UnityEngine;

public class PlayerAttack : Unit
{
    private Animator animator;
    private bool isAttacking = false;
    public static bool canAttack = false;
    public GameObject attRange;
    public GameObject dashAtkRange;
    public int poisonDamage = 1;

    public GameObject atkEff;
    public AudioClip atkSound; // 적 피격 사운드
    public AudioClip atkSound2; // 검 휘두르는 사운드

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        // 공격 애니메이션 재생
        animator.SetTrigger("Attack");
        SoundManager.instance.PlayAtkSound(atkSound2);

        canAttack = true;
        yield return new WaitForSeconds(0.3f); // 공격 시 이동 멈춤
        canAttack = false;


        // 공격 애니메이션의 길이만큼 대기
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(playerAttSpeed);

        isAttacking = false;

    }

    public void Coll() // 기본 공격 / 범위를 온오프 해주면서 
    {
        attRange.SetActive(true);
    }
    public void CollFalse()
    {
        attRange.SetActive(false);
    }
    
    public void DashColl()
    {
        dashAtkRange.SetActive(true);
    }
    public void DashCollOff()
    {
        dashAtkRange.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("공격" + playerDamage);
            other.gameObject.GetComponent<MonsterDamage>().TakeDamage(playerDamage);
            SoundManager.instance.PlayAtkSound(atkSound);
            GameObject eff = Instantiate(atkEff, other.transform.position, Quaternion.identity);
            Destroy(eff, 0.2f);

            if (PlayerSkillList.instance.poison)
            {
                other.gameObject.GetComponent<MonsterDamage>().ApplyPoison(); // 몬스터에게 중독 상태 적용
            }
        }
    }
}
