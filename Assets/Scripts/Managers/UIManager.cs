using Patterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] GameObject loadingDB;
        [SerializeField] GameObject normaModelUI;
        [SerializeField] GameObject arModelUI;
        [SerializeField] GameObject arModelScanMsg;

        [SerializeField] Transform productsContainer;

        void Start()
        {

        }

        public void GenerateStartUI()
        {
            GenerateProductList();
        }

        void GenerateProductList()
        {
            Product[] products = DBManager.instance.GetProducts();

            GameObject prefab = productsContainer.GetChild(0).gameObject;
            prefab.SetActive(true);

            for(int i=0; i< products.Length; i++)
            {
                Transform btnProduct = Instantiate(prefab).transform;
                btnProduct.SetParent(productsContainer);
                btnProduct.localScale = Vector3.one;

                btnProduct.GetChild(1).GetComponent<TextMeshProUGUI>().text = products[i].title;

                btnProduct.GetComponent<BtnSelectProduct>().Init(i, AppManager.instance.SwitchToARMode);

                LoadingManager.instance.AddRequestedImage(btnProduct.GetChild(0).GetComponent<Image>(), products[i].imageLink);
            }

            prefab.SetActive(false);
        }

        public void SwitchToARMode()
        {
            normaModelUI.SetActive(false);
            arModelUI.SetActive(true);

            ShowScanPlaneMsg();
        }

        public void SwitchToNormalMode()
        {
            normaModelUI.SetActive(true);
            arModelUI.SetActive(false);
        }

        public void ShowScanPlaneMsg()
        {
            arModelScanMsg.SetActive(true);
        }
        public void HideScanPlaneMsg()
        {
            arModelScanMsg.SetActive(false);
        }

        public void ShowLoadingDB()
        {
            loadingDB.SetActive(true);
        }
        public void HideLoadingDB()
        {
            loadingDB.SetActive(false);
        }
    }
}
