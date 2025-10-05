using UnityEngine;

namespace Blaze
{
    public class SceneInjection : MonoBehaviour
    {
        public GameObject _Instantiate(GameObject _gameObject)
        {
            return Instantiate(_gameObject);
        }
        public GameObject _Instantiate(GameObject _gameObject, Transform anchor)
        {
            return Instantiate(_gameObject, anchor);
        }

        public void _Destroy(GameObject _gameObject, float delay = 0f)
        {
            Destroy(_gameObject, delay);
        }
    }
}
