using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButtons : MonoBehaviour
{
    string currentPlayer;
    [SerializeField]
    GameObject playButton;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        GameObject play = Instantiate(playButton) as GameObject;
        play.SetActive(true);
        play.transform.SetParent(gameObject.transform, false);
    }

}
