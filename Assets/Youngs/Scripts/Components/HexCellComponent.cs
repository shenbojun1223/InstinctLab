using UnityEngine;
using Unity.Entities;
using System;

//@Youngs 2019年4月24日16:39:21
//@Youngs 2019年4月29日10:30:21

namespace ILab.Youngs
{

    //地图块的数据内容
    [Serializable]
    public struct HexCellData : IComponentData
    {
        public int cellIndex;
        public int existTree;
    }

    // 这个组件表明了这个块是元组块
    // 为什么要这样做呢? 因为HexMapGenerateSystem里的Execute正如我所提到的那样
    // 当存在着吻合的组件时,它就会执行
    [Serializable]
    public struct HexCellPrototype : IComponentData
    {
        public int mapWidth;
        public int mapHeight;
        public Entity entity;
        public Entity wood;
    }

    [Serializable]
    public struct TopographyProcessRequire : IComponentData //
    {
        public Entity entity;
        public Entity wood;
    }

    [Serializable]
    public struct TopographyProcessed : IComponentData //这两个组件都可以考虑做成shared
    {
       
    }

   

}

