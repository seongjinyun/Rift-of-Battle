using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject objectToSummon; // ��ȯ�� ������Ʈ
    public float summonInterval = 6f; // ��ȯ ����
    private Animator anim;

    private Transform playerTransform; // �÷��̾��� ��ġ

    private float timer = 0f; // ��ȯ Ÿ�̸�

    private void Start()
    {
        // �÷��̾��� ��ġ�� �����ϱ� ���� �÷��̾��� Transform ������Ʈ�� ������
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ÿ�̸Ӹ� ������Ʈ
        timer += Time.deltaTime;

        // ���� �ð����� ������Ʈ�� ��ȯ
        if (timer >= summonInterval)
        {
            SummonObject();
            timer = 0f;
        }

        // �÷��̾ �ٶ󺸵��� ȸ��
        transform.LookAt(playerTransform);
    }

    private void SummonObject()
    {
        // �÷��̾��� ��ġ�� �������� ������Ʈ�� ��ȯ
        anim.SetTrigger("Attack");
        Instantiate(objectToSummon, playerTransform.position, Quaternion.identity);
    }
}

