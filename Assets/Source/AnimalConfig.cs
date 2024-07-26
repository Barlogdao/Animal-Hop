using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal", order = 1)]
public class AnimalConfig : ScriptableObject
{
    [SerializeField] private string _name;

    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public int Price { get; private set; } = 0;
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Mesh Mesh { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
}
