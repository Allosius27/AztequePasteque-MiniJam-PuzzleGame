using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShootArrows : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool isFiring = true;
    public float countdown = 7.5f;

    public Transform[] waypoints;
    private Transform target;

    public bool isActive;
    public int count;
    public int countMax = 6;
    public float speed;
    private int destPoint;
    public DeathZone deathZone;

    public float forceMult = 100.0f;
    private Vector3 collisionDir = Vector3.zero;

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

        if (isFiring == true && isActive && count < countMax)
        {
            StartCoroutine(Shoot());
            isFiring = false;
        }
    }

    IEnumerator Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        AudioManager.instance.PlaySFX(5);
        count += 1;
        //AudioManager.instance.PlaySFX(4);
        yield return new WaitForSeconds(countdown);
        isFiring = true;
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

            
        }
    }
}
