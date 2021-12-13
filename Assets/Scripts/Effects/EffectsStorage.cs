using System;
using System.Collections.Generic;
using Positions;
using Spells.Parts;

namespace Effects
{
    public class EffectsStorage
    {
        private const float HEALTH_DAMAGE_BASE_VALUE = 5f;
        private const float MANA_DAMAGE_BASE_VALUE = 5f;
        private const float STAMINA_DAMAGE_BASE_VALUE = 5f;
        private const float HEALTH_RECOVER_BASE_VALUE = 5f;
        private const float MANA_RECOVER_BASE_VALUE = 5f;
        private const float STAMINA_RECOVER_BASE_VALUE = 5f;

        private static readonly Dictionary<EffectEnum, Action<float, int[]>> EffectsDictionary =
            new Dictionary<EffectEnum, Action<float, int[]>>
            {
                [EffectEnum.Shuffle] = Shuffle,
                [EffectEnum.HealthDrop] = HealthDamage,
                [EffectEnum.HealthRecover] = HealthRecover,
                [EffectEnum.InitDrop] = StaminaDamage,
                [EffectEnum.InitRecover] = StaminaRecover,
                [EffectEnum.ManaDrop] = ManaDamage,
                [EffectEnum.ManaRecover] = ManaRecover,
            };


        public static Action<float, int[]> GetEffectByName(EffectEnum name)
        {
            return EffectsDictionary[name];
        }
        
        public static void HealthDamage(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is Damageable)
                    MakeHealthDamage((Damageable) current, power);
            
        }
        
        public static void HealthRecover(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is Damageable)
                    MakeHealthRecover((Damageable) current, power);
            
        }
        
        public static void ManaDamage(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is GolemBase)
                    MakeManaDamage((GolemBase) current, power);
        }
        
        public static void ManaRecover(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is GolemBase)
                    MakeManaRecover((GolemBase) current, power);
        }
        
        public static void StaminaDamage(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is GolemBase)
                    MakeStaminaDamage((GolemBase) current, power);
        }
        
        public static void StaminaRecover(float power, int[] positions)
        {
            foreach (var current in PositionsHandler.Instance.GetByIndex(positions))
                if (current is GolemBase)
                    MakeStaminaRecover((GolemBase) current, power);
        }
        
        public static void Shuffle(float power, int[] positions)
        { 
            PositionsHandler.Instance.ShufflePositions(positions);
        }

        private static void MakeHealthDamage(Damageable golem, float powerMultiplier) => 
            golem.damage.DecreaseValue(HEALTH_DAMAGE_BASE_VALUE * powerMultiplier);
        
        private static void MakeManaDamage(GolemBase golem, float powerMultiplier) => 
            golem.magic.DecreaseValue(MANA_DAMAGE_BASE_VALUE * powerMultiplier);
        
        private static void MakeStaminaDamage(GolemBase golem, float powerMultiplier) => 
            golem.initiative.DecreaseValue(STAMINA_DAMAGE_BASE_VALUE * powerMultiplier);
        
        private static void MakeHealthRecover(Damageable golem, float powerMultiplier) => 
            golem.damage.IncreaseValue(HEALTH_RECOVER_BASE_VALUE * powerMultiplier);
        
        private static void MakeManaRecover(GolemBase golem, float powerMultiplier) => 
            golem.magic.IncreaseValue(MANA_RECOVER_BASE_VALUE * powerMultiplier);

        private static void MakeStaminaRecover(GolemBase golem, float powerMultiplier) =>
            golem.initiative.IncreaseValue(STAMINA_RECOVER_BASE_VALUE * powerMultiplier);
        
    }
}
