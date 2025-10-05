using UnityEngine;
using UnityEngine.SceneManagement;

namespace Banchy
{
    public class Resetter : MonoBehaviour
    {
        public string gameSceneName;
        
        public void Reset()
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}