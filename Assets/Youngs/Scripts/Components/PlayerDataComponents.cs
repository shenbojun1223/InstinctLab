using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System;


//@youngs 2019年4月20日18:50:20
//@youngs 2019年4月20日20:01:27  尝试改为网络对战
//@youngs 2019年4月25日20:46:50

/*
*   
*   PlayerDataComponents包含构成一个玩家角色需要的所有组件
*   
*   
*/
namespace ILab.Youngs
{

    [Serializable]
    public struct PlayerData : IComponentData
    {
        public Entity entity;
        public float3 respawnPosition;
        public float moveSpeed;

    }

    [Serializable]
    public struct PlayerInput : IComponentData
    {

        public float3 inputMovementDirection;
        public BlittableBool pFire;

    }

    public struct PlayerDead : IComponentData
    {
        //仅仅只需要有这个组件就表明玩家死亡
    }

}
