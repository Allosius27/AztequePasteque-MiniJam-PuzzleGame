using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public SpriteRenderer graphics;
    public float r, g, b, a;
    public bool isTouched = false;
    public bool isBonus = false;
    public bool isDoublePointsBonus = false;
    public bool isTrapShootBonus = false;

    public float forceMult = 100.0f;
    public int scoreValue = 10;
    private Vector3 collisionDir = Vector3.zero;

    public DoublePointsBonusBloc doublePointsBonusBloc;
    public TrapShootBonusBloc trapShootBonusBloc;

    // Start is called before the first frame update
    void Start()
    {
        doublePointsBonusBloc = GetComponent<DoublePointsBonusBloc>();
        trapShootBonusBloc = GetComponent<TrapShootBonusBloc>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            collisionDir = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, 0);
            collisionDir = collisionDir.normalized;
            hit.rigidbody.AddForce(collisionDir * forceMult);

            AudioManager.instance.PlaySFX(1);

            if (!isTouched)
            {
                int score = (int)(scoreValue * GameCore.instance.comboScoreMultiplier);

                graphics.color = new Color(r, g, b, a);
                if (this.isBonus == false)
                {
                    GameCore.instance.DisplayPointsScore(this.transform, score);
                }
                else if(this.isBonus && this.isDoublePointsBonus && doublePointsBonusBloc != null)
                {
                    GameCore.instance.DisplayTextEffect(this.transform, GameCore.instance.textEffectDoublePoints);
                    //Debug.Log(GameCore.instance.scoreToAdd);
                    doublePointsBonusBloc.Effect();
                    AudioManager.instance.PlaySFX(3);
                    //Debug.Log(GameCore.instance.scoreToAdd);
                }
                else if(this.isBonus && this.isTrapShootBonus && trapShootBonusBloc != null)
                {
                    GameCore.instance.DisplayTextEffect(this.transform, GameCore.instance.textEffectTrapShoot);
                    trapShootBonusBloc.Effect();
                    AudioManager.instance.PlaySFX(4);
                }
                GameCore.instance.scoreToAdd += score;
                GameCore.instance.comboNumber += 1;
                isTouched = true;
            }
        }
    }
}
