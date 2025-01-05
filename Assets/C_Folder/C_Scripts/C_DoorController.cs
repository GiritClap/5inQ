using UnityEngine;

public class C_DoorController : MonoBehaviour
{
    public Animator animator; // Sprite에 연결된 Animator
    public float activationDistance = 3.0f; // 플레이어와의 거리 기준 (단위: 유니티 거리)
    public string animationName = "C_Door"; // 애니메이션 이름

    private Transform playerTransform;
    private bool isAnimationPlaying = false; // 애니메이션이 실행 중인지 여부
    private bool isTriggeredOnce = false; // 플레이어가 가까이 간 적이 있는지 확인

    private void Start()
    {
        // Player의 Transform을 찾아서 참조
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }

        // Animator를 초기 상태에서 비활성화
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    private void Update()
    {
        if (playerTransform == null || animator == null)
            return;

        // Player와의 거리 계산
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= activationDistance && !isAnimationPlaying)
        {
            // 플레이어가 일정 거리 안에 들어오고 애니메이션이 실행 중이 아니면 실행
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        if (animator != null)
        {
            if (!isTriggeredOnce)
            {
                animator.enabled = true; // 처음으로 애니메이션 활성화
                isTriggeredOnce = true; // 최초 실행 확인 플래그
            }

            animator.Play(animationName, 0, 0f); // 애니메이션 처음부터 재생
            isAnimationPlaying = true;

            // 애니메이션 길이를 기준으로 상태 초기화 코루틴 실행
            float animationLength = GetAnimationClipLength(animationName);
            Invoke(nameof(ResetAnimationState), animationLength);
        }
    }

    private void ResetAnimationState()
    {
        isAnimationPlaying = false; // 애니메이션 상태 초기화
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (animator.runtimeAnimatorController != null)
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    return clip.length; // 애니메이션 클립 길이 반환
                }
            }
        }
        Debug.LogError($"'{clipName}' 애니메이션 클립을 찾을 수 없습니다.");
        return 0f;
    }
}
