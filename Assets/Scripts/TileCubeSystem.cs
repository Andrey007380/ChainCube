using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileCubeSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] tmpText;
    [SerializeField] private GameObject connectEff;
    [SerializeField] private Color[] colors;
    
    private int _value = 0;
    private MeshRenderer _renderer;
    private readonly int[] _valuesRange = {2, 4, 8};

    public static Action<int> OnScoreChange;
    public static Action<GameObject> OnCollision;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        tmpText = GetComponentsInChildren<TextMeshProUGUI>();
    }
    
    private void OnEnable()
    { 
       int rand = Random.Range(0, _valuesRange.Length);
       ChangeValue(_valuesRange[rand]);
       ChangeColor();
    }
    
    private void ChangeValue(int multiplier)
    {
        _value = multiplier;
        foreach (var textMeshProUGUI in tmpText)
        {
            textMeshProUGUI.text = _value.ToString();
            ChangeColor();
        }
    }
    
    private void ChangeColor()
    {
        int result;
        for (int i = 0; Math.Pow(2, i) != _value ; i++)
        {
            result = i;
            _renderer.material.color = colors[result];
        }
    }
    
    private void OnCollisionEnter(Collision other)
    { 
        if (other.collider.GetComponent<TileCubeSystem>() == null) 
            return; 
        var values = other.collider.GetComponent<TileCubeSystem>()._value;
        
        if (values != GetComponent<TileCubeSystem>()._value) 
             return;
        
        OnCollision?.Invoke(gameObject);
        
        MultiplyCollidedObjects(values);
        Pool.Instance.AddToPool(1, other.gameObject);

        Instantiate(connectEff, transform.position, Quaternion.identity);
        OnScoreChange?.Invoke(MultiplyCollidedObjects(values));
    }
    
    private int MultiplyCollidedObjects(int value)
    {
        value *= 2;
        ChangeValue(value);
        return value;
    }
    
}
