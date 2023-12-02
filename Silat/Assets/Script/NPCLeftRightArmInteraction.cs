using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class NPCLeftRightArmInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust this value as needed
    public Animator animator;
    [SerializeField] private NPCConversation myConversation;
    private GameObject playerObject;
    public int sceneNumber;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }
    }
    public void LeftPunch()
    {
        animator.SetTrigger("hit");
        Fight fight = playerObject.GetComponentInChildren<Fight>();
        fight.LeftArmUnlockedByNPC();
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
    }
    public void RightPunch()
    {
        animator.SetTrigger("hit1");
        Fight fight = playerObject.GetComponentInChildren<Fight>();
        fight.RightArmUnlockedByNPC();
    }
    public void NextScene()
    {
        int sceneName = sceneNumber;
        SceneManager.LoadScene(sceneName);
    }

}
