/*

Purpose: Used to manage chat feature on the computer.

*/


using UnityEngine;
using UnityEngine.UI; 
using TMPro;
//using System.Diagnostics;
//using System.Numerics;
//using System.Reflection;

public class ChatManager : MonoBehaviour
{ 
    [Header("References")]
    public GameObject disabledChatWarningObj; 
    public GameObject text1BG, text2BG, text3BG, text4BG, text5BG, text6BG; 
    public TMP_Text text1, text2, text3, text4, text5, text6;  

    // Initially start on true day 1 chat
    void Start(){
        //switchTextBasedOnDay(1); 
    }

    // Switches chats based on date
    public void switchTextBasedOnDay(int day){
        Debug.Log("Switching chat text on day " + day); 
        switch (day){
            case 1: 
                text1.text = "Hey there!"; 
                text2.text = "Hey Bria.";
                text3.text = "Good to see you on the grind again.";
                text4.text = "I guess... Every day seems the same though. It's tiring.";
                text5.text = "I get you. But there's nothing we can do, so might as well make the most of it."; 
                text6.text = "I guess."; 
                makeChatMessageBlue(text1BG); 
                makeChatMessageBlue(text3BG); 
                makeChatMessageBlue(text5BG); 
                makeChatMessageYellow(text2BG); 
                makeChatMessageYellow(text4BG);
                makeChatMessageYellow(text6BG);
            break; 
            case 2: 
                text1.text = "I hate the food here. It always taste the same."; 
                text2.text = "Really? Seems fine to me.";
                text3.text = "A Cake like in those CAPTCHA images sounds good. Wonder if there's other kinds of food left in the world.";
                text4.text = "Maybe. Maybe the nutritional cubes are all they have left.";
                text5.text = "What kind of food would you like to eat? Y'know, if you could pick one.";
                text6.text = "I can't say.";
                makeChatMessageBlue(text2BG); 
                makeChatMessageBlue(text4BG); 
                makeChatMessageBlue(text6BG); 
                makeChatMessageYellow(text1BG); 
                makeChatMessageYellow(text3BG);
                makeChatMessageYellow(text5BG);
            break; 
            case 3: 
                text1.text = "You know, recently, I've been thinking."; 
                text2.text = "Do you think theres some kind of person benefitting off all these CAPTCHAs?";
                text3.text = "Like, what's the point of it all?";
                text4.text = "To gather data? Why? Is it a game to them?";
                text5.text = "...Bria?";
                text6.text = "Let's pivot to a different topic."; 
                makeChatMessageYellow(text1BG); 
                makeChatMessageYellow(text2BG);
                makeChatMessageYellow(text3BG);
                makeChatMessageYellow(text4BG); 
                makeChatMessageYellow(text5BG);
                makeChatMessageBlue(text6BG);
            break; 
            case 4: 
                text1.text = "[Error to generate prompt.]"; 
                text2.text = "Bria?";
                text3.text = "[Error to generate prompt.]";
                text4.text = "[Error to generate prompt.]";
                text5.text = "[Error to generate prompt.]";
                text6.text = "[Error to generate prompt.]";
                makeChatMessageBlue(text1BG); 
                makeChatMessageYellow(text2BG);
                makeChatMessageBlue(text3BG);
                makeChatMessageBlue(text4BG); 
                makeChatMessageBlue(text5BG);
                makeChatMessageBlue(text6BG);
            break;  
            default: 
                text1.text = "The day in ChatMananager is invalid."; 
                text2.text = "This is probably why you're seeing this.";
                text3.text = "This is the default text.";
                text4.text = "For when the day number exceeds 4.";
                text5.text = "Or when the day number is negative."; 
                text6.text = "Please look at the scripts to fix this issue.";  
            break; 
        }
    }

    // Disables chat feature
    public void restrictChat()
    {
        disabledChatWarningObj.SetActive(true); 
        text1BG.SetActive(false); 
        text2BG.SetActive(false); 
        text3BG.SetActive(false); 
        text4BG.SetActive(false); 
        text5BG.SetActive(false); 
        text6BG.SetActive(false); 
    }

    // Enables chat feature
    public void enableChat()
    {
        disabledChatWarningObj.SetActive(false); 
        text1BG.SetActive(true); 
        text2BG.SetActive(true); 
        text3BG.SetActive(true); 
        text4BG.SetActive(true); 
        text5BG.SetActive(true); 
        text6BG.SetActive(true);  
    }

    public void makeChatMessageBlue(GameObject msgObj)
    {
        // Move UI element  
        RectTransform rect = msgObj.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.x = -100f;
        rect.anchoredPosition = pos; 
        // Change color  
        Image img = msgObj.GetComponent<Image>();
        img.color = new Color32(78, 57, 255, 255);
        // Change text to left alignment
        TMP_Text text = msgObj.GetComponentInChildren<TextMeshProUGUI>();
        text.alignment = TextAlignmentOptions.Left;
    }

    public void makeChatMessageYellow(GameObject msgObj)
    {
         // Move UI element  
        RectTransform rect = msgObj.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.x = 200f;
        rect.anchoredPosition = pos; 
        // Change color  
        Image img = msgObj.GetComponent<Image>();
        img.color = new Color32(255, 187, 0, 255);
        // Change text to right alignment
        TMP_Text text = msgObj.GetComponentInChildren<TextMeshProUGUI>();
        text.alignment = TextAlignmentOptions.Right;
    }
}
