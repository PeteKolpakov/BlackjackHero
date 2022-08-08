using UnityEngine;
using UnityEngine.UI;

namespace BlackJackHero
{
    [ExecuteInEditMode()]
    public class ProgressBar : MonoBehaviour
    {
        public int Maximum;
        public int Current;
        public Image Mask;

        private void Update()
        {
            GetCurrentFill();
        }

        private void GetCurrentFill()
        {
            float fillamount = (float)Current / (float)Maximum;
            Mask.fillAmount = fillamount;
        }
    }
}
