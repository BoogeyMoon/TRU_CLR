using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Timmy Alvelöv

//Ser tll att spelknappen försvinner
public class PlayButtonManager : MonoBehaviour
{
    GameObject playButton;
    public GameObject PlayButton
    {
        set { Destroy(playButton); playButton = value; }
    }
	
}
