using UnityEngine;

public class UpperMenuController : MonoBehaviour
{
    [SerializeField] private GameObject upperMenuSprite;
    [SerializeField] private EmotionController mid;
    
    private GameObject corner;


    public void InitMenu()
    {
        corner = upperMenuSprite;


        corner.transform.localScale = new Vector3(((float) Data.CurrentGameData.ColumSize / 2) - 0.5f, 1, 1);
        Instantiate(corner, new Vector3(0, Data.CurrentGameData.RowSize), Quaternion.identity);
        Instantiate(corner, new Vector3(((float) Data.CurrentGameData.ColumSize / 2) + 0.5f, Data.CurrentGameData.RowSize), Quaternion.identity);
        mid.transform.localScale = Vector3.one;
        Instantiate(mid, new Vector3(((float) Data.CurrentGameData.ColumSize / 2) - 0.5f, Data.CurrentGameData.RowSize, 0), Quaternion.identity);
    }
    



}
