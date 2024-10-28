using UnityEngine;

public class BossFlashAttack : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public Transform boss;    // ������ Transform
    public float flashDamageRange = 90f; // �÷��̾ �� �� �̻����� ������ �־�� ���ظ� ���� ����

    // ���� ���� �ߵ�
    public void TriggerFlashAttack()
    {
        Debug.Log("���� ������ �ߵ��Ǿ����ϴ�!");

        // �÷��̾ �������� ���� ������ �ִ��� Ȯ��
        if (IsPlayerLookingAway())
        {
            Debug.Log("�÷��̾ ������ ���߽��ϴ�.");
        }
        else
        {
            Debug.Log("�÷��̾ ������ �¾ҽ��ϴ�!");
            // �÷��̾�� ������ �ֱ�
        }
    }

    // �÷��̾ �������� ���� ������ �ִ��� Ȯ���ϴ� �Լ�
    private bool IsPlayerLookingAway()
    {
        // �÷��̾ ���� �ִ� ����
        Vector3 playerForward = player.forward;
        // ������ ��ġ���� �÷��̾��� ��ġ������ ����
        Vector3 directionToBoss = (boss.position - player.position).normalized;

        // �÷��̾ ������ �ݴ� ������ ���� �ִ��� Ȯ��
        float angle = Vector3.Angle(playerForward, directionToBoss);

        Debug.Log("�÷��̾�� ���� ���� ����: " + angle);

        // �÷��̾ flashDamageRange �̻����� �������� ���� ������ ������ ���� �� ����
        return angle > flashDamageRange;
    }
}
