using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Managers
{
    public class ARManager : Singleton<ARManager>
    {
        [SerializeField] private Camera arCamera;
        [SerializeField] private Material planeMaterial;

        [SerializeField] private GameObject[] arModeObjects;
        [SerializeField] private GameObject[] normalModeObjects;

        bool planeScanning;

        void Start()
        {
            planeScanning = false;
        }

        private void Update()
        {
            CheckPlaneScan();
        }

        private void CheckPlaneScan()
        {
            if (planeScanning)
            {
                RaycastHit hit;
                if (Physics.Raycast(arCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out hit))
                {
                    if (hit.collider.GetComponent<ARPlane>() != null)
                    {
                        float size = hit.collider.GetComponent<Renderer>().bounds.size.magnitude;
                        if (size > 1)
                        {
                            planeScanning = false;
                            UIManager.instance.HideScanPlaneMsg();
                        }
                    }
                }
            }
        }

        public void TurnARModeOn()
        {
            for (int i = 0; i < arModeObjects.Length; i++)
                arModeObjects[i].SetActive(true);

            for (int i = 0; i < normalModeObjects.Length; i++)
                normalModeObjects[i].SetActive(false);

            planeScanning = true;
        }

        public void TurnARModeOff()
        {
            for (int i = 0; i < arModeObjects.Length; i++)
                arModeObjects[i].SetActive(false);

            for (int i = 0; i < normalModeObjects.Length; i++)
                normalModeObjects[i].SetActive(true);

            planeScanning = false;

            planeMaterial.mainTexture = null;
        }

        public void ChangePlaneMaterialTexture(Texture2D texture)
        {
            planeMaterial.mainTexture = texture;
        }
    }
}
