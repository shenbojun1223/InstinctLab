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
        public int m_mapWidth;
        public int m_mapHeight;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(m_prefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {

            dstManager.AddComponentData(
                entity, new HexCellData {
                    entity    = conversionSystem.GetPrimaryEntity(m_prefab),
                    mapWidth  = m_mapWidth,
                    mapHeight = m_mapHeight

                });

        }
      
    }
}
