using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool transition = false;

    public int modifierBall = -1;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameCore.instance.currentLevel.listBlocs.Count; i++)
        {
            if (GameCore.instance.currentLevel.listBlocs[i] == null)
            {
                if (GameCore.instance.currentLevel.listTargetBlocs.Contains(GameCore.instance.currentLevel.listBlocs[i]))
                {
                    GameCore.instance.currentLevel.listTargetBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);
                }

                GameCore.instance.currentLevel.listBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);
            }
        }

        if (GameCore.instance.currentLevel.listTargetBlocs.Count == 0 && GameCore.s_current_level < GameCore.instance.levels.Length - 1 && transition == true)
        {
            GameCore.instance.scoreObtainedActive = true;
            transition = false;
        }
    }

    public void CheckBlocsToDestroy()
    {
        for(int i = 0; i < GameCore.instance.currentLevel.listBlocs.Count; i++)
        {
            if(GameCore.instance.currentLevel.listBlocs[i].isTouched && GameCore.instance.currentLevel.listBlocs[i] != null)
            {
                Debug.Log(GameCore.instance.currentLevel.listBlocs[i].gameObject);
                Destroy(GameCore.instance.currentLevel.listBlocs[i].gameObject);
                /*if(GameCore.instance.currentLevel.listTargetBlocs.Contains(GameCore.instance.currentLevel.listBlocs[i]))
                {
                    GameCore.instance.currentLevel.listTargetBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);
                }
                GameCore.instance.currentLevel.listBlocs.Remove(GameCore.instance.currentLevel.listBlocs[i]);*/
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
            //Debug.Log(this.name + "Collides");

            GameCore.instance.SetStockBalls(modifierBall);

            CheckBlocsToDestroy();
            Destroy(hit.gameObject);
            anim.SetTrigger("AnimLoose");

            GameCore.instance.setScoreAmount = GameCore.instance.scoreToAdd;
            GameCore.instance.scoreToAdd = 0;
            //GameCore.instance.SetScore(GameCore.instance.scoreToAdd);

            transition = true;

            if (GameCore.instance.currentLevel.listTargetBlocs.Count == 0 && GameCore.s_current_level < GameCore.instance.levels.Length-1 && transition == true)
            {
                //Debug.Log("End Level");
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
