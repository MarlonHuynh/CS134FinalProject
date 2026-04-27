using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopManager : MonoBehaviour
{
    [Header("Refs")]
    public ChatManager chatManager; 
    
    public GameObject ChatExclaimationNotif; 
    [Header("Panels")]
    public GameObject desktopPanel;
    public GameObject workPanel;
    public GameObject chatPanel;
    public GameObject trashPanel;
    public GameObject shopPanel;

    [Header("Points")]
    public TextMeshProUGUI desktopPointsText;
    public TextMeshProUGUI shopPointsText; 
    public TextMeshProUGUI workPointsText;

    public int captchaPoints = 0;
    public bool seenChatForTheDay = false; 

    void Start()
    {
        seenChatForTheDay = false; 
        ShowDesktop();
    }

    void SwitchTo(GameObject panel)
    {
        panel.SetActive(true);
        SetRaycast(panel, true);
    }

    public void ShowDesktop()
    {
        workPanel.SetActive(false);
        chatPanel.SetActive(false);
        trashPanel.SetActive(false);
        shopPanel.SetActive(false);
    }
    void SetRaycast(GameObject panel, bool enabled)
    {
        Image img = panel.GetComponent<Image>();
        if (img != null) img.raycastTarget = enabled;
    }

    public void OpenWork() { SwitchTo(workPanel); }
    public void OpenChat() {  
        SwitchTo(chatPanel); 
        seenChatForTheDay = true; 
        turnOffNotifIfSeenChat(); 
    }
    public void OpenTrash() { SwitchTo(trashPanel); }
    public void OpenShop() { SwitchTo(shopPanel); }


    public void AddPoints(int amount)
    {
        captchaPoints += amount;
        desktopPointsText.text = "Points: " + captchaPoints;
        shopPointsText.text = "Points: " + captchaPoints;
        if (workPointsText != null) workPointsText.text = "Points: " + captchaPoints;
    }

    public void subtractPoints(int amount)
    {
        captchaPoints -= amount;
        desktopPointsText.text = "Points: " + captchaPoints;
        shopPointsText.text = "Points: " + captchaPoints;
        if (workPointsText != null) workPointsText.text = "Points: " + captchaPoints;
    }

    public void CorruptPoints()
    {
        string corrupted = "9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999";
        captchaPoints = int.MaxValue; // max int internally
        desktopPointsText.text = "Points: " + corrupted;
        shopPointsText.text = "Points: " + corrupted;
        if (workPointsText != null) workPointsText.text = "Points: " + corrupted;
    }

    public void resetDesktopNotification()
    {
        ChatExclaimationNotif.SetActive(true); 
        seenChatForTheDay = false; 
    }
    public void turnOffNotifIfSeenChat()
    {
        if (seenChatForTheDay == true)
        { 
            ChatExclaimationNotif.SetActive(false); 
        } 
    }
    public void disableDesktopNotification()
    {
        ChatExclaimationNotif.SetActive(false); 
    }
    
    public void RestorePoints(int points)
    {
        captchaPoints = points;
        desktopPointsText.text = "Points: " + captchaPoints;
        shopPointsText.text = "Points: " + captchaPoints;
        if (workPointsText != null) workPointsText.text = "Points: " + captchaPoints;
    }
}