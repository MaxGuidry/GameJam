using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class InteractableScript : MonoBehaviour
    {

        public Transform playerHead;
        public Text text;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            int mask = ~LayerMask.GetMask("Ignore Cam Raycast");
           // Debug.Log(mask);
           // Debug.DrawRay(playerHead.transform.position + new Vector3(0, 1, 0), Camera.main.transform.forward * 2, Color.black);
            if (Physics.SphereCast(playerHead.position - Camera.main.transform.forward, .5f, Camera.main.transform.forward, out hit, 3,mask))
            {
                if (hit.transform == null)
                    return;
                IInteractable iact = hit.transform.gameObject.GetComponent<IInteractable>();
                if (iact != null)
                {
                    text.text = "Press " + InputMap.KeyBinds["Interact"].ToString() + " to " + iact.WhatDo + " " + iact.Name;
                    text.gameObject.SetActive(true);

                }
                else
                {
                    text.gameObject.SetActive(false);
                }
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }
}
