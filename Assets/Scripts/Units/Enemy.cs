using System;
using Core;
using UnityEngine;

namespace Units
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        protected GameObject deathEffect;
        [SerializeField]
        protected float deathEffectLifeTime = 0;

        public Material outlineMaterial;

        private Material defaultMaterial;

        private SpriteRenderer _renderer;

        protected void Start()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public virtual void Die()
        {
            InstantiateDeathEffect();
        }


        protected virtual void InstantiateDeathEffect()
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectLifeTime);
        }

        protected virtual void Dispose()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 3f);
        }


        public virtual void SelectedAsTarget()
        {
            defaultMaterial = _renderer.material;
            _renderer.material = outlineMaterial;
        }

        public virtual void ReleaseAsTarget()
        {
            _renderer.material = defaultMaterial;
            defaultMaterial = null;
        }
    }
}