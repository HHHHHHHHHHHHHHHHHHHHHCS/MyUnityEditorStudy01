using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class XMLTest
    {
        [System.Serializable]
        public class XMLTestCls
        {
            [SerializeField] public List<string> list = new List<string>() {"1", "2", "3"};
        }

        private const string addPath = "/_04ToDoList/Editor/Resources/QuestionConfig/test.xml";

        [Test]
        public void CreateXML()
        {
            string filePath = Application.dataPath + addPath;

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var fs = File.OpenWrite(filePath))
            {
                var serializer = new XmlSerializer(typeof(XMLTestCls));

                serializer.Serialize(fs, new XMLTestCls());
            }
        }

        [Test]
        public void LoadXML()
        {
            string filePath = Application.dataPath + addPath;

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));


            var doc = new XmlDocument();
            doc.Load(filePath);

            var firstChild = doc.FirstChild;

            Debug.Log(firstChild.Value);

            var element = doc.DocumentElement;

            Debug.Log(element.Name);

        }
    }
}