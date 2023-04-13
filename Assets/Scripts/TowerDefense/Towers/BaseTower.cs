using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Base Tower - cache attributes for level up
    /// Tower upgrades all attributes at once, but could have individual upgrades by having individual buttons and hooks
    /// </summary>
    public abstract class BaseTower : MonoBehaviour
    {
        [SerializeField] protected TowerDefinition _towerDefinition;
        [SerializeField] protected LayerMask _detectionLayer;
        [SerializeField] protected int _detectionCap;
        
        //common attributes separated to have more quick access in the specialization classes
        protected TowerAttributesDTO _damage;
        protected TowerAttributesDTO _speed;
        protected TowerAttributesDTO _range;
        protected TowerAttributesDTO _special;
        protected Dictionary<string, TowerAttributesDTO> _otherAttributes;

        private void Start()
        {
            LoadAttributes();
        }

        protected virtual void LoadAttributes()
        {
            _damage = new TowerAttributesDTO(_towerDefinition.Damage);
            _speed = new TowerAttributesDTO(_towerDefinition.Speed);
            _range = new TowerAttributesDTO(_towerDefinition.Range);
            _special = new TowerAttributesDTO(_towerDefinition.Special);
            
            if (_otherAttributes == null) return;
            _otherAttributes = new Dictionary<string, TowerAttributesDTO>();
            foreach (var definition in _towerDefinition.OtherAttributes)
            {
                TowerAttributesDTO attributesDto = new TowerAttributesDTO(definition);
                _otherAttributes.Add(definition.StatName, attributesDto);
            }
        }
        
        //Called when upgrade button is activated
        public virtual void OnUpgradeEvent()
        {
            _damage.LevelUp();
            _speed.LevelUp();
            _range.LevelUp();
            _special.LevelUp();
            if (_otherAttributes == null) return;
            foreach (var towerAttributesDto in _otherAttributes)
            {
                towerAttributesDto.Value.LevelUp();
            }
        }
        
        
    }
}