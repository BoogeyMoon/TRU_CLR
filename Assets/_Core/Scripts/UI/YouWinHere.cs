using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinHere : MonoBehaviour {

    private float timer;
    private bool waitForKey;
    [SerializeField] GameObject winText;

	// Use this for initialization
	void Start () {
        waitForKey = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (waitForKey)
        {
            print("waiting for enter");
            if (Input.GetKeyDown(KeyCode.Return))
            { 
                Destroy(winText.gameObject);
                SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
            }
        }
	}

     private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            winText.SetActive(true);            
            waitForKey = true;

        }

    }
}
