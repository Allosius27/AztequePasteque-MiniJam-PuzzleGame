using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(GameCore.instance.fxLose, GameCore.instance.losePoint.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
