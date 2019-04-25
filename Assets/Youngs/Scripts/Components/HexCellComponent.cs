using UnityEngine;
using Unity.Entities;
using System;

//@Youngs 2019年4月24日16:39:21

namespace ILab.Youngs
{

    [Serializable]
    public struct HexCellData : IComponentData
    {
        public int mapWidth;
        public int mapHeight;
        public Entity entity;
    }
    
}

