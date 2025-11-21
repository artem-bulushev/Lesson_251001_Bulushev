using UnityEngine;

namespace Code
{
    public class FallingUICoins : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private RectTransform _spawnArea;
        [SerializeField] private float _fallSpeed = 200f;
        [SerializeField] private float _spawnRate = 0.5f;
        [SerializeField] private int _maxCoins = 20;

        [Header("Rotation")]
        [SerializeField] private bool _rotateCoins = true;
        [SerializeField] private float _rotationSpeed = 180f;

        private float spawnTimer;
        private int currentCoins;

        private void Update()
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= _spawnRate && currentCoins < _maxCoins)
            {
                SpawnCoin();
                spawnTimer = 0f;
            }
        }

        private void SpawnCoin()
        {
            GameObject coin = Instantiate(_coinPrefab, _spawnArea);
            coin.transform.SetAsFirstSibling(); 
            currentCoins++;

            RectTransform coinRect = coin.GetComponent<RectTransform>();
            float randomX = Random.Range(0, _spawnArea.rect.width);
            coinRect.anchoredPosition = new Vector2(randomX, _spawnArea.rect.height);

            StartCoroutine(MoveCoin(coin));
        }

        private System.Collections.IEnumerator MoveCoin(GameObject coin)
        {
            RectTransform rectTransform = coin.GetComponent<RectTransform>();

            while (rectTransform != null &&
                   rectTransform.anchoredPosition.y > -rectTransform.rect.height)
            {
                rectTransform.anchoredPosition -= new Vector2(0, _fallSpeed * Time.deltaTime);

                if (_rotateCoins)
                {
                    rectTransform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
                }

                yield return null;
            }

            if (coin != null)
            {
                Destroy(coin);
                currentCoins--;
            }
        }
    }
}