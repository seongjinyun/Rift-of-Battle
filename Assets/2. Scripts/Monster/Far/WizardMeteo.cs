using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMeteo : MonoBehaviour
{
    public float damageDelay = 2f; // ������ ����������� ���� �ð�
    public int wizardDamage = 1;

    private bool triggered = false; // OnTriggerEnter �̺�Ʈ�� �߻��ߴ��� ���θ� ��Ÿ���� �÷���
    private float timer = 0f; // ��� �ð��� ����ϱ� ���� Ÿ�̸� ����

    private void OnTriggerStay(Collider other)
    {
        if (triggered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                triggered = false;
                timer = 0f;
                other.gameObject.GetComponent<PlayerDie>().PlayerTakeDamage(wizardDamage);
                Destroy(gameObject,0.8f);
            }
        }
    }
    private void Start()
    {
        Destroy(gameObject, 2.3f);

    }

    private void Update()
    {

        timer += Time.deltaTime; // ��� �ð� ������Ʈ

        if (timer >= damageDelay)
        {
            // ���⿡ �������� ������ �ڵ带 �ۼ��մϴ�.
            // ���� ���, �ٸ� ������Ʈ�� ������� ������ �Լ��� ȣ���ϰų�
            // �÷��̾��� ü���� ���ҽ�Ű�� ���� �۾��� ������ �� �ֽ��ϴ�.

            triggered = true; // �ʱ�ȭ
        }
    }
}
