using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactRadius = 2f;
    public string promptText = "Press E to interact";

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}