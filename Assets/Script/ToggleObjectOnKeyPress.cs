using UnityEngine;

public class ToggleObjectOnTrigger : MonoBehaviour
{
    public GameObject targetObject; // The object to activate/deactivate
    public AudioSource audioSource; // The AudioSource component for playing the sound
    private bool isPlayerInTrigger = false; // Track if the player is in the trigger

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = targetObject.activeSelf; // Check if the object is currently active
            targetObject.SetActive(!isActive); // Toggle the object's active state
            
            if (audioSource != null)
            {
                audioSource.Play(); // Play the sound
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
