using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        #region UIManager Singleton
        private static UIManager _instance;

        public static UIManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

        }
        #endregion

        [Header("Canvas")]
        public Canvas canvas;

        [Header("Login")]
        public GameObject login_panel;
        public TMP_InputField email_login;
        public TMP_InputField password_login;

        [Header("Sing Up")]
        public GameObject singUp_panel;
        public TMP_InputField email_singUp;
        public TMP_InputField nickname_singUp;
        public TMP_InputField password_singUp;

        [Header("Sync Data")]
        public GameObject sincdata_panel;
        [Header("Main Menu")]
        public GameObject main_menu;
        public Animator main_anim;

        [Header("Chat Box")]
        public ChatBox chatbox;

        [Header("Friend Box")]
        public FriendBox friendBox;
        public Animator friendsAnim;
        public GameObject XButton;
        public Button friendsButton;

        [Header("Match Accept")]
        public GameObject match;
        public TextMeshProUGUI enemy_nickname;
        public TextMeshProUGUI enemy_rank;
        public GameObject accept_decline;
        public GameObject match_waiting;
        public GameObject match_has_declined;
        public GameObject match_has_accepted;

        [Header("Match Button")]
        public TextMeshProUGUI match_finding_text;
        public Button match_finding_btn;
        public Animator match_finding_anim;
        public Michsky.UI.Shift.QuickMatchButton matchButton;
        [HideInInspector]
        public bool startFindingMatch;

        [Header("Player")]
        public TextMeshProUGUI player_name;
        public TextMeshProUGUI player_rank;
        public TextMeshProUGUI rank_name;


        public void OnLogin()
        {
            // send on server email and password and check in data base is there that account
            if (email_login.text != "" && password_login.text != "")
            {
                ClientJobSystem.LoginRequest(email_login.text.ToString(), password_login.text.ToString());
                sincdata_panel.SetActive(true);
            }
            else
                Debug.Log("Nisu sva polja popunjena");
        }

        public void OnCreateNewAccount()
        {
            // open new page for creating acc
            login_panel.SetActive(false);
            singUp_panel.SetActive(true);
        }

        public void OnCancelCreateNewAccount()
        {
            login_panel.SetActive(true);
            singUp_panel.SetActive(false);
        }

        public void OnSingUp()
        {
            if (email_singUp.text != "" && nickname_singUp.text != "" && password_singUp.text != "")
            {
                ClientJobSystem.CreateAccountRequest(email_singUp.text.ToString(), nickname_singUp.text.ToString(), password_singUp.text.ToString());
                sincdata_panel.SetActive(true);
            }

            else
                Debug.Log("Nisu sva polja popunjena");
        }

        public void ShowFriendRequest(string nickname, string friendID)
        {
            FirendRequstBTN frbtn = Instantiate(chatbox.friendRequestAcceptForm, canvas.transform).GetComponent<FirendRequstBTN>();
            frbtn.friendID = friendID;
            frbtn.setNicknameText(nickname);
        }

        public void CancelMatchFinding()
        {
            match_finding_anim.Play("Normal");
            match_finding_text.text = "FIND MATCH";
            match_finding_btn.interactable = true;
        }

        public void SelectTeam(int team)
        {
            // anim
            if (team == 0)
                main_anim.SetBool("Light",true);
            else
                main_anim.SetBool("Dark", true);
            GameManager.Instance.selected_class = team;
        }

        public void BackToTheMiddleOfTheMainMenu()
        {
            // prekinuti trazenje game
            if (GameManager.Instance.selected_class == 0)
                main_anim.SetBool("Light", false);
            else
                main_anim.SetBool("Dark", false);

            GameManager.Instance.selected_class = -1;
        }

        public void ShowFriends()
        {
            friendsAnim.SetBool("ShowFriendList", true);
            XButton.SetActive(true);
            friendsButton.interactable = false;
        }

        public void HideFriends()
        {
            friendsAnim.SetBool("ShowFriendList", false);
            XButton.SetActive(false);
            friendsButton.interactable = true;
        }

        public void SetPlayerInfo(string name, int rank)
        {
            player_name.text = name;
            player_rank.text = rank.ToString();
        }

        public void StartFindingMatch()
        {
            if(!startFindingMatch)
            {
                Matchmaking.Instance.CreateTicket(GameManager.Instance.selected_class);
                startFindingMatch = true;
            }
        }

        public void StopFindingMatch()
        {
            if(startFindingMatch)
            {
                Matchmaking.Instance.CancelMatchFinding();
                match_finding_btn.interactable = false;
                startFindingMatch = false;
            }
        }

    }
}