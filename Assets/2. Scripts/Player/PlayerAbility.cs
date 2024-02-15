using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    public List<string> abilities; // 능력 리스트
    public List<Sprite> abilityImages; // 능력 이미지 리스트
    public List<Transform> buttonParents; // 버튼들을 배치할 부모 객체 리스트
    public GameObject buttonPrefab; // 버튼 프리팹
    public GameObject imageHolder; // 이미지를 표시할 오브젝트

    private HashSet<string> selectedAbilities = new HashSet<string>(); // 선택된 능력들의 집합

    public static bool Skill1 = false;
    public static bool Skill2 = false;
    public static bool Skill3 = false;
    public static bool Skill4 = false;
    public static bool Skill5 = false;
    public static bool Skill6 = false;
    public static bool Skill7 = false;
    public static bool Skill8 = false;
    public static bool Skill9 = false;
    public static bool Skill10 = false;
    
    private void Start()
    {
        PauseUi.isPaused = true;
        Time.timeScale = 0; // 게임 시간을 정지하여 일시정지 효과

        GenerateRandomAbilityButtons();
    }

    private void GenerateRandomAbilityButtons()
    {
        // 기존의 버튼들을 삭제
        foreach (Transform parent in buttonParents)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        int buttonCount = 3; // 생성할 버튼의 개수

        // 버튼 개수가 능력 개수보다 많을 경우, 버튼 개수를 능력 개수로 설정
        if (buttonCount > abilities.Count)
        {
            buttonCount = abilities.Count;
        }

        // 버튼 생성 및 설정
        List<string> randomAbilities = abilities.OrderBy(x => Random.value).Take(buttonCount).ToList(); // 랜덤으로 선택된 능력 리스트

        for (int i = 0; i < buttonCount; i++)
        {
            string ability = randomAbilities[i];
            GameObject buttonObject = Instantiate(buttonPrefab, buttonParents[i]);
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => ChooseAbility(buttonObject, ability));

            // 이미지 설정
            Image buttonImage = buttonObject.GetComponent<Image>();
            Text buttonText = buttonObject.GetComponentInChildren<Text>();

            if (buttonImage != null && i < abilityImages.Count)
            {
                int abilityIndex = abilities.IndexOf(ability); // 선택된 능력의 인덱스
                buttonImage.sprite = abilityImages[abilityIndex];
            }

            if (buttonText != null)
            {
                buttonText.text = ability;
            }
        }
    }

    private void ChooseAbility(GameObject buttonObject, string ability)
    {
        Debug.Log("Selected Ability: " + ability);

        // 선택된 능력에 따라 이동 속도 변경
        if (ability == abilities[0])  // 이동속도 최대치
        {
            Skill1 = true;
            PlayerSkillList.instance.SpeedUpSkill();
        }
        else if (ability == abilities[1]) // 한방 데미지
        {
            Skill2 = true;
            PlayerSkillList.instance.OneKillSkill();

        }
        else if (ability == abilities[2]) // 공격력 두배
        {
            Skill3 = true;
            PlayerSkillList.instance.DoubleDamageSkill();

        }
        else if (ability == abilities[3]) // 원거리 투사체
        {
            Skill4 = true;
            PlayerSkillList.instance.FarAttackSkill();

        }
        else if (ability == abilities[4]) // 공격속도 최대치
        {
            Skill5 = true;
            PlayerSkillList.instance.AttackSpeedSKill();

        }
        else if (ability == abilities[5]) // 체력 무한
        {
            Skill6 = true;
            PlayerSkillList.instance.InfinityHpSkill();

        }
        else if (ability == abilities[6]) // 검 공격 범위 증가
        {
            Skill7 = true;
            PlayerSkillList.instance.LongSwordSkill();
        }
        else if (ability == abilities[7]) // 공격 시 지속 데미지 부여
        {
            Skill8 = true;
            PlayerSkillList.instance.PoisonSkill();
        }
        else if (ability == abilities[8]) // 7초마다 전방으로 일격 발사
        {
            Skill9 = true;
            PlayerSkillList.instance.SwordBlow();
        }
        else if (ability == abilities[9]) // 공격 시 주변 적 공격 (감소된 데미지)
        {
            Skill10 = true;
            PlayerSkillList.instance.GiantSkill();
        }

        // 선택된 버튼을 삭제
        Destroy(buttonObject);
        Time.timeScale = 1; // 게임 시간을 정지하여 일시정지 효과
        PauseUi.isPaused = false;

        // 모든 버튼 삭제
        foreach (Transform parent in buttonParents)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        // 이미지 설정
        int abilityIndex = abilities.IndexOf(ability); // 선택된 능력의 인덱스
        if (abilityIndex >= 0 && abilityIndex < abilityImages.Count)
        {
            Image imageComponent = imageHolder.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = abilityImages[abilityIndex];
            }
        }
    }

    private void Update()
    {
    }

}
