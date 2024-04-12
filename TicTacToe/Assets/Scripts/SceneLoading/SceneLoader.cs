using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance => instance;

        // Creating a simple singleton
        private static SceneLoader instance;
        public string SceneToLoad => sceneToLoad;
    
        [SerializeField] private string sceneToLoad = "Game";
    
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadScene(string sceneName, Action<AsyncOperation> onSceneLoadedCallback = null)
        {
            AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            sceneOperation.completed += OnSceneLoaded;
            if (onSceneLoadedCallback != null)
            {
                sceneOperation.completed += onSceneLoadedCallback;
            }
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {
            // Do some logic
        }
    }
}