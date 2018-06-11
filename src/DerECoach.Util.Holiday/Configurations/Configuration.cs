using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace DerECoach.Util.Holiday.Configurations
{
    public partial class Configuration
    {
        #region indexed subconfigurations -------------------------------------
        public Configuration this[string key]
        {
            get { return SubConfigurations.Single(subconfiguration => subconfiguration.hierarchy == key); }
            set
            {
                var existing = SubConfigurations.FirstOrDefault(a => a.hierarchy == key);
                if (existing != null)
                {
                    SubConfigurations.Remove(existing);
                }
                SubConfigurations.Add(value);
                
            }
        }

        #endregion

        #region properties ----------------------------------------------------
        private static XmlSerializer _serializer;
        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializer(typeof(Configuration));

                }
                return _serializer;
            }
        }
        #endregion

        #region Serialize/Deserialize -----------------------------------------
        /// <summary>
        /// Serializes current Configuration object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                Serializer.Serialize(memoryStream, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public static Configuration Deserialize(string xml)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(xml);
                return ((Configuration)(Serializer.Deserialize(XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                var xmlString = Serialize();
                var xmlFile = new FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        public static Configuration LoadFromFile(string fileName)
        {
            FileStream file = null;
            StreamReader sr = null;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(file);
                var xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion
        
    }
}
