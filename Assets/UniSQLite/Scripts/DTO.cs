using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UniSQLite.Scripts.Serialization;
using UnityEngine;
using ISerializable = System.Runtime.Serialization.ISerializable;
using UnityObject = UnityEngine.Object;

namespace UniSQLite.Scripts
{
    public class DTO
    {
        // private Dictionary<string, UnityObject> serializedObjects = new Dictionary<string, UnityObject>();
        // private Dictionary<string, string> serializedStrings = new Dictionary<string, string>();
        // private BinaryFormatter serializer = new BinaryFormatter();
        //
        // public void OnAfterDeserialize()
        // {
        //     Deserialize();
        // }
        //
        // public void OnBeforeSerialize()
        // {
        //     Serialize();
        // }
        //
        // private void Serialize()
        // {
        //     foreach (PropertyInfo property in GetProperties())
        //     {
        //         object value = property.GetValue(this);
        //         if (value == null)
        //             continue;
        //         
        //         string name = property.Name;
        //         if (value is UnityObject obj) // the implementor is a UnityEngine.Object
        //         {
        //             serializedObjects[name] = obj; // using the field's name as a key because you can't have two fields with the same name
        //         }
        //         else
        //         {
        //             if (!value.GetType().IsSerializable) 
        //                 value = SerializationUtility.TryGetSerializableVersion(value);
        //
        //             // try to serialize the interface to a string and store the result in our other dictionary
        //             using (var stream = new MemoryStream())
        //             {
        //                 serializer.Serialize(stream, value);
        //                 stream.Flush();
        //                 serializedObjects.Remove(name); // it could happen that the field might end up in both the dictionaries, ex when you change the implementation of the interface to use a System.Object instead of a UnityObject
        //                 serializedStrings[name] = Convert.ToBase64String(stream.ToArray());
        //             }
        //         }
        //     }
        // }
        //
        // private void Deserialize()
        // {
        //     foreach (var field in GetProperties())
        //     {
        //         object result = null;
        //         string name = field.Name;
        //         // Try and fetch the field's serialized value
        //         UnityObject obj;
        //         if (serializedObjects.TryGetValue(name, out obj)
        //         ) // if the implementor is a UnityObject, then we just fetch the value from our dictionary as the result
        //         {
        //             result = obj;
        //         }
        //         else // otherwise, get it from our other dictionary
        //         {
        //             string serializedString;
        //             if (serializedStrings.TryGetValue(name, out serializedString))
        //             {
        //                 // deserialize the string back to the original object
        //                 byte[] bytes = Convert.FromBase64String(serializedString);
        //                 using (var stream = new MemoryStream(bytes))
        //                     result = serializer.Deserialize(stream);
        //             }
        //         }
        //
        //         field.SetValue(this, result);
        //     }
        // }
        //
        // private IEnumerable<PropertyInfo> GetProperties()
        // {
        //     return GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //         .Where(x => !x.IsDefined(typeof(HideInInspector)));
        // }
    }
}