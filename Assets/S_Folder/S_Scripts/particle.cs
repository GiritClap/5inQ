using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ReverseSimulationParticles : MonoBehaviour
{
    private ParticleSystem particleSystem; // ��ƼŬ �ý���
    private float currentTime; // ���� �ùķ��̼� �ð�
    private float duration; // ��ƼŬ ��� �ð�

    private bool isReversing = false; // ����� ����

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        duration = particleSystem.main.duration; // ��ƼŬ�� ��ü ���� �ð�

        // ��ƼŬ �ʱ�ȭ
        currentTime = duration; // �������� ����
        particleSystem.Play();
        isReversing = true; // ����� ����
    }

    void Update()
    {
        if (isReversing && currentTime > 0)
        {
            // �ð��� ���ҽ�Ű�� �����
            currentTime -= Time.deltaTime;
            particleSystem.Simulate(currentTime, true, false);
        }
        else if (isReversing)
        {
            // ����� �Ϸ� �� ����
            isReversing = false;
            particleSystem.Stop();
        }
    }
}
