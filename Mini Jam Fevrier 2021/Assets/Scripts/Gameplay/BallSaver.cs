using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSaver : MonoBehaviour
{
    public Transform[] waypoints;
    private Transform target;

    public float speed;
    private int destPoint;

    public int modifierBall = 0;

    public DeathZone deathZone;


    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

        }
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {

        Entity typeCollision = hit.gameObject.GetComponent<Entity>();

        if (typeCollision == null)
        {
            return;
        }

        if (typeCollision.Type == Entity.TypeObject.Player)
        {
            //Debug.Log(this.name + "Collides");

            GameCore.instance.DisplayTextEffect(this.transform, GameCore.instance.textEffectBallSaver);

            GameCore.instance.SetStockBalls(modifierBall);


            deathZone.CheckBlocsToDestroy();
            Destroy(hit.gameObject);

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
    }
}
