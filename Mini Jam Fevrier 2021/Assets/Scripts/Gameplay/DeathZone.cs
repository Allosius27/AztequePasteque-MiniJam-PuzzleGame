using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckBlocsToDestroy()
    {
        for(int i = 0; i < GameCore.instance.currentLevel.listBlocs.Count; i++)
        {
            if(GameCore.instance.currentLevel.listBlocs[i].isTouched)
            {
                Destroy(GameCore.instance.currentLevel.listBlocs[i].gameObject);
                if(GameCore.instance.currentLevel.listTargetBlocs.Contains(GameCore.instance.currentLevel.listBlocs[i]))
                {
                    GameCore.instance.currentLevel.listTargetBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);
                }
                GameCore.instance.currentLevel.listBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Entity typeCollision = hit.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.Type == Entity.TypeObject.Player)
        {
            Debug.Log(this.name + "Collides");

            GameCore.instance.SetStockBalls(-1);
            

            CheckBlocsToDestroy();
            Destroy(hit.gameObject);

            GameCore.instance.SetScore(GameCore.instance.scoreToAdd);

            if (GameCore.instance.currentLevel.listTargetBlocs.Count == 0 && GameCore.s_current_level < GameCore.instance.levels.Length-1)
            {
                Debug.Log("End Level");
                GameCore.instance.scoreObtainedActive = true;
                //GameCore.instance.ScoreObtainedAtLevel();
                //GameCore.instance.NextLevel();
            }
            else
            {
                GameCore.instance.launcher.ballRelaunch = true;
            }
        }
    }
}
