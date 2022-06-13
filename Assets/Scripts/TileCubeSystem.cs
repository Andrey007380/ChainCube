using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileCubeSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] tmpText;
    [SerializeField] private GameObject connectEff;
        
    private int _value;
    
    [SerializeField] private Color[] colors;
    
    public static Action<int> OnScoreChange;
    public static Action<GameObject> OnCollision;

    private MeshRenderer _renderer;

    private readonly int[] _valuesRange = {2, 4, 8, 16};
    
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
        String firstPartOfColor = "0.";
        String secondPartOfColor = _value.ToString();
        var colorMultiplier = 6;
        var result = firstPartOfColor + secondPartOfColor;
        float colorValue = float.Parse(result);

        _renderer.material.color = new Color(colorValue * colorMultiplier , colorValue, colorValue);

    }
    
    private void OnCollisionEnter(Collision other)
    { 
        if (other.collider.GetComponent<TileCubeSystem>() == null) 
            return; 
        var values = other.collider.GetComponent<TileCubeSystem>()._value;
        
        if (values != GetComponent<TileCubeSystem>()._value) 
             return;
        
        OnCollision?.Invoke(gameObject);
        
        MultiplyCollidedObjects(values, transform);
        Pool.Instance.AddToPool(1, other.gameObject);

        Instantiate(connectEff, transform.position, Quaternion.identity);
        OnScoreChange?.Invoke(MultiplyCollidedObjects(values, transform));
    }
    
    private int MultiplyCollidedObjects(int value, Component changedObject)
    {
        value *= 2;
        changedObject.GetComponent<TileCubeSystem>().ChangeValue(value);
        return value;
    }
    
}
