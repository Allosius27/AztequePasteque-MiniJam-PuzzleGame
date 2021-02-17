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

    public float forceMult = 100.0f;
    public int scoreValue = 10;
    private Vector3 collisionDir = Vector3.zero;

    DoublePointsBonusBloc doublePointsBonusBloc;

    // Start is called before the first frame update
    void Start()
    {
        doublePointsBonusBloc = GetComponent<DoublePointsBonusBloc>();
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

            if (!isTouched)
            {
                graphics.color = new Color(r, g, b, a);
                if (this.isBonus == false)
                {
                    GameCore.instance.DisplayPointsScore(this.transform, scoreValue);
                }
                else if(this.isBonus && this.isDoublePointsBonus && doublePointsBonusBloc != null)
                {
                    GameCore.instance.DisplayTextEffectBallSaver(this.transform, GameCore.instance.textEffectDoublePoints);
                    //Debug.Log(GameCore.instance.scoreToAdd);
                    doublePointsBonusBloc.Effect();
                    //Debug.Log(GameCore.instance.scoreToAdd);
                }
                GameCore.instance.scoreToAdd += scoreValue;
                isTouched = true;
            }
        }
    }
}
