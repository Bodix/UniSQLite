using UnityEngine;
using UnityEngine.UI;
using UniSQLite;

namespace Example.Scripts
{
    public class Example : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;
        [SerializeField]
        private Transform container = null;
        [SerializeField]
        private EntityView entityPrefab = null;

        private void Awake()
        {
            button.onClick.AddListener(GetEntities);
        }

        [ContextMenu("Get entities")]
        public void GetEntities()
        {
            SQLiteDatabase database = new SQLiteDatabase("database.db");
            
            ClearContainer();
            Entity[] entities = database.GetAll<Entity>();
            foreach (Entity entity in entities) 
                Instantiate(entityPrefab, container).Initialize(entity);

            database.ShowTable<Entity>();
        }

        private void ClearContainer()
        {
            foreach (Transform child in container) 
                Destroy(child);
        }
    }
}