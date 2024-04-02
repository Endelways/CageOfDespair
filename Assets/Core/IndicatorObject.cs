using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class IndicatorObject : MonoBehaviour
    {
        public Image image;
        public Text textField;
        public bool Enabled
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

    }
}