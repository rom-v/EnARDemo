using Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Managers
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        Queue<RequestedImage> imageQueue;
        bool isLoading;
        private void Awake()
        {
            imageQueue = new Queue<RequestedImage>();
            isLoading = false;
        }

        public void AddRequestedImage(Image image, string url)
        {
            imageQueue.Enqueue(new RequestedImage {image=image, url = url});

            if (!isLoading)
                ProcessQueue();
        }

        void ProcessQueue()
        {
            if (imageQueue.Count > 0)
            {
                isLoading = true;
                var loadedImage = imageQueue.Dequeue();
                StartCoroutine(LoadBtnImageCoroutine(loadedImage));
            }
            else
                isLoading = false;
        }

        IEnumerator LoadBtnImageCoroutine(RequestedImage loadedImage)
        {

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(loadedImage.url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("ERROR: "+uwr.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    loadedImage.image.GetComponent<Image>().sprite = sprite;

                    ProcessQueue();
                }
            }
        }

        public void LoadARImage(string url, Action<Texture2D> onComplete)
        {
            StartCoroutine(LoadARImageCoroutine(url, onComplete));
        }

        IEnumerator LoadARImageCoroutine(string url, Action<Texture2D> onComplete)
        {

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log("ERROR: " + uwr.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    onComplete?.Invoke(texture);
                }
            }
        }
    }
}

public class RequestedImage
{
    public Image image;
    public string url;
}
