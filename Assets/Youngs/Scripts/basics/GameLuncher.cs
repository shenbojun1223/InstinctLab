using UnityEngine;

//@ILab.Youngs 2019年4月19日20:51:44
namespace ILab.Youngs {

    public class GameLuncher : MonoBehaviour
    {
        public static GameLuncher instance;

        private void Awake()
        {
            if (instance == null){

                instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
                ILabBootstrap.NewGame();
        }
    }
}

