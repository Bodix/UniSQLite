using UnityEngine;
using UnityEngine.UI;
using UniSQLite;

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
        }
    }
}