using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SerializationDeserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\670264172\\Desktop\\LocalGIT\\SerializationDeserialization\\information.txt";
            Student student = new Student();
            student.name = "charitha";
            student.regno = "17MIS0044";

            DataSerialization dataSerialization = new DataSerialization();
            //dataSerialization.BinaryFormatSerialize(student, path);

            Student student1 = new Student();
            //student1 = dataSerialization.BinaryDeserialize(path) as Student;
            //Console.WriteLine($"Name : {student1.name}");
            //Console.WriteLine($"Registration Number : {student1.regno}");

            //dataSerialization.XMLSerialize(typeof(Student), student, path);
            //student1 = dataSerialization.XMLDeserialize(typeof(Student), path) as Student;
            //Console.WriteLine($"Name : {student1.name}");
            //Console.WriteLine($"Registration No : {student1.regno}");

            dataSerialization.JSONSerializer(student, path);
            student1 = dataSerialization.JSONDeserializer(typeof(Student), path) as Student;
            Console.WriteLine($"Name : {student1.name}");
            Console.WriteLine($"Reg. Number : {student1.regno}");

        }
    }

    [Serializable]
    public class Student
    {
       public string name { get; set; }
       public string regno { get; set; }

    }

    class DataSerialization
    {
        //serialising using BinaryFormatter
        public void BinaryFormatSerialize(Object details, string filePath)
        {
            FileStream stream = File.Create(filePath);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, details);
            stream.Close();
        }

        //Deserialising using BinaryFormatter
        public object BinaryDeserialize(string filePath)
        {
            FileStream fileStream1 = File.OpenRead(filePath);
            BinaryFormatter binaryFormatter1 = new BinaryFormatter();
            object deserialisedDetails = binaryFormatter1.Deserialize(fileStream1);
            return deserialisedDetails;
        }

        //serialising using XML
        public void XMLSerialize(Type type, Object details, string filePath)
        {
            StreamWriter streamWriter = new StreamWriter(filePath);
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            xmlSerializer.Serialize(streamWriter, details);
            streamWriter.Close();
        }

        //Deserialising using XML
        public object XMLDeserialize(Type type, string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            XmlSerializer xmlSerializer1 = new XmlSerializer(type);
            object deserialisedDetails = xmlSerializer1.Deserialize(streamReader);
            return deserialisedDetails;
        }

        //serialising using JSON
        public void JSONSerializer(Object details, string filePath)
        {
            StreamWriter streamWriter = new StreamWriter(filePath);
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonWriter jsonWriter = new JsonTextWriter(streamWriter);
            jsonSerializer.Serialize(jsonWriter, details);
            jsonWriter.Close();
            streamWriter.Close();
        }

        //Deserialising using JSON
        public object JSONDeserializer(Type type, string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            JsonSerializer jsonSerializer1 = new JsonSerializer();
            JsonReader jsonReader = new JsonTextReader(streamReader);
            JObject details = jsonSerializer1.Deserialize(jsonReader) as JObject;
            return details.ToObject(type);
        }
    }
}
