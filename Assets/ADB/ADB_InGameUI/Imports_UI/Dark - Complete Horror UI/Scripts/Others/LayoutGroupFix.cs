using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.Dark
{
    public class LayoutGroupFix : MonoBehaviour
    {
        [Header("Settings")]
        public bool fixAtStart = true;
        public bool fixParent = false;

        void Start()
        {
            if (fixAtStart == true && fixParent == false)
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            else if (fixAtStart == true && fixParent == true)
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        }

        public void FixLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}