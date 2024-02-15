using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMeteo : MonoBehaviour
{
    public float damageDelay = 2f; // 데미지 입히기까지의 지연 시간
    public int wizardDamage = 1;

    private bool triggered = false; // OnTriggerEnter 이벤트가 발생했는지 여부를 나타내는 플래그
    private float timer = 0f; // 경과 시간을 계산하기 위한 타이머 변수

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

        timer += Time.deltaTime; // 경과 시간 업데이트

        if (timer >= damageDelay)
        {
            // 여기에 데미지를 입히는 코드를 작성합니다.
            // 예를 들어, 다른 오브젝트에 대미지를 입히는 함수를 호출하거나
            // 플레이어의 체력을 감소시키는 등의 작업을 수행할 수 있습니다.

            triggered = true; // 초기화
        }
    }
}
