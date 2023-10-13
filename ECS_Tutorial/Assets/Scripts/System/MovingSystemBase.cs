using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        foreach(MoveToPositionAspect moveToPositionAspect in SystemAPI.Query
            <MoveToPositionAspect>())
        {
            moveToPositionAspect.Move(SystemAPI.Time.DeltaTime);
        }

        // Entities ForEach
        //Entities.ForEach((LocalTransform localTransform) =>
        //{
        //    localTransform.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
        //}).Run();
    }
}
