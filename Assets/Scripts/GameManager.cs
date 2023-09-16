using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private GroundUnitController[] allGroundUnits;
    public ParticleSystem[] winParticleSystem;
    private int finalSceneBuildIndex = 7;
    private bool isCompleting = false; // Added flag to prevent multiple calls
    private void Awake()
    {
        // Check if an instance of GameManager already exists
        if (singleton == null)
        {
            // If not, set this instance as the GameManager and don't destroy it on scene changes
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this GameObject
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // Cache the ground tiles and particle systems if we're not in the main menu
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SetupNewLevel();
        }
    }

    private void SetupNewLevel()
    {
        allGroundUnits = FindObjectsOfType<GroundUnitController>();
        winParticleSystem = FindObjectsOfType<ParticleSystem>();
        Time.timeScale = 1;
    }

    public void CheckIfComplete()
    {
        bool isComplete = true;

        for (int i = 0; i < allGroundUnits.Length; i++)
        {
            if (allGroundUnits[i].hasMadeContact == false)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete && !isCompleting)
        {
            isCompleting = true;
            StartCoroutine(PlayWinParticleAndLoadNextLevel());
        }
    }

    private IEnumerator PlayWinParticleAndLoadNextLevel()
    {
        // Play the win particle system
        for (int i = 0; i < winParticleSystem.Length; i++)
        {
            winParticleSystem[i].Play();
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        if (SceneManager.GetActiveScene().buildIndex == finalSceneBuildIndex)
        {
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        isCompleting = false; // Reset the flag
    }
}
