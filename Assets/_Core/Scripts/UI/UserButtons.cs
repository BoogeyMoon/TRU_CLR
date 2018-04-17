using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButtons : MonoBehaviour
{
    [SerializeField]
    GameObject playButton;
    GameObject play;
    PlayButtonManager buttonManager;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void Start()
    {
        buttonManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PlayButtonManager>();
    }
    void OnClick()
    {
        if (play == null)
        {
            play = Instantiate(playButton) as GameObject;
            play.SetActive(true);
            play.transform.SetParent(gameObject.transform, false);
            buttonManager.PlayButton = play;
        }
    }


}
