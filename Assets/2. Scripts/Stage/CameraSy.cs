using UnityEngine;

public class CameraSy : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target; // Player

    public float rotSensitive = 3f; // ī�޶� ȸ�� ����
    public float minDistance = 1f; // ī�޶�� �÷��̾� ������ �ּ� �Ÿ�
    public float maxDistance = 10f; // ī�޶�� �÷��̾� ������ �ִ� �Ÿ�
    private float RotationMin = -10f; // ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f; // ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f; // ī�޶� ȸ���ϴµ� �ɸ��� �ð�

    private Vector3 targetRotation;
    private Vector3 currentVel;

    [Header("Camera Position")]
    public Vector3 cameraOffset; // ī�޶��� ��ġ ������

    void LateUpdate() // Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive; // ���콺 �¿� �������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive; // ���콺 ���� �������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
        // Xaxis�� ���콺�� �Ʒ��� ���� ��(�������� �Է� �޾��� ��) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax); // X�� ȸ���� �Ѱ�ġ�� ���� �ʰ� �������ش�.

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        transform.eulerAngles = targetRotation; // SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��

        // ���콺 �ٷ� ī�޶� �� �Ÿ� ����
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel");
        float distance = Vector3.Distance(transform.position, target.position); // ���� ī�޶�� �÷��̾� ������ �Ÿ� ���
        distance -= zoomAmount;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        transform.position = target.position - transform.forward * distance + cameraOffset; // ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ ������ �ְ� ��� ����ȴ�.
    }
}
