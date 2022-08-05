using System;
using System.IO;
using System.Xml;

namespace Demkin.Blog.Utils.Help
{
    public class XmlHelper
    {
        /// <summary>
        /// 根据路径读取xml文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static XmlDocument XMLFromFilePath(string filePath)
        {
            StreamReader streamReader;

            try
            {
                streamReader = new StreamReader(filePath);
            }
            catch (FileNotFoundException exception)
            {
                throw new Exception("文件不存在", exception);
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(streamReader);
            return xmlDocument;
        }
    }
}