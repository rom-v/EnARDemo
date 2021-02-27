using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace UI
{
    public class BtnSelectProduct : MonoBehaviour
    {
        [SerializeField] private int productDBIndex;
        [SerializeField] private UnityAction<int> onClick;

        public void Init(int productDBIndex, UnityAction<int> onClick)
        {
            this.productDBIndex = productDBIndex;
            this.onClick = onClick;
        }

        public void OnClick()
        {
            onClick.Invoke(productDBIndex);
        }
    }
}
