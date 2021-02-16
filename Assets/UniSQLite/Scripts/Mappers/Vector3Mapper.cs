using System.Globalization;
using System.Linq;
using UnityEngine;

namespace UniSQLite.Mappers
{
    public class Vector3Mapper : Mapper<Vector3>
    {
        protected override Vector3 Deserialize(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string[] vectorValues = text.Split(',').ToArray();

                return new Vector3(
                    float.Parse(vectorValues[0], CultureInfo.InvariantCulture.NumberFormat), 
                    float.Parse(vectorValues[1], CultureInfo.InvariantCulture.NumberFormat), 
                    float.Parse(vectorValues[2], CultureInfo.InvariantCulture.NumberFormat));
            } else
            {
                return Vector3.zero;
            }
        }

        protected override string Serialize(Vector3 @object)
        {
            throw new System.NotImplementedException();
        }
    }
}