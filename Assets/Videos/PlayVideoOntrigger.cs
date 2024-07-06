using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class PlayVideoOnTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay;
    public Canvas videoCanvas;
    public Transform destination; // Add a public Transform for the destination
    public bool flipHorizontally = false; // Add a public boolean to control horizontal flip
    private bool isPlayerInTrigger = false;
    private Transform playerTransform; // Store the player's transform

    void Start()
    {
        videoCanvas.gameObject.SetActive(false); // Hide the video canvas at the start
        videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to the end of the video event
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PlayVideo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            playerTransform = other.transform; // Store the player's transform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void PlayVideo()
    {
        videoCanvas.gameObject.SetActive(true);
        videoDisplay.gameObject.SetActive(true); // Activate the RawImage
        videoPlayer.gameObject.SetActive(true); // Activate the VideoPlayer
        
        // Flip the video horizontally if the option is enabled
        if (flipHorizontally)
        {
            videoDisplay.rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            videoDisplay.rectTransform.localScale = new Vector3(1, 1, 1); // Ensure the scale is normal if not flipping
        }

        videoPlayer.Play();
        StartCoroutine(TransportPlayerWithDelay(1f)); // Start the coroutine to transport the player with a delay
    }

    private IEnumerator TransportPlayerWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        TransportPlayer();
    }

    private void TransportPlayer()
    {
        if (playerTransform != null && destination != null)
        {
            playerTransform.position = destination.position; // Move the player to the destination
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        videoCanvas.gameObject.SetActive(false);
        videoDisplay.gameObject.SetActive(false); // Deactivate the RawImage
        videoPlayer.gameObject.SetActive(false); // Deactivate the VideoPlayer
    }
}
