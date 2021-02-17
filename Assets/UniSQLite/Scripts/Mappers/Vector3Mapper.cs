using System.Globalization;
using UnityEngine;

namespace UniSQLite.Mappers
{
    public class Vector3Mapper : Mapper<Vector3>
    {
        protected override Vector3 Deserialize(string text)
        {
            if (text != string.Empty)
            {
                if (text.StartsWith("(") && text.EndsWith(")")) 
                    text = text.Substring(1, text.Length - 2);

                string[] vectorValues = text.Split(',');

                return new Vector3(
                    float.Parse(vectorValues[0], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(vectorValues[1], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(vectorValues[2], CultureInfo.InvariantCulture.NumberFormat));
            }
            else
            {
                return Vector3.zero;
            }
        }

        protected override string Serialize(Vector3 obj)
        {
            return obj.ToString();
        }
    }
}