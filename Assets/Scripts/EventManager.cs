using UnityEngine;
using UnityEngine.UI;

namespace Dupa
{
    public class EventManager : MonoBehaviour
    {

        public static EventManager instance;

        public delegate void EventHandler(GameObject gameObj);
        public static event EventHandler OnEvent;

        public Dropdown dropdown;
        public GameObject optionsMenu;

        private void Awake()
        {
            //Check if there is already an instance of SoundManager
            if (instance == null)
                //if not, set it to this.
                instance = this;
            //If instance already exists:
            else if (instance != this)
                //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
                Destroy(gameObject);

            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            // Reverse the active state every time escape is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Check whether it's active / inactive
                bool isActive = optionsMenu.activeSelf;

                optionsMenu.SetActive(!isActive);
            }
        }

        public static void TriggerEvent(GameObject gameObj)
        {
            OnEvent?.Invoke(gameObj);
        }

        public void ChangeResolution()
        {
            switch (dropdown.value)
            {
                case 1: Screen.SetResolution(1920, 1080, true); break;
                case 2: Screen.SetResolution(1600, 900, false); break;
                case 3: Screen.SetResolution(1024, 768, false); break;
                default: print("none"); break;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}