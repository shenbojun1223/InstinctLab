using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


//@ILab.Youngs 2019年4月24日19:14:53

namespace ILab.Youngs
{
    [RequiresEntityConversion]
    public class HexCellProxy : MonoBehaviour, IDeclareReferencedPrefabs,IConvertGameObjectToEntity
    {
        public GameObject m_prefab;
        public GameObject m_woodPrefab;
        public int m_mapWidth;
        public int m_mapHeight;
       
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(m_prefab);
            referencedPrefabs.Add(m_woodPrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity, new HexCellData {
                  
                });
            dstManager.AddComponentData(
               entity, new HexCellPrototype
               {
                   entity = conversionSystem.GetPrimaryEntity(m_prefab),
                   wood = conversionSystem.GetPrimaryEntity(m_woodPrefab),
                   mapWidth = m_mapWidth,
                   mapHeight = m_mapHeight
               });
          
        }
      
    }
}
