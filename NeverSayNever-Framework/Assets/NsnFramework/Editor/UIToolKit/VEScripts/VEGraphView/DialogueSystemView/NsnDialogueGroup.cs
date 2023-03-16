using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueGroup : NsnBaseGroup
    {
        public string ID { get; set; }

        public string PreTitle { get; set; }

        private Color m_DefaultBorderColor;
        private float m_DefaultBorderWidth;

        public NsnDialogueGroup(string groupTitle, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();
            title = groupTitle;
            PreTitle = groupTitle;

            SetPosition(new Rect(position, Vector2.zero));

            m_DefaultBorderColor = contentContainer.style.borderBottomColor.value;
            m_DefaultBorderWidth = contentContainer.style.borderBottomWidth.value;

            float bgColorV = 100 / 255f;
            contentContainer.style.backgroundColor = new Color(bgColorV, bgColorV, bgColorV, 0.5f);

            var titleContainer = contentContainer.Q<VisualElement>("titleContainer");
            float bgTitleColorV = 39 / 255f;
            titleContainer.style.backgroundColor = new Color(bgTitleColorV, bgTitleColorV, bgTitleColorV, 1);
        }


        public void SetErrorStyle(Color color, float width = 2f)
        {
            contentContainer.style.borderBottomColor = color;
            contentContainer.style.borderBottomWidth = width;
        }

        public void ResetStyle()
        {
            contentContainer.style.borderBottomColor = m_DefaultBorderColor;
            contentContainer.style.borderBottomWidth = m_DefaultBorderWidth;
        }
    }
}

