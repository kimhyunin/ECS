using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject playerPrefab;
}

public class PlayerSpawnerBaker : Baker<PlayerSpawnerAuthoring>
{
    public override void Bake(PlayerSpawnerAuthoring authoring)
    {
        // GameObject to Entities
        AddComponent(new PlayerSpawnerComponent
        {
            playerPrefab = GetEntity(authoring.playerPrefab)
        });
    }
}