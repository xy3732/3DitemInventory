using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePix.input
{
    public class inputManager : MonoBehaviour
    {

        [field: SerializeField] private Queue<GameObject> obejctQueue { get; set; }

        private void Awake()
        {
            obejctQueue = new Queue<GameObject>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote)) UIManager.instance.debugController.OnToggleDebug();
            if (Input.GetKeyDown(KeyCode.Return)) UIManager.instance.debugController.OnReturn();
            if (Input.GetKeyDown(KeyCode.I)) InventoryView.instance.ShowInventory();
            if (Input.GetKeyDown(KeyCode.K)) SkillView.instance.ShowSkillsView();
            if (Input.GetMouseButtonDown(0)) TouchOBject();
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        Vector3 mousePos;
        private void TouchOBject()
        {
            mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) 
            {
                if (hit.collider.CompareTag("item")) ItemAddFromObject(hit.collider.gameObject);
                if (hit.collider.CompareTag("gameObject")) GameManager.instance.selectObejct = hit.collider.gameObject;
            }
        }

        private void ItemAddFromObject(GameObject objects)
        {
            var id = objects.GetComponent<ObjectItem>();
            InventoryController.instance.addItems(id.itemID);

            Destroy(objects);
        }
    }

}
