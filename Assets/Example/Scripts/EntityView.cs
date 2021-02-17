using UnityEngine;
using UnityEngine.UI;

namespace Example.Scripts
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField]
        private Text text = null;

        public void Initialize(Entity entity)
        {
            text.text =
                $"ID: {entity.Id}\n" +
                $"Name: {entity.Name}\n" +
                $"Position: {entity.Position}";
        }
    }
}