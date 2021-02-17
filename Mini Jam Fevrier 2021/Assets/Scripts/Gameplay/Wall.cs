using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float forceMult = 100.0f;
    private Vector3 collisionDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
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

            
        }
    }
}
