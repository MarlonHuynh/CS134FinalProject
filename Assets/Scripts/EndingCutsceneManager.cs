using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using System.Collections;
using System.Reflection;

public class EndingCutsceneManager : MonoBehaviour
{
    public GameObject endingBG; 
    public Image endingBGImage;
    public GameObject bodyTextObj; 
    public GameObject headingObj;  
    public TMP_Text bodyText; 
    public TMP_Text headingText; 
    public AudioClip openDoorClip; 
    public AudioSource flex2DAudioSource; 
    public AudioSource flex2DAudioSource_looping; 
    public AudioSource mainRoomMusicSource; 
    public PlayerMovement playerMovement; 

    void Start()
    {
        Color color = endingBGImage.color;
        color.a = 0;
        endingBGImage.color = color;
        bodyTextObj.SetActive(false); 
        headingObj.SetActive(false); 

    }

    public void playEnding(int num)
    {
        endingBG.SetActive(true); 
        StartCoroutine(endingEnum(num)); 
    }

    IEnumerator endingEnum(int endingNum)
    {
        playerMovement.disableMovement(); 
        endingBG.SetActive(true); 

        float time = 0f;
        Color color = endingBGImage.color;

        while (time < 3f)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / 3f);

            color.a = alpha;
            endingBGImage.color = color;

            yield return null;
        }
 
        // Audio 
        flex2DAudioSource_looping.Stop(); 
        mainRoomMusicSource.Stop(); 
        flex2DAudioSource.clip = openDoorClip; 
        flex2DAudioSource.Play(); 
        
        yield return new WaitForSeconds(3f);

        if (endingNum == 1)
        {
            bodyTextObj.SetActive(true); 
            headingObj.SetActive(true); 
            bodyText.text = "They seized you instantly. Your captors—your so-called protectors—covered your eyes, plunging you into darkness. The air filled with the whir of machinery, and a thick sharp metallic scent burned your nose. Before you could react, they threw you forward. The machine roared, and in seconds, your body was reduced to nothing but pulp, ready to be consumed. You paid the ultimate price for refusing to play their game."; 
            headingText.text = "Ending 1: Meatgrinder"; 
        }
    }
}
