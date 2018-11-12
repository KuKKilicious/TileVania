using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField]
    AudioClip coinPickupSFX;
    [SerializeField]
    int coinPickupAmount = 50;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        FindObjectOfType<GameSession>().AddScore(coinPickupAmount);
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position,0.2f);
        Destroy(gameObject);
    }
}
