using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Managers
{
    public class DBManager : Singleton<DBManager>
    {
        public bool workOffline;
        public TextAsset csvDB;

        private Product[] products;

        public void LoadDB(UnityAction onComplete)
        {
            if (workOffline)
            {
                CreateDB(csvDB.text);
                onComplete?.Invoke();
            }
            else
                StartCoroutine(LoadCSV(onComplete));
        }

        IEnumerator LoadCSV(UnityAction onComplete)
        {
            string url = "https://enita.com.ua/feeds/feedmerchant_new.php";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    string[] pages = url.Split('/');
                    int page = pages.Length - 1;
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    CreateDB(webRequest.downloadHandler.text);
                    onComplete?.Invoke();
                }
            }
        }

        public Product[] GetProducts()
        {
            return products;
        }

        void CreateDB(string text)
        {
            products = CSVReader.CreateProductsArray(text);
            Debug.Log(products.Length);

        }
    }
}
