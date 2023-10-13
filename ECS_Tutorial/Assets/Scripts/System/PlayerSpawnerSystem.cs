using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery playerEntityQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag));

        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        EntityCommandBuffer entityCommandBuffer = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        int spawnerAmount = 100;
        if(playerEntityQuery.CalculateEntityCount() < spawnerAmount)
        {
            Entity spawnedEntity =  entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
            //EntityManager.Instantiate(playerSpawnerComponent.playerPrefab);
            entityCommandBuffer.SetComponent(spawnedEntity, new Speed
            {
                value = randomComponent.ValueRW.random.NextFloat(1f, 5f)
            });
        }
    }
}
