/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using EasingCore;


namespace FancyScrollView.Example07
{
    class Cell : FancyScrollRectCell<ItemData, Context>
    {
        [SerializeField] private ScrollView m_ScrollView;
        [SerializeField] Text message = default;
        [SerializeField] Image image = default;
        [SerializeField] Button button = default;
        private const int DataCount = 20;

        public override void Initialize()
        {
            button.onClick.AddListener(() => OnClickButton(Index));
        }

        private void OnClickButton(int index)
        {
            GenerateCells();
        }

        public void GenerateCells()
        {
            var items = Enumerable.Range(0, DataCount)
                .Select(i => new ItemData($"Cell {i}"))
                .ToArray();
            m_ScrollView.UpdateData(items);
            SelectCell();
        }

        private void SelectCell()
        {
            m_ScrollView.ScrollTo(Index, 0.3f, Ease.InOutQuint, Alignment.Middle);
        }


        public override void UpdateContent(ItemData itemData)
        {
            message.text = itemData.Message;

            var selected = Context.SelectedIndex == Index;
            image.color = selected
                ? new Color32(0, 255, 255, 100)
                : new Color32(255, 255, 255, 77);
        }

        protected override void UpdatePosition(float normalizedPosition, float localPosition)
        {
            base.UpdatePosition(normalizedPosition, localPosition);

            var wave = Mathf.Sin(normalizedPosition * Mathf.PI * 2) * 65;
            transform.localPosition += Vector3.right * wave;
        }
    }
}