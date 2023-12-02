using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class NPCKickInteraction : MonoBehaviour
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
    public void Kick()
    {
        animator.SetTrigger("kick");
        Fight fight = playerObject.GetComponentInChildren<Fight>();
        fight.KickUnlockedByNPC();
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
    }

    public void NextScene()
    {
        int sceneName = sceneNumber;
        SceneManager.LoadScene(sceneName);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentSceneIndex >= unlockedLevel - 1)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
