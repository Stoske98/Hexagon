using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBox : MonoBehaviour
{
    public int maxMessage = 50;
    public GameObject textObject, chatContent, friendRequsetSendForm,friendRequestAcceptForm;
    public TMP_InputField input;
    [SerializeField]
    List<Message> messageList = new List<Message>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && input.text != "")
        {
            ClientJobSystem.ChatMessageRequest(GameManager.Instance.user_nickame, input.text.ToString(),GameManager.Instance.user_specialID);
            /*SendMessageToMainChat(input.text.ToString());*/
            input.text = "";
        }
    }
    
    public void SendMessageToMainChat(string text, string nickname ,string userID)
    {
        if (messageList.Count >= maxMessage)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
            
        Message newMessage = new Message();
        newMessage.text = "[" + nickname + "]" + " : " + text;

        GameObject newText = Instantiate(textObject, chatContent.transform);

        newMessage.textObject = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObject.text = newMessage.text;

        Button clickOnText = newText.GetComponent<Button>();
        clickOnText.onClick.AddListener(() => OnChatTextClick(newMessage.textObject));

        newMessage.nickname = nickname;
        newMessage.userID = userID;
        messageList.Add(newMessage);

    }
    public void OnChatTextClick(TextMeshProUGUI text)
    {
        foreach (var message in messageList)
        {
            if(message.textObject == text)
            {
                if(message.userID != GameManager.Instance.user_specialID)
                {
                    FirendRequstBTN fr = Instantiate(friendRequsetSendForm, text.transform).GetComponent<FirendRequstBTN>();
                    fr.friendID = message.userID;
                    fr.setNicknameText(message.nickname);
                }
                break;  
            }
        }
    }


    [System.Serializable]
    public class Message
    {
        public string text;
        public TextMeshProUGUI textObject;
        public string nickname;
        public string userID;
    }

}
