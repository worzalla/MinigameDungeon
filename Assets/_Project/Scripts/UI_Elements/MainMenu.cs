using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : VisualElement
{
    VisualElement m_RootElement;
    VisualTreeAsset m_ModulesVisualTree;

    public void Start()
    {
        m_RootElement = new VisualElement();
        m_ModulesVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/_Project/UI_Elements/MainMenu.uxml"
            );

    }

}

class StatusBar : VisualElement
{
    public new class UxmlFactory : UxmlFactory<StatusBar> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription m_Status = new UxmlStringAttributeDescription { name = "status" };
        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            
            get 
            { 
                yield break; // We do not accept children
                // yield return new UxmlChildElementDescription(typeof(VisualElement)); // We accept children of any type
                // yield return new UxmlChildElementDescription(typeof(StatusBar)); // We accept children of type status bar
            }
        }
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            ((StatusBar)ve).status = m_Status.GetValueFromBag(bag, cc);
        }
    }
    public StatusBar()
    {
        m_status = string.Empty;
    }

    string m_status;
    public string status { get; set; }
}