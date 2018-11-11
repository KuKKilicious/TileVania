using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
    [SerializeField]
    float timeToLoad = 3f;
    [SerializeField]
    float levelExitSloMoFactor = 0.2f;
    private IEnumerator loadNextLevel;

    private ParticleSystem.EmissionModule particlesEmmision;

    private void Start() {
        particlesEmmision = GetComponent<ParticleSystem>().emission;
        particlesEmmision.enabled = false;
    }
    private void SomethingHappens() {
        //start coroutine
        particlesEmmision.enabled = true;
        

        loadNextLevel = LoadNextLevel(timeToLoad);
        StartCoroutine(loadNextLevel);

    }

    private IEnumerator LoadNextLevel(float waitTime)
    {
        Time.timeScale = levelExitSloMoFactor;
        yield return new WaitForSeconds(waitTime*levelExitSloMoFactor);
        //yield with a delay
        //load the next scene
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        SomethingHappens();
    }
}
