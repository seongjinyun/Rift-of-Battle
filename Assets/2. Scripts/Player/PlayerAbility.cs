using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    public List<string> abilities; // �ɷ� ����Ʈ
    public List<Sprite> abilityImages; // �ɷ� �̹��� ����Ʈ
    public List<Transform> buttonParents; // ��ư���� ��ġ�� �θ� ��ü ����Ʈ
    public GameObject buttonPrefab; // ��ư ������
    public GameObject imageHolder; // �̹����� ǥ���� ������Ʈ

    private HashSet<string> selectedAbilities = new HashSet<string>(); // ���õ� �ɷµ��� ����

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
        Time.timeScale = 0; // ���� �ð��� �����Ͽ� �Ͻ����� ȿ��

        GenerateRandomAbilityButtons();
    }

    private void GenerateRandomAbilityButtons()
    {
        // ������ ��ư���� ����
        foreach (Transform parent in buttonParents)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        int buttonCount = 3; // ������ ��ư�� ����

        // ��ư ������ �ɷ� �������� ���� ���, ��ư ������ �ɷ� ������ ����
        if (buttonCount > abilities.Count)
        {
            buttonCount = abilities.Count;
        }

        // ��ư ���� �� ����
        List<string> randomAbilities = abilities.OrderBy(x => Random.value).Take(buttonCount).ToList(); // �������� ���õ� �ɷ� ����Ʈ

        for (int i = 0; i < buttonCount; i++)
        {
            string ability = randomAbilities[i];
            GameObject buttonObject = Instantiate(buttonPrefab, buttonParents[i]);
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => ChooseAbility(buttonObject, ability));

            // �̹��� ����
            Image buttonImage = buttonObject.GetComponent<Image>();
            Text buttonText = buttonObject.GetComponentInChildren<Text>();

            if (buttonImage != null && i < abilityImages.Count)
            {
                int abilityIndex = abilities.IndexOf(ability); // ���õ� �ɷ��� �ε���
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

        // ���õ� �ɷ¿� ���� �̵� �ӵ� ����
        if (ability == abilities[0])  // �̵��ӵ� �ִ�ġ
        {
            Skill1 = true;
            PlayerSkillList.instance.SpeedUpSkill();
        }
        else if (ability == abilities[1]) // �ѹ� ������
        {
            Skill2 = true;
            PlayerSkillList.instance.OneKillSkill();

        }
        else if (ability == abilities[2]) // ���ݷ� �ι�
        {
            Skill3 = true;
            PlayerSkillList.instance.DoubleDamageSkill();

        }
        else if (ability == abilities[3]) // ���Ÿ� ����ü
        {
            Skill4 = true;
            PlayerSkillList.instance.FarAttackSkill();

        }
        else if (ability == abilities[4]) // ���ݼӵ� �ִ�ġ
        {
            Skill5 = true;
            PlayerSkillList.instance.AttackSpeedSKill();

        }
        else if (ability == abilities[5]) // ü�� ����
        {
            Skill6 = true;
            PlayerSkillList.instance.InfinityHpSkill();

        }
        else if (ability == abilities[6]) // �� ���� ���� ����
        {
            Skill7 = true;
            PlayerSkillList.instance.LongSwordSkill();
        }
        else if (ability == abilities[7]) // ���� �� ���� ������ �ο�
        {
            Skill8 = true;
            PlayerSkillList.instance.PoisonSkill();
        }
        else if (ability == abilities[8]) // 7�ʸ��� �������� �ϰ� �߻�
        {
            Skill9 = true;
            PlayerSkillList.instance.SwordBlow();
        }
        else if (ability == abilities[9]) // ���� �� �ֺ� �� ���� (���ҵ� ������)
        {
            Skill10 = true;
            PlayerSkillList.instance.GiantSkill();
        }

        // ���õ� ��ư�� ����
        Destroy(buttonObject);
        Time.timeScale = 1; // ���� �ð��� �����Ͽ� �Ͻ����� ȿ��
        PauseUi.isPaused = false;

        // ��� ��ư ����
        foreach (Transform parent in buttonParents)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        // �̹��� ����
        int abilityIndex = abilities.IndexOf(ability); // ���õ� �ɷ��� �ε���
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
