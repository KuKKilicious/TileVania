using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    private void SomethingHappens() {
	//start coroutine

    }

    private IEnumerator HasAName()
    {
        //yield with a delay
        //load the next scene
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
