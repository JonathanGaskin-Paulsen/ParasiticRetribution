using UnityEngine;
using TMPro;
public class textcontroller : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextAsset fileName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = fileName.text;
    }

}
