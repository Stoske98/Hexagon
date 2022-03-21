using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.Shift
{
    public class QuickMatchButton : MonoBehaviour
    {
        [Header("TEXTS")]
        public bool useCustomText = false;
        public string buttonTitle = "My Title";

        [Header("IMAGES")]
        public bool useCustomImage = false;
        public Sprite backgroundImage;

        TextMeshProUGUI titleText;
        Image image1;
        Animator anim;
        public bool start;

        void Start()
        {
            if (useCustomText == false)
            {
                titleText = gameObject.transform.Find("Content/Title").GetComponent<TextMeshProUGUI>();
                titleText.text = buttonTitle;
            }

            if (useCustomImage == false)
            {
                image1 = gameObject.transform.Find("Content/Background").GetComponent<Image>();
                image1.sprite = backgroundImage;
            }

            anim = gameObject.GetComponent<Animator>();
        }


        public void StartMatchMaking()
        {
            start = !start;
            if(start)
            {
                anim.Play("Highlighted");
                titleText.text = "FINDING MATCH";
                Matchmaking.Instance.CreateTicket(GameManager.Instance.selected_class);
            }
            else
            {
                //anim.Play("Normal");
                //titleText.text = "FIND MATCH";
                Matchmaking.Instance.CancelMatchFinding();
                Manager.UIManager.Instance.match_finding_btn.interactable = false;
            }
        }
    }

}