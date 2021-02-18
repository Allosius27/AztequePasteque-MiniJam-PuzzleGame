using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody2D rb;
    DeathZone deathZone;

    // Start is called before the first frame update
    void Start()
    {
        deathZone = GetComponentInParent(typeof(DeathZone)) as DeathZone;

        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCore.instance.desactivateArrow)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity typeCollision = collision.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.Type == Entity.TypeObject.Player)
        {
            if (deathZone != null)
            {
                GameCore.instance.SetStockBalls(deathZone.modifierBall);

                deathZone.CheckBlocsToDestroy();
                Destroy(collision.gameObject);

                GameCore.instance.setScoreAmount = GameCore.instance.scoreToAdd;
                GameCore.instance.scoreToAdd = 0;
                //GameCore.instance.SetScore(GameCore.instance.scoreToAdd);

                deathZone.transition = true;

                if (GameCore.instance.currentLevel.listTargetBlocs.Count == 0 && GameCore.s_current_level < GameCore.instance.levels.Length - 1 && deathZone.transition == true)
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

            Destroy(this.gameObject);
        }

        if (typeCollision.Type == Entity.TypeObject.Bloc)
        {
            Bloc bloc = collision.gameObject.GetComponent<Bloc>();
            if (!bloc.isTouched)
            {
                int score = (int)(bloc.scoreValue * GameCore.instance.comboScoreMultiplier);

                bloc.graphics.color = new Color(bloc.r, bloc.g, bloc.b, bloc.a);
                if (bloc.isBonus == false)
                {
                    GameCore.instance.DisplayPointsScore(this.transform, score);
                }
                else if (bloc.isBonus && bloc.isDoublePointsBonus && bloc.doublePointsBonusBloc != null)
                {
                    GameCore.instance.DisplayTextEffect(this.transform, GameCore.instance.textEffectDoublePoints);
                    //Debug.Log(GameCore.instance.scoreToAdd);
                    bloc.doublePointsBonusBloc.Effect();
                    //Debug.Log(GameCore.instance.scoreToAdd);
                }
                else if (bloc.isBonus && bloc.isTrapShootBonus && bloc.trapShootBonusBloc != null)
                {
                    GameCore.instance.DisplayTextEffect(this.transform, GameCore.instance.textEffectTrapShoot);
                    bloc.trapShootBonusBloc.Effect();
                }
                GameCore.instance.scoreToAdd += score;
                GameCore.instance.comboNumber += 1;
                bloc.isTouched = true;
            }

            Destroy(this.gameObject);
        }

       
    }
}
