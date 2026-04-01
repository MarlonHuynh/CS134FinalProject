using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesktopManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject desktopPanel;
    public GameObject workPanel;
    public GameObject chatPanel;
    public GameObject trashPanel;
    public GameObject shopPanel;

    [Header("Points")]
    public TextMeshProUGUI pointsText;
    private int captchaPoints = 0;

    void Start()
    {
        ShowDesktop();
    }

    public void ShowDesktop()
    {
        workPanel.SetActive(false);
        chatPanel.SetActive(false);
        trashPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void OpenWork() { SwitchTo(workPanel); }
    public void OpenChat() { SwitchTo(chatPanel); }
    public void OpenTrash() { SwitchTo(trashPanel); }
    public void OpenShop() { SwitchTo(shopPanel); }

    void SwitchTo(GameObject panel)
    {

        panel.SetActive(true);
    }

    public void AddPoints(int amount)
    {
        captchaPoints += amount;
        pointsText.text = "Points: " + captchaPoints;
    }
}