using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

