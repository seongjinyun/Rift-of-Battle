using System.Collections;
using UnityEngine;

public class PlayerMove : Unit
{
    private bool canDash = true;
    public float dashCooldown = 3f; // �뽬 ��
    public float dashDistance = 5f;
    public GameObject[] dashEff; // 0 ����Ʈ, 1 ���� ��ġ
    public AudioClip runSound;

    private void Update()
    {

        if (PlayerAttack.canAttack == false && !PlayerDie.playerDie)
        {
            Move();
        }
        Dash();
    }

    public void RunSound()
    {
        SoundManager.instance.PlaySound(runSound);
    }

    /*    private void Move() // ���� �ڵ�
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                playerAnim.SetBool("Run", true);


                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                playerAnim.SetBool("Run", false);
            }
        }*/
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            playerAnim.SetBool("Run", true);

            // ī�޶� �ٶ󺸴� �������� �̵� ���͸� ��ȯ
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0f; // �÷��̾ y������ �̵����� �ʵ��� y���� 0���� ����
            moveDirection = Quaternion.LookRotation(cameraForward) * moveDirection;

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            playerAnim.SetBool("Run", false);
        }
    }


    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            playerAnim.SetTrigger("Dash");
            //dashEff[0].SetActive(true);
            /*GameObject eff = Instantiate(dashEff[0], dashEff[1].transform.position, Quaternion.identity);
            eff.transform.SetParent(transform); // �ڽİ�ü�� ����
            eff.transform.rotation = transform.rotation;
            Destroy(eff, 0.5f);*/
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        canDash = false;

        Vector3 dashDirection = transform.forward;
        float dashTime = 0.1f;
        float elapsedTime = 0f;

        Quaternion originalRotation = transform.rotation; // ���� ȸ���� ����

        while (elapsedTime < dashTime)
        {
            transform.rotation = originalRotation; // ȸ������ ���� ������ ����
            transform.Translate(dashDirection * dashDistance / dashTime * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        //dashEff[0].SetActive(false);

        canDash = true;
    }


}
