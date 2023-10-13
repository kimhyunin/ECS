using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity entity;

    private readonly RefRW<LocalTransform> localTransform;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetPosition> targetPosition;


    // �̵� �Լ�
    public void Move(float deltaTime)
    {
        // ����
        float3 direction = math.normalize(targetPosition.ValueRW.value - localTransform.ValueRW.Position);
        // �̵�
        localTransform.ValueRW.Position += direction * deltaTime * speed.ValueRO.value;
    }
    // ���ο� Ÿ�� ���� ���� �Լ� 
    public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
    {
        float threshHold = 0.5f;
        if (math.distance(localTransform.ValueRW.Position, targetPosition.ValueRW.value) < threshHold) 
        {
            //���ο� ��� ����
            targetPosition.ValueRW.value = GetRandomPosition(randomComponent);
        }
    }

    private float3 GetRandomPosition(RefRW<RandomComponent> randomComponent)
    {
        return new float3(
            randomComponent.ValueRW.random.NextFloat(0f, 15f),
            0,
            randomComponent.ValueRW.random.NextFloat(0f, 15f));
    }

}
