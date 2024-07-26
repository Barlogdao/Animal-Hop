using UnityEngine;

public class AnimalProvider : MonoBehaviour
{
    [field:SerializeField] public AnimalConfig[] Configs { get; private set; }

    public AnimalConfig GetConfig(int id)
    {
        return Configs[id];
    }
}
