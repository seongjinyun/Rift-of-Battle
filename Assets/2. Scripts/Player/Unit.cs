using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public static int playerHp = 7;
    public static int playerDamage = 10;
    public static int playerSpeed = 10;
    public static float playerAttSpeed = 0.7f;
    public static int blowDamage = 40;
    public static int farDamage = 3;
    public static int lightningDamage = 4;

    public Animator playerAnim;
    public Rigidbody rigid;

    public void Awake() // 스테이지 클리어 시 스탯 초기화
    {
        playerHp = 7;
        playerDamage = 10;
        playerSpeed = 10;
        playerAttSpeed = 0.7f;

        gameObject.transform.localScale = new Vector3(1, 1, 1);



        /*PlayerSkillList.instance.atkRange.center = new Vector3(0, 0, 0);
        PlayerSkillList.instance.atkRange.radius = 0.73f;
        PlayerSkillList.instance.sword.localScale = new Vector3(1, 1, 1);
        PlayerSkillList.instance.poison = false;
        PlayerSkillList.instance.blow = false;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>(); 
        
        PlayerAbility.Skill1 = false;
        PlayerAbility.Skill2 = false;
        PlayerAbility.Skill3 = false;
        PlayerAbility.Skill4 = false;
        PlayerAbility.Skill5 = false;
        PlayerAbility.Skill6 = false;
        PlayerAbility.Skill7 = false;
        PlayerAbility.Skill8 = false;
        PlayerAbility.Skill9 = false;
        PlayerAbility.Skill10 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
