using UnityEngine;

public class ParallaxUI : MonoBehaviour
{
    Vector2 startPos;

    [SerializeField] float moveModifier = 10f; // keep  small, like 5-15 for UI

    void Start()
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f);

        float targetX = startPos.x + (pz.x * moveModifier);
        float targetY = startPos.y + (pz.y * moveModifier);

        Vector2 target = new Vector2(targetX, targetY);
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(
            GetComponent<RectTransform>().anchoredPosition, 
            target, 
            2f * Time.deltaTime
        );
    }
}