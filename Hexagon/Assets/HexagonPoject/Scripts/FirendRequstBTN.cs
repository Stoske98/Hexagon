using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirendRequstBTN : MonoBehaviour
{
    public TextMeshProUGUI nickname_text;
    [HideInInspector]
    public string friendID;

    public void setNicknameText(string text)
    {
        nickname_text.text = text;
    }
    public void SendRequset()
    {
        ClientJobSystem.FriendRequst(GameManager.Instance.user_nickame,GameManager.Instance.user_specialID, friendID);
        Destroy(gameObject);
    }
    public void ExitRequset()
    {
        Destroy(gameObject);
    }

    public void AcceptRequst()
    {
        Debug.Log("My nickname " + GameManager.Instance.user_nickame + " id: " + GameManager.Instance.user_specialID);
        Debug.Log("My friend nickname " + nickname_text.text + " id: " + friendID);
        ClientJobSystem.AcceptFriendRequest(GameManager.Instance.user_specialID,friendID);
        Destroy(gameObject);
    }
    public void DeclineRequest()
    {
        Destroy(gameObject);
    }
}
