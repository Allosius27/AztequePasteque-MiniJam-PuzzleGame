using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LangueManager : MonoBehaviour
{

    public int currentLanguage;
    //public Dropdown selectDropdown;
    //public DictionaryDropdown dictionaryDropdown;
    public static LangueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LangueManager dans la scène");
            return;
        }

        instance = this;

        //dictionaryDropdown = GameObject.FindGameObjectWithTag("LangueDropdown").GetComponent<DictionaryDropdown>();
        //selectDropdown = dictionaryDropdown.selectDropdown;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ValueChangeCheck()
    {
        currentLanguage = selectDropdown.value;
    }*/
}
