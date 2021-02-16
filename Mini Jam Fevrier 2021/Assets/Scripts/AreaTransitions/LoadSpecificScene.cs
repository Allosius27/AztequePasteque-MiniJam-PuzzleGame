using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;

    public static LoadSpecificScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadSpecificScene dans la scène");
            return;
        }

        instance = this;

        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(2);
        /*for (int i = 0; i < Inventory.instance.listInvisibleSquare.Count; i++)
        {
            Destroy(Inventory.instance.listInvisibleSquare[i]);
        }
        for (int i = 0; i < PlayerSpawn.instance.listChecksPointActivations.Count; i++)
        {
            Destroy(PlayerSpawn.instance.listChecksPointActivations[i]);
        }
        Destroy(Inventory.instance.gameObject);
        Destroy(PlayerSpawn.instance.gameObject);
        SceneManager.LoadScene(sceneName);*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
