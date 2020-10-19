using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class XMLTest
    {
        [System.Serializable]
        public class XMLTestCls
        {
            [SerializeField] public List<string> list = new List<string>() {"1", "2", "3"};
        }

        private const string xmlPath = "/_04ToDoList/Editor/Resources/Config/test.xml";


        private string GetPath()
        {
            string filePath = Application.dataPath + xmlPath;

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            return filePath;
        }

        [Test]
        public void CreateXML()
        {
            string filePath = GetPath();

            using (var fs = File.OpenWrite(filePath))
            {
                var serializer = new XmlSerializer(typeof(XMLTestCls));

                serializer.Serialize(fs, new XMLTestCls());
            }
        }

        [Test]
        public void LoadXML()
        {
            string filePath = GetPath();

            var doc = new XmlDocument();
            doc.Load(filePath);

            var firstChild = doc.FirstChild;

            Debug.Log(firstChild.Value);

            var element = doc.DocumentElement;

            Debug.Log(element.Name);
        }

        [Test]
        public void ReadXML()
        {
            string filePath = GetPath();

            var doc = new XmlDocument();
            doc.Load(filePath);

            var element = doc.DocumentElement;

            foreach (XmlElement nodeElement in element.ChildNodes)
            {
                if (nodeElement.Name == "Question")
                {
                    foreach (XmlElement child in nodeElement.ChildNodes)
                    {
                        Debug.Log(child.Name);
                    }
                }
                else if (nodeElement.Name == "Choice")
                {
                    foreach (XmlElement child in nodeElement.ChildNodes)
                    {
                        Debug.Log(child.Name);
                    }
                }
            }
        }
    }
}