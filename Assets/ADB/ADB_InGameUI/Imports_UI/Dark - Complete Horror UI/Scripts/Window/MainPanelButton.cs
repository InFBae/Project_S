using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.Dark
{
    public class MainPanelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator buttonAnimator;

        void Start()
        {
            buttonAnimator = this.GetComponent<Animator>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed"))
                buttonAnimator.Play("Hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed"))
                buttonAnimator.Play("Hover to Normal");
        }
    }
}