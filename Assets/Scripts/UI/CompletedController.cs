using TMPro;
using UnityEngine;

namespace UI
{
    public class CompletedController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI manaCost;

        public void SetResources(string titleName, float manaCostNumber)
        {
            title.text = titleName;
            manaCost.text = ((int)manaCostNumber).ToString();
        }
    }
}
