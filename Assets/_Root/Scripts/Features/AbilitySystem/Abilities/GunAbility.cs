using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;

namespace Features.AbilitySystem.Abilities
{
    internal class GunAbility : IAbility
    {
        private readonly IAbilityItem _abilityItem;


        public GunAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));


        public void Apply(IAbilityActivator activator)
        {
            var projectile = Addressables.InstantiateAsync(_abilityItem.Projectile).Result.GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _abilityItem.Value;
            projectile.AddForce(force, ForceMode2D.Force);
        }
    }
}
