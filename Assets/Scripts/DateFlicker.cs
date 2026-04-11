using UnityEngine;
using TMPro;
using System.Text;

public class DateFlicker : MonoBehaviour
{
    public int date; // Ingame date
    public TMP_Text text;
    public int randomCharCount = 3;
    public float updateSpeed = 0.05f; // how fast chars change

    private float timer;

    void Start()
    {
        date = 1;   
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime; // works even if paused

        if (timer >= updateSpeed)
        {
            timer = 0f;
            UpdateText();
        }
    }

    void UpdateText()
    {
        string randomPart = GenerateRandomChars(randomCharCount);
        int dayNumber = System.DateTime.Now.Day;

        text.text = "DAY " + randomPart + date;
    }

    string GenerateRandomChars(int length)
    {
        const string chars = "0123456789";
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[Random.Range(0, chars.Length)]);
        }

        return sb.ToString();
    }
}