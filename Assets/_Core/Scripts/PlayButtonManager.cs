using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonManager : MonoBehaviour
{
    GameObject playButton;
    public GameObject PlayButton
    {
        set { Destroy(playButton); playButton = value; }
    }
	
}
