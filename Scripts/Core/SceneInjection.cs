using UnityEngine;

namespace Blaze
{
    public class SceneInjection : MonoBehaviour
    {
        public GameObject _Instantiate(GameObject gameObject)
        {
            return Instantiate(gameObject);
        }
        public GameObject _Instantiate(GameObject gameObject, Transform anchor)
        {
            return Instantiate(gameObject, anchor);
        }

        public void _Destroy(GameObject gameObject)
        {
            Destroy(gameObject);
        }

    }
}
