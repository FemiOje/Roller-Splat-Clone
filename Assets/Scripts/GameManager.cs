using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private GroundUnitController[] allGroundUnits;
    public ParticleSystem[] winParticleSystem;

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
        //cache the ground tiles, if we're not in the main menu
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            SetupNewLevel();
        }
    }
    private void SetupNewLevel() // this runs about four times at once
    {
        allGroundUnits = FindObjectsOfType<GroundUnitController>();
        Time.timeScale = 1;

        winParticleSystem = FindObjectsOfType<ParticleSystem>();
        Debug.Log(winParticleSystem.Length);
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

        if (isComplete)
        {
            StartCoroutine(PlayWinParticleAndLoadNextLevel());
        }
    }

    private IEnumerator PlayWinParticleAndLoadNextLevel()
    {
        for (int i=0; i < winParticleSystem.Length; i++)
        {
            winParticleSystem[i].Play();

        }

        yield return new WaitForSeconds(3);
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            //handle completion particle system
            Debug.Log("You have reached the end of the game");
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}