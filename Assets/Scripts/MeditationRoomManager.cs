using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using System.Reflection;

public class MeditationRoomManager : MonoBehaviour
{ 
    [Header("Refs")]
    public OpenMeditationRoom openMeditationRoom; 
    public Material floorMat; 
    public Material wallMat; 
    public Material roofMat;
    public AudioSource meditationMusicSource; 
    public Sprite beachFloor, beachWall, beachRoof; 
    public AudioClip beachMusic; 
    public Sprite grassyFloor, grassyWall, grassyRoof; 
    public AudioClip grassyMusic; 
    [Header("Cutscene Refs")]
    public InteractionManager interactionManager; 
    public GameObject meditationRoomCutsceneObject; 
    public Image meditationRoomCutsceneImage; 
    public float fadeDuration = 3f;
    public float stayDuration; 
    public AudioClip staticClip;  


    void Start()
    {
        switchToBeach();  
        meditationRoomCutsceneObject.SetActive(false); 
    }
    
    void Update(){
        /*
        if (Input.GetKeyUp(KeyCode.O)){
            switchToBeach(); 
        }
        if (Input.GetKeyUp(KeyCode.P)){
            switchToGrassyField(); 
        }*/ 
    }  

    public void playMeditationRoomCutscene()
    {
        StartCoroutine(CutsceneIEnumerator());  
    }
    private IEnumerator CutsceneIEnumerator()
    {
        meditationRoomCutsceneObject.SetActive(true);   
        yield return Fade(0f, 1f); // Fade in
        yield return Fade(1f, 1f); // Stay on image
        yield return Fade(1f, 0f); // Fade out
        meditationRoomCutsceneObject.SetActive(false); 
        yield return interactionManager.HintCoroutine("You feel refreshed...", 3f); 
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {  
        float time = 0f;
        Color color = meditationRoomCutsceneImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);

            color.a = alpha;
            meditationRoomCutsceneImage.color = color;

            yield return null;
        }

        // Ensure final value is exact
        color.a = endAlpha;
        meditationRoomCutsceneImage.color = color; 
    }

    void switchToBeach(){
        floorMat.SetTexture("_BaseMap", beachFloor.texture);
        wallMat.SetTexture("_BaseMap", beachWall.texture);
        roofMat.SetTexture("_BaseMap", beachRoof.texture);
        meditationMusicSource.clip = beachMusic; 
        meditationMusicSource.Play(); 
    }

    void switchToGrassyField(){
        floorMat.SetTexture("_BaseMap", grassyFloor.texture);
        wallMat.SetTexture("_BaseMap", grassyWall.texture);
        roofMat.SetTexture("_BaseMap", grassyRoof.texture);
        meditationMusicSource.clip = grassyMusic; 
        meditationMusicSource.Play(); 
    } 
}
