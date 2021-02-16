using System.Linq;
using UniSQLite;
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

        [ContextMenu("Get entities")]
        public void GetEntities()
        {
            SQLiteDatabase database = new SQLiteDatabase("database.db");
            database.ShowTable<Entity>();

            Debug.Log(string.Join(", ", database.GetAll<Entity>().Select(x => x.Id + "\n" + x.Name + "\n" + x.Position)));
        }
    }
}