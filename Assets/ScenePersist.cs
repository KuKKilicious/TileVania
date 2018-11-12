using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {
    private int startingBuildIndex;
    private void Awake() {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersist > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        startingBuildIndex = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != startingBuildIndex) {
            Destroy(gameObject);
        }
	}
}
