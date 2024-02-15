using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAttack : MonoBehaviour
{
    public GameObject blowPrefab; // �ϰ� ������
    public Transform blowPoint; // �߻� ����
    public float cooldown = 7f; // ��ų ��ٿ� �ð�

    private float timer; // Ÿ�̸�


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSkillList.instance.blow)
        {
            timer += Time.deltaTime;

            if (timer >= cooldown)
            {
                timer = 0f;
                Blow();
            }
        }
    }

    void Blow()
    {
        // �÷��̾ �ٶ󺸴� �������� �߻� ���� ����
        Vector3 playerForward = transform.forward;
        blowPoint.rotation = Quaternion.LookRotation(playerForward);

        // �߻�ü ����
        GameObject projectile = Instantiate(blowPrefab, blowPoint.position, blowPoint.rotation);

        // �߻�ü�� ���ϴ� ���� �� ȿ�� ����
        // ����: �߻�ü�� ���� ���� �������� ���ư����� ����
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(blowPoint.forward * 10f, ForceMode.Impulse);
        Destroy(projectile, 3f);
    }
}
