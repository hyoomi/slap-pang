using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Bestscore : MonoBehaviour
{
    void Start()
    {
        CreateXml();
    }

    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Bestscore", string.Empty);
        xmlDoc.AppendChild(root);

        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Bestscore", string.Empty);
        root.AppendChild(child);

        XmlElement Score = xmlDoc.CreateElement("Score");
        Score.InnerText = "d";
        child.AppendChild(Score);

    }

    void Update()
    {
        
    }
}
