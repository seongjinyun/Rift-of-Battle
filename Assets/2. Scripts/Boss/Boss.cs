using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;

    private Transform playerTransform; // �÷��̾��� ��ġ

    public GameObject firePrefab; // ���̾ ������
    public GameObject[] firePos; // ���̾ ������ġ 0 - ����, 1 - ����

    public GameObject screamObj; // ���׿� ����  ������Ʈ
    public BoxCollider tailBox; // ���� ���� �ݶ��̴�

    private float delay = 0f;
    private float delay2 = 0f;

    private bool turn = true;
    public float rotationSpeed = 4f; //ȸ�� ���ǵ�

    public static bool bossMove = false;
    public bool flyAttack = false;

    bool isPatternRunning = false;
    bool isCoroutineRunning = false;

    CapsuleCollider coll;
    private void Start()
    {
        // �÷��̾��� ��ġ�� �����ϱ� ���� �÷��̾��� Transform ������Ʈ�� ������
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

        // õõ�� ȸ���ϱ� ���� Quaternion.Lerp ���
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

    public void FireBall() // ���̾ ����
    {
        GameObject projectile = Instantiate(firePrefab, firePos[0].transform.position, Quaternion.identity);
        // ���̾�� �̵� ���� ����
        Vector3 direction = playerTransform.position - firePos[0].transform.position;
        projectile.transform.forward = direction.normalized;
    }

    public void FlyFireBall() // ���̾ ����
    {
        GameObject projectile = Instantiate(firePrefab, firePos[1].transform.position, Quaternion.identity);
        // ���̾�� �̵� ���� ����
        Vector3 direction = playerTransform.position - firePos[1].transform.position;
        projectile.transform.forward = direction.normalized;
    }

    public void BossMove() // �÷��̾� �������� �̵�
    {
        anim.SetBool("Walk",true);
        // �÷��̾��� ��ġ�� ������ ��ġ�� ���̸� ���մϴ�.
        Vector3 direction = playerTransform.position - transform.position;

        // ������ �÷��̾ �ٶ󺸵��� ȸ���մϴ�.
        transform.rotation = Quaternion.LookRotation(direction);

        // ������ �ӵ��� �����մϴ�.
        float moveSpeed = 1.5f;

        // ������ �÷��̾� �������� �̵��մϴ�.
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

    public void ScreamSkill() // ���׿�
    {
        // �÷��̾��� ��ġ�� �������� ������Ʈ�� ��ȯ
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
