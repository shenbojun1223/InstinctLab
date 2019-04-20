using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


//@ILab.Youngs 2019年4月20日17:23:21 新技术的里程碑
/*
 *  该脚本用于添加玩家角色的各种组件
 * 
 */

namespace ILab.Youngs
{
    [RequiresEntityConversion]
    public class PlayerDataProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
          
            dstManager.AddComponentData(
                entity, new PlayerData{
                    moveSpeed = 5f   //测试需要，实际项目中不应该在此处设置具体的值,应在游戏创建初使用Set方法来做
                });
            dstManager.AddComponentData(entity, new PlayerInput { });

        }
    }
}
