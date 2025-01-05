using UnityEngine;

public class C_DoorController : MonoBehaviour
{
    public Animator animator; // Sprite�� ����� Animator
    public float activationDistance = 3.0f; // �÷��̾���� �Ÿ� ���� (����: ����Ƽ �Ÿ�)
    public string animationName = "C_Door"; // �ִϸ��̼� �̸�

    private Transform playerTransform;
    private bool isAnimationPlaying = false; // �ִϸ��̼��� ���� ������ ����
    private bool isTriggeredOnce = false; // �÷��̾ ������ �� ���� �ִ��� Ȯ��

    private void Start()
    {
        // Player�� Transform�� ã�Ƽ� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        // Animator�� �ʱ� ���¿��� ��Ȱ��ȭ
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    private void Update()
    {
        if (playerTransform == null || animator == null)
            return;

        // Player���� �Ÿ� ���
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= activationDistance && !isAnimationPlaying)
        {
            // �÷��̾ ���� �Ÿ� �ȿ� ������ �ִϸ��̼��� ���� ���� �ƴϸ� ����
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {
            if (!isTriggeredOnce)
            {
                animator.enabled = true; // ó������ �ִϸ��̼� Ȱ��ȭ
                isTriggeredOnce = true; // ���� ���� Ȯ�� �÷���
            }

            animator.Play(animationName, 0, 0f); // �ִϸ��̼� ó������ ���
            isAnimationPlaying = true;

            // �ִϸ��̼� ���̸� �������� ���� �ʱ�ȭ �ڷ�ƾ ����
            float animationLength = GetAnimationClipLength(animationName);
            Invoke(nameof(ResetAnimationState), animationLength);
        }
    }

    private void ResetAnimationState()
    {
        isAnimationPlaying = false; // �ִϸ��̼� ���� �ʱ�ȭ
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (animator.runtimeAnimatorController != null)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip.length; // �ִϸ��̼� Ŭ�� ���� ��ȯ
                }
            }
        }
        Debug.LogError($"'{clipName}' �ִϸ��̼� Ŭ���� ã�� �� �����ϴ�.");
        return 0f;
    }
}
