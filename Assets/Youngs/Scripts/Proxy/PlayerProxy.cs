using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


//@Youngs 2019年4月20日17:23:21 新技术的里程碑
//@Youngs 2019年4月25日21:48:56

/*
 *  该脚本用于添加玩家角色的各种组件
 * 
 */

namespace ILab.Youngs
{
    [RequiresEntityConversion]
    public class PlayerProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        [Tooltip("这个字段里直接丢玩家的最终模型")]
        public Mesh m_prefabMesh;

        public float3 m_respawnPosition;
        public float m_playerMoveSpeed;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerData
            {
                respawnPosition = m_respawnPosition,
                moveSpeed = m_playerMoveSpeed
            });
            dstManager.AddComponentData(entity, new PlayerInput { });
           
        }

        private void Awake()
        {
            this.gameObject.GetComponent<MeshFilter>().mesh = m_prefabMesh;
        }
    }
}
