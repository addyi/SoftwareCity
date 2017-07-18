using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SoftwareCity.ConfigMenu
{
    /// <summary>
    /// Script to follow the cursor.
    /// </summary>
    public class MenuFollowCursor : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// Save the reference of the current cursor.
        /// </summary>
        private GameObject cursor;

        void Start()
        {
            cursor = GameObject.FindGameObjectWithTag("Cursor");

            //gameObject.GetComponent<SpriteRenderer>().enabled = true;

            InputManager.Instance.PushModalInputHandler(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            this.gameObject.transform.position = cursor.transform.position;
            this.gameObject.transform.eulerAngles = new Vector3(cursor.transform.eulerAngles.x, cursor.transform.eulerAngles.y + 180f, cursor.transform.eulerAngles.z);
        }

        /// <summary>
        /// Input event is clicked then place the menu in the global space.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            /*
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            */
            InputManager.Instance.PopModalInputHandler();
            this.gameObject.GetComponent<MenuFollowCursor>().enabled = false;
        }
    }
}
