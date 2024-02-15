using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAttack : MonoBehaviour
{
    public GameObject blowPrefab; // 일격 프리팹
    public Transform blowPoint; // 발사 지점
    public float cooldown = 7f; // 스킬 쿨다운 시간

    private float timer; // 타이머


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
        // 플레이어가 바라보는 방향으로 발사 지점 설정
        Vector3 playerForward = transform.forward;
        blowPoint.rotation = Quaternion.LookRotation(playerForward);

        // 발사체 생성
        GameObject projectile = Instantiate(blowPrefab, blowPoint.position, blowPoint.rotation);

        // 발사체에 원하는 동작 및 효과 적용
        // 예시: 발사체에 힘을 가해 전방으로 날아가도록 설정
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(blowPoint.forward * 10f, ForceMode.Impulse);
        Destroy(projectile, 3f);
    }
}
