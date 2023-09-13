using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private GroundUnitController[] allGroundUnits;
    public string lastLevelName = "Level5";


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
    private void Start()
    {
        SetupNewLevel();
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }
    private void SetupNewLevel()
    {
        allGroundUnits = FindObjectsOfType<GroundUnitController>();
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
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Debug.Log("You have reached the end of the game");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
