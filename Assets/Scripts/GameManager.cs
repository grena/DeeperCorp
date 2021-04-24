using Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
    public static GameManager Singleton => _singleton;
    
    public Stock Stock;
    public RefOperationLoader RefOperationLoader;
    
    private void Awake()
    {
        if (_singleton != null && _singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _singleton = this;
        }
        
        Initialize();
    }
    
    private void Initialize()
    {
        RefOperationLoader = new RefOperationLoader();
        RefOperationLoader.LoadData();
        
        Stock = new Stock()
        {
            Caps = 100,
            Roots = 100,
        };
    }
}
