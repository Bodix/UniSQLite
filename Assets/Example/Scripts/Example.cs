using System.Linq;
using UniSQLite.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Example.Scripts
{
    public class Example : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        private void Awake()
        {
            button.onClick.AddListener(GetEntities);
        }

        public void GetEntities()
        {
            SQLiteDatabase database = new SQLiteDatabase("database.db");
            database.ShowTable(database.GetAll<Entity>()[0]);

            Debug.Log(string.Join(", ", database.GetAll<Entity>().Select(x => x.Id + "\n" + x.Name + "\n" + x.Positions)));
        }
    }
}