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


    // 이동 함수
    public void Move(float deltaTime)
    {
        // 방향
        float3 direction = math.normalize(targetPosition.ValueRW.value - localTransform.ValueRW.Position);
        // 이동
        localTransform.ValueRW.Position += direction * deltaTime * speed.ValueRO.value;
    }
    // 새로운 타겟 지점 생성 함수 
    public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
    {
        float threshHold = 0.5f;
        if (math.distance(localTransform.ValueRW.Position, targetPosition.ValueRW.value) < threshHold) 
        {
            //새로운 경로 생성
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
