using UnityEngine;
using UnityEngine.Assertions;

//@ILab.Youngs 2019年4月19日20:26:20
namespace ILab.Youngs
{
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

    //后期需要重写GUI Layout
    public class ILabSettings : MonoBehaviour
    {
        public int forestCoverRate=2;
      
    }

}