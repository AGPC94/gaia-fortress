using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllies : MonoBehaviour
{
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void OnMouseDown()
    {
        GameObject allyGo = GameManager.instance.allySelected;

        if (allyGo != null)
        {
            Ally ally = allyGo.GetComponent<Ally>();

            if (GameManager.instance.recoursePoints >= ally.RecoursePoints && ally.Btn.IsReady)
            {
                Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPosition.z = 0;

                Instantiate(allyGo, mouseWorldPosition, Quaternion.identity);
                GameManager.instance.recoursePoints -= ally.RecoursePoints;
                ally.Btn.StartCooldown();
            }

        }
    }
}
