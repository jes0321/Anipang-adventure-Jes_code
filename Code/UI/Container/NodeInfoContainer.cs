using System;
using Code.CoreSystem;
using Code.UI;
using GondrLib.Dependencies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.JES.Code.Event;
using Work.JES.Code.Items;
using Work.JES.Code.Notes;
using Work.JES.Code.Skills;

namespace Work.JES.Code.UI.Container
{
    public class NodeInfoContainer : MonoBehaviour, IUIElement<NoteDataSo>
    {
        [SerializeField] private TextMeshProUGUI descriptionText, dropOriginText;
        [Inject] private RotatePoolObject _rotatePoolObject;

        [Header("Item Info")] [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemNameText;

        [Header("Skill info")] [SerializeField]
        private TextMeshProUGUI skillNameText;

        [SerializeField] private TextMeshProUGUI skillDescriptionText;

        public void EnableFor(NoteDataSo item)
        {
            if (item.IsFind == false)
            {
                _rotatePoolObject.DestroyObj();
                itemImage.color = Color.clear;
                itemNameText.text = "??????";
                descriptionText.text = "??????";
                dropOriginText.text = "??????";
                //skillNameText.text = "??????";
                //skillDescriptionText.text = "??????";
                return;
            }

            AnipangDataSO anipangData = item as AnipangDataSO;

            _rotatePoolObject.SetObj(anipangData);


            ItemDataSO itemData = anipangData.dropItemData;
            SkillDataSO skillData = anipangData.SkillData;

            itemImage.color = Color.white;
            itemImage.sprite = itemData.Icon;
            itemNameText.text = itemData.Name;

            descriptionText.text = item.Description;
            dropOriginText.text = item.GetDropOrgin();

            //skillNameText.text = skillData.Name;
            //skillDescriptionText.text = skillData.Description;
        }

        public void Disable()
        {
            _rotatePoolObject.DestroyObj();
        }
    }
}