using UnityEngine;

public class AlienadeCount : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.GetComponent<UnityEngine.UI.Text>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        float aCount = 0;
        foreach (ItemList i in PlayerStats.instance.items)
        {
            if (i.item.name == "Alienade")
            {
                aCount = i.stacks;
            }
        }
        this.transform.GetComponent<UnityEngine.UI.Text>().text = aCount.ToString();
    }
}
