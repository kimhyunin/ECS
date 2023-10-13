using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
public class PlayerVisual : MonoBehaviour
{
    private Entity targetEntity;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetEntity = GetRandomEntity();
        }
        if(targetEntity != Entity.Null)
        {
            Vector3 followPosition = 
                World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorld>(targetEntity).Position;

            transform.position = followPosition;
        }
    }
    private Entity GetRandomEntity()
    {
        EntityQuery playerTagEntityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(PlayerTag));
        NativeArray<Entity> entityNativeArray =  playerTagEntityQuery.ToEntityArray(Unity.Collections.Allocator.Temp);
        if(entityNativeArray.Length > 0)
        {
            return entityNativeArray[Random.Range(0, entityNativeArray.Length)];
        } else
        {
            return Entity.Null;
        }
    }
}
