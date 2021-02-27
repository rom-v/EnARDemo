using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AppManager : Singleton<AppManager>
    {
        void Start()
        {
            UIManager.instance.ShowLoadingDB();
            DBManager.instance.LoadDB(DBLoadComplete);
        }

        public void DBLoadComplete()
        {
            UIManager.instance.HideLoadingDB();
            UIManager.instance.GenerateStartUI();
            UIManager.instance.SwitchToNormalMode();
        }

        public void SwitchToARMode(int productIndex)
        {
            var textureUrl = DBManager.instance.GetProducts()[productIndex].texture;

            LoadingManager.instance.LoadARImage(textureUrl, ARManager.instance.ChangePlaneMaterialTexture);

            ARManager.instance.TurnARModeOn();
            UIManager.instance.SwitchToARMode();
        }

        public void SwitchToNormalMode()
        {
            ARManager.instance.TurnARModeOff();
            UIManager.instance.SwitchToNormalMode();
        }
    }
}
