using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SettingsXmlReader : MonoBehaviour
{
    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

    public Text[] textsToTranslate;
    public string[] translates;

    public List<string> listTranslates = new List<string>();

    public Dropdown selectDropdown;

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    // Start is called before the first frame update
    void Awake()
    {
        Reader();
    }

    void Start()
    {
        selectDropdown.value = LangueManager.instance.currentLanguage;
    }


    // Update is called once per frame
    void Update()
    {
        languages[LangueManager.instance.currentLanguage].TryGetValue("Name", out languageName);

        for(int i = 0; i < translates.Length; i++)
        {
            LanguageTryGetValue(translates[i], textsToTranslate[i]);
        }
    }

    void LanguageTryGetValue(string textToTranslate, Text uiTextToTranslate)
    {
        languages[LangueManager.instance.currentLanguage].TryGetValue(textToTranslate, out textToTranslate);

        if (uiTextToTranslate != null)
        {
            uiTextToTranslate.text = textToTranslate;
        }
    }

    void TranslateText(XmlNode value, string textToTranslate)
    {
        if (value.Name == textToTranslate)
        {
            obj.Add(value.Name, value.InnerText);
        }
    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent)
            {
                if (value.Name == "Name")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                for (int i = 0; i < listTranslates.Count; i++)
                {
                    TranslateText(value, listTranslates[i]);
                }
            }

            languages.Add(obj);
        }
    }

    public void ValueChangeCheck()
    {
        LangueManager.instance.currentLanguage = selectDropdown.value;
    }
}
