using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public static bool interactedWithFirstNPC = false; // Variable to track if the first NPC has been interacted with
    public static bool interactedWithSecondNPC = false; // Variable to track if the second NPC has been interacted with

    void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("E key pressed");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("NPC1"))
            {
                interactedWithFirstNPC = true;
                Debug.Log("Interacting with first NPC");
            }
            else if (hit.collider.CompareTag("NPC2"))
            {
                interactedWithSecondNPC = true;
                Debug.Log("Interacting with second NPC");
            }
        }
    }
}
}
