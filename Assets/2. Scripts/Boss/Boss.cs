using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;

    private Transform playerTransform; // 플레이어의 위치

    public GameObject firePrefab; // 파이어볼 프리팹
    public GameObject[] firePos; // 파이어볼 생성위치 0 - 지상, 1 - 공중

    public GameObject screamObj; // 메테오 패턴  오브젝트
    public BoxCollider tailBox; // 꼬리 공격 콜라이더

    private float delay = 0f;
    private float delay2 = 0f;

    private bool turn = true;
    public float rotationSpeed = 4f; //회전 스피드

    public static bool bossMove = false;
    public bool flyAttack = false;

    bool isPatternRunning = false;
    bool isCoroutineRunning = false;

    CapsuleCollider coll;
    private void Start()
    {
        // 플레이어의 위치를 추적하기 위해 플레이어의 Transform 컴포넌트를 가져옴
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    public void Update()
    {
 

        if (turn)
        {
            BossRotation();
        }

        if (!isPatternRunning && !isCoroutineRunning)
        {
            int randomPattern = Random.Range(0, 6);

            switch (randomPattern)
            {
                case 0:
                    StartCoroutine(FlyAtt());
                    flyAttack = true; 
                    break;
                case 1:
                    bossMove = true;
                    break;
                case 2:
                    StartCoroutine(Scream());
                    break;
                case 3:
                    StartCoroutine(Tail());
                    break;
                case 4:
                    StartCoroutine(Sleep());
                    break;
                case 5:
                    StartCoroutine(Fire());
                    break;
            }
        }

        if (bossMove)
        {
            isPatternRunning = true;

            BossMove();
        }

/*        if (flyAttack)
        {
            isPatternRunning = true;
            coll.enabled = false;
            anim.SetTrigger("FlyTake");
            anim.SetBool("FlyIdle", true);
            StartCoroutine(FlyAttack());
        }*/
        else
        {
            coll.enabled = true;
        }
    }
    public void BossRotation()
    {
        Vector3 direction = playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 천천히 회전하기 위해 Quaternion.Lerp 사용
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    IEnumerator Fire()
    {
        isPatternRunning = true;
        anim.SetTrigger("FireBallOn");

        yield return new WaitForSeconds(4f);

        isPatternRunning = false;
    }
    IEnumerator Scream()
    {
        anim.SetTrigger("Scream");
        isPatternRunning = true;

        yield return new WaitForSeconds(4f);
        isPatternRunning = false;
    }
    IEnumerator Tail()
    {
        anim.SetTrigger("TailAttack");
        isPatternRunning = true;

        yield return new WaitForSeconds(4f);
        isPatternRunning = false;
    }
    IEnumerator Sleep()
    {
        isPatternRunning = true;
        anim.SetBool("Sleep", true);
        turn = false;

        yield return new WaitForSeconds(5f);
        anim.SetBool("Sleep", false);
        turn = true;

        yield return new WaitForSeconds(2f);
        isPatternRunning = false;
    }

    public void FireBall() // 파이어볼 패턴
    {
        GameObject projectile = Instantiate(firePrefab, firePos[0].transform.position, Quaternion.identity);
        // 파이어볼의 이동 방향 설정
        Vector3 direction = playerTransform.position - firePos[0].transform.position;
        projectile.transform.forward = direction.normalized;
    }

    public void FlyFireBall() // 파이어볼 패턴
    {
        GameObject projectile = Instantiate(firePrefab, firePos[1].transform.position, Quaternion.identity);
        // 파이어볼의 이동 방향 설정
        Vector3 direction = playerTransform.position - firePos[1].transform.position;
        projectile.transform.forward = direction.normalized;
    }

    public void BossMove() // 플레이어 방향으로 이동
    {
        anim.SetBool("Walk",true);
        // 플레이어의 위치와 보스의 위치를 차이를 구합니다.
        Vector3 direction = playerTransform.position - transform.position;

        // 보스가 플레이어를 바라보도록 회전합니다.
        transform.rotation = Quaternion.LookRotation(direction);

        // 보스의 속도를 설정합니다.
        float moveSpeed = 1.5f;

        // 보스를 플레이어 방향으로 이동합니다.
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        delay += Time.deltaTime;

        if (delay >= 3f)
        {
            moveSpeed = 3f;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);

            delay2 += Time.deltaTime;

            if (delay2 >= 2f)
            {
                anim.SetBool("Run", false);
                bossMove = false;
                delay = 0;
                delay2 = 0;
                StartCoroutine(Mov());
            }
        }
    }
    IEnumerator Mov()
    {
        yield return new WaitForSeconds(4f);
        isPatternRunning = false;

    }

    public void ScreamSkill() // 메테오
    {
        // 플레이어의 위치를 기준으로 오브젝트를 소환
        Instantiate(screamObj, playerTransform.position, Quaternion.identity);
    }

    public void TailAtkOn()
    {
        tailBox.enabled = true;
    }
    public void TailAtkOff()
    {
        tailBox.enabled = false;
    }

    IEnumerator FlyAtt()
    {
        isPatternRunning = true;
        coll.enabled = false;
        anim.SetTrigger("FlyTake");
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("FlyIdle", true);
        StartCoroutine(FlyAttack());
    }

    IEnumerator FlyAttack()
    {
        anim.SetTrigger("FlyShoot");

        yield return new WaitForSeconds(4f);

        anim.SetBool("FlyIdle", false);
        anim.SetTrigger("FlyLand");
        flyAttack = false;

        yield return new WaitForSeconds(8f);
        isPatternRunning = false;
    }

}
