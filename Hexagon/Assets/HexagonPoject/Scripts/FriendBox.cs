using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FriendBox : MonoBehaviour
{
    public GameObject friendObject, content;

    LinkedList<Friend> friends = new LinkedList<Friend>();

    public void AddInList(string nickname, string userid, int isOnline)
    {
        
        Friend friend = new Friend
        {
            nickname = nickname,
            userID = userid,
            isOnline = isOnline
        };

        foreach (Friend f in friends)
        {
            if (f.userID == friend.userID)
            {
                if(f.isOnline != friend.isOnline)
                    f.isOnline = friend.isOnline;
                FillContent();
                return;
            }
        }
        if (friend.isOnline == 1)
            friends.AddFirst(friend);
        else
            friends.AddLast(friend);

        FillContent();
    }

    private void FillContent()
    {
        foreach (var friend in friends)
        {
            friend.friendObject = null;
        }
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Friend friend in friends)
        {
            GameObject friendGO = Instantiate(friendObject, content.transform);
            Button btn = friendGO.GetComponentInChildren<Button>();
            TextMeshProUGUI[] texts = friendGO.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = friend.nickname;
            btn.onClick.AddListener(() => SendMatchRequest(btn));
            if (friend.isOnline == 1)
            {
                texts[1].text = "Online";
                texts[1].color = Color.green;
            }
            else
            {
                texts[1].text = "Offline";
                texts[1].color = Color.grey;
                btn.gameObject.SetActive(false);
            }
          
            friend.friendObject = friendGO;
            
        }
    }
    public void SendMatchRequest(Button btn)
    {
        btn.interactable = false;
        Debug.Log("Send match request");
    }
    public class Friend
    {
        public string nickname;
        public string userID;
        public int isOnline;
        public GameObject friendObject;
    }
}
