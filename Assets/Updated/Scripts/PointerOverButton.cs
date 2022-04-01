using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MapViewer
{
    public class PointerOverButton : MonoBehaviour
    {
        public Image buttonimage;
        public Color onoverColor = Color.yellow;
        public Color onNormalColor = Color.red;
        string ButtonName;
        private void Start()
        {
            buttonimage = GetComponent<Image>();
            ButtonName = this.gameObject.name;
        }
        public void OnOverButton()
        {
            GameManager.Instance.OnOverPoint(ButtonName, onoverColor);
        }

        public void OnExitButton()
        {
            GameManager.Instance.OnOverPoint(ButtonName, onNormalColor);

        }
    }
}
