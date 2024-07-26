using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] int maxPosX = 5;
    [SerializeField] MeshRenderer _renderer;
    [SerializeField] GameData _data;
    [SerializeField] ParticleSystem _splatFx;
    [SerializeField] private LeanTweenType _ease;
    [SerializeField] private Transform _goldPrefab;
    [SerializeField, Range(0f, 1f)] private float _spawnGoldChance;

    [SerializeField] private Transform _savePoint;

    public Vector3 SavePoint => _savePoint.position;


    private bool _hasGold = false;
    private Transform _gold = null;

    Vector3 childPos;

    public static event Action OnCollideWithPlayer;
    public static event Action OnGoldCollected;

    private void Start()
    {

        _renderer.material = _data.GetRandomMaterial;

        childPos = transform.GetChild(0).transform.localPosition;
        childPos.x = UnityEngine.Random.Range(-maxPosX, maxPosX+1);
        transform.GetChild(0).transform.localPosition = childPos;
        SpawnGold();

        LeanTween.moveLocalY(_renderer.transform.gameObject, -.5f, .5f).setEase(_ease);
    }

    private void SpawnGold()
    {
        if (Random.value <= _spawnGoldChance)
        {
            _hasGold = true;
            Vector3 position = transform.position + Vector3.up * 3;
            position.x = childPos.x;
            _gold = Instantiate(_goldPrefab, position, Quaternion.Euler(-90f,0f,0f), transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).normal.y != -1f) return;

        if (collision.collider.CompareTag("Player"))
        {
            OnCollideWithPlayer?.Invoke();

            if (_hasGold)
            {
                OnGoldCollected?.Invoke();
                Destroy(_gold.gameObject);
                _hasGold = false;
            }

            _splatFx.transform.position = collision.GetContact(0).point + (Vector3.up * .1f);
            _splatFx.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}