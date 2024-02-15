using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillList : MonoBehaviour
{
    public static PlayerSkillList instance;
    public GameObject farChildObject; // 원거리 공격 나가는 곳 자식객체

    public Transform sword; // 무기
    public SphereCollider atkRange;
    public bool poison = false;
    public bool blow = false;

    public GameObject[] allEff;

    private void Awake()
    {
        PlayerSkillList.instance = this;
    }

    public void SpeedUpSkill() // 1 Eff - 0
    {
        if (PlayerAbility.Skill1 )
        {
            Unit.playerSpeed *= 2;
            GameObject childObject = Instantiate(allEff[0], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정

        }
    }

    public void OneKillSkill() // 2 Eff - 1
    {
        if (PlayerAbility.Skill2)
        {
            Unit.playerDamage += 10000;
            GameObject childObject = Instantiate(allEff[1], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정
        }
    }
    
    public void DoubleDamageSkill() // 3 Eff - 2
    {
        if (PlayerAbility.Skill3)
        {
            Unit.playerDamage *= 2;
            GameObject childObject = Instantiate(allEff[2], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정
        }
    }

    public void FarAttackSkill() // 4
    {
        if (PlayerAbility.Skill4)
        {
            // 자식 객체 활성화
            farChildObject.SetActive(true);    
        }
    }

    public void AttackSpeedSKill() // 5 Eff - 3
    {
        if (PlayerAbility.Skill5)
        {
            Unit.playerAttSpeed = 0.2f;
            GameObject childObject = Instantiate(allEff[3], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정
        }
    }
    
    public void InfinityHpSkill() // 6 Eff - 4
    {
        if (PlayerAbility.Skill6)
        {
            Unit.playerHp = 10000;
            GameObject childObject = Instantiate(allEff[4], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정
        }
    }

    public void LongSwordSkill() // 7
    {
        if (PlayerAbility.Skill7)
        {
            sword.transform.localScale = new Vector3(1, 2.5f, 1);

            atkRange.center = new Vector3(0, 0.73f, 0);
            atkRange.radius = 1.01f;
        }
    }

    public void PoisonSkill() // 8 Eff - 5
    {
        if (PlayerAbility.Skill8)
        {
            poison = true;
            GameObject childObject = Instantiate(allEff[5], transform.position, Quaternion.identity);
            childObject.transform.SetParent(transform); // 부모 객체의 자식으로 설정
        }
    }

    public void SwordBlow() // 9
    {
        if (PlayerAbility.Skill9)
        {
            blow = true;
        }
    }

    public void GiantSkill()
    {
        if (PlayerAbility.Skill10)
        {
            gameObject.transform.localScale = new Vector3(3, 3, 3);
        }
    }
}
