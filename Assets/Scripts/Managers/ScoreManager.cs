using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mirror;

public class ScoreManager : NetworkBehaviour
{
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}
