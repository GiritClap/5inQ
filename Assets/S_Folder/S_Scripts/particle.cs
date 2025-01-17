using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ReverseSimulationParticles : MonoBehaviour
{
    private ParticleSystem particleSystem; // 파티클 시스템
    private float currentTime; // 현재 시뮬레이션 시간
    private float duration; // 파티클 재생 시간

    private bool isReversing = false; // 역재생 여부

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        duration = particleSystem.main.duration; // 파티클의 전체 지속 시간

        // 파티클 초기화
        currentTime = duration; // 끝점부터 시작
        particleSystem.Play();
        isReversing = true; // 역재생 시작
    }

    void Update()
    {
        if (isReversing && currentTime > 0)
        {
            // 시간을 감소시키며 역재생
            currentTime -= Time.deltaTime;
            particleSystem.Simulate(currentTime, true, false);
        }
        else if (isReversing)
        {
            // 역재생 완료 후 멈춤
            isReversing = false;
            particleSystem.Stop();
        }
    }
}
