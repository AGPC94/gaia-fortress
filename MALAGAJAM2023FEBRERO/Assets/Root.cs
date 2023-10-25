using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] float growTime;
    [SerializeField] float progress;
    [SerializeField] float max;

    public float Progress { get => progress; set => progress = value; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        CheckRoots();
    }

    void CheckRoots()
    {
        RootPart[] parts = transform.GetComponentsInChildren<RootPart>(true);

        for (int i = 0; i < parts.Length; i++)
        {
            if (i < progress)
                parts[i].gameObject.SetActive(true);
            else
                parts[i].gameObject.SetActive(false);
        }
    }

    IEnumerator Grow()
    {
        while (true)
        {
            yield return new WaitForSeconds(growTime);
            if (progress != max)
                progress++;
        }
    }


}
