using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float Amount = 0.002f;

    [Range(1f, 30f)]
    public float Frequency = 10.0f;

    [Range(10f, 100f)]
    public float Smooth = 20.0f;

    private Vector3 _startPos;
    private bool _isMoving;

    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        CheckForHeadBobTrigger();
    }

    private void CheckForHeadBobTrigger()
    {
        float inputMagnitude = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).magnitude;

        if (inputMagnitude > 0)
            StartHeadBob();
        else
            StopHeadBob();
    }

    private void StartHeadBob()
    {

        Vector3 targetPos = _startPos;
        targetPos.x += Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f;
        targetPos.y += Mathf.Sin(Time.time * Frequency) * Amount * 1.4f;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            Smooth * Time.deltaTime
        );
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == _startPos) return;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            _startPos,
            Smooth * Time.deltaTime
        );
    }
}