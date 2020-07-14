using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns
{
    /// <summary>
    /// Binary Formatter Enforces Serilizable on it's classes,
    /// Xml Formatter Enforces default Constructor on it's though
    /// </summary>
    public static partial class Extensions
    {
        public static T DeepCopy_BinaryFormatter<T>(T obj)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }
        
        public static T DeepCopy_XML<T>(T obj)
        {
            using var stream = new MemoryStream();
            var xmlFormatter = new XmlSerializer(typeof(T));
            xmlFormatter.Serialize(stream, obj);
            stream.Position = 0;
            return (T)xmlFormatter.Deserialize(stream);
        }
    }
}