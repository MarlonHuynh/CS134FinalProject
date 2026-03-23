using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector2 startPos;

    [SerializeField] int moveModifier;
    [SerializeField] float clampRange = 0.5f;
    
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f);

        float targetX = startPos.x + (pz.x * moveModifier);
        float targetY = startPos.y + (pz.y * moveModifier);

        // clamp so sprites don't drift too far from origin
        targetX = Mathf.Clamp(targetX, startPos.x - clampRange, startPos.x + clampRange);
        targetY = Mathf.Clamp(targetY, startPos.y - clampRange, startPos.y + clampRange);

        float posX = Mathf.Lerp(transform.position.x, targetX, 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, targetY, 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}