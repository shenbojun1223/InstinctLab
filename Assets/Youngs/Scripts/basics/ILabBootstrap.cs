using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

//@ILab.Youngs 2019年4月20日17:23:51
namespace ILab.Youngs {

    public sealed class ILabBootstrap
    {
        public static ILabSettings Settings;

        public static void NewGame()
        {
            Debug.Log("游戏运行");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void InitializeWithScene()
        {
            var settingsGo = GameObject.Find("Settings");
            Settings = settingsGo?.GetComponent<ILabSettings>();
            Assert.IsNotNull(Settings);     
        }
    }

}
