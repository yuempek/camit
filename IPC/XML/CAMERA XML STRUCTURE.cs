﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4963
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace IPCFileFormat
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Cameras
    {

        private CamerasCamera[] cameraField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Camera")]
        public CamerasCamera[] Camera
        {
            get
            {
                return this.cameraField;
            }
            set
            {
                this.cameraField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCamera
    {

        private CamerasCameraModelInfo modelInfoField;

        private string nameField;

        private string ipField;

        private string userNameField;

        private string passwordField;

        private CamerasCameraStamp[] stampsField;

        private int idField;

        private int orderField;

        /// <remarks/>
        public CamerasCameraModelInfo ModelInfo
        {
            get
            {
                return this.modelInfoField;
            }
            set
            {
                this.modelInfoField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Ip
        {
            get
            {
                return this.ipField;
            }
            set
            {
                this.ipField = value;
            }
        }

        /// <remarks/>
        public string UserName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Stamp", IsNullable = false)]
        public CamerasCameraStamp[] Stamps
        {
            get
            {
                return this.stampsField;
            }
            set
            {
                this.stampsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Order
        {
            get
            {
                return this.orderField;
            }
            set
            {
                this.orderField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfo
    {

        private string modelField;

        private string streamTypeField;

        private string sourceUrlField;

        private CamerasCameraModelInfoControls controlsField;

        /// <remarks/>
        public string Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        public string StreamType
        {
            get
            {
                return this.streamTypeField;
            }
            set
            {
                this.streamTypeField = value;
            }
        }

        /// <remarks/>
        public string SourceUrl
        {
            get
            {
                return this.sourceUrlField;
            }
            set
            {
                this.sourceUrlField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControls Controls
        {
            get
            {
                return this.controlsField;
            }
            set
            {
                this.controlsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControls
    {

        private CamerasCameraModelInfoControlsStandartControls standartControlsField;

        private CamerasCameraModelInfoControlsControl[] additionalControlsField;

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControls StandartControls
        {
            get
            {
                return this.standartControlsField;
            }
            set
            {
                this.standartControlsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Control", IsNullable = false)]
        public CamerasCameraModelInfoControlsControl[] AdditionalControls
        {
            get
            {
                return this.additionalControlsField;
            }
            set
            {
                this.additionalControlsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControls
    {

        private CamerasCameraModelInfoControlsStandartControlsUP upField;

        private CamerasCameraModelInfoControlsStandartControlsDown downField;

        private CamerasCameraModelInfoControlsStandartControlsLeft leftField;

        private CamerasCameraModelInfoControlsStandartControlsRight rightField;

        private CamerasCameraModelInfoControlsStandartControlsZoomIn zoomInField;

        private CamerasCameraModelInfoControlsStandartControlsZoomOut zoomOutField;

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsUP Up
        {
            get
            {
                return this.upField;
            }
            set
            {
                this.upField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsDown Down
        {
            get
            {
                return this.downField;
            }
            set
            {
                this.downField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsLeft Left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsRight Right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsZoomIn ZoomIn
        {
            get
            {
                return this.zoomInField;
            }
            set
            {
                this.zoomInField = value;
            }
        }

        /// <remarks/>
        public CamerasCameraModelInfoControlsStandartControlsZoomOut ZoomOut
        {
            get
            {
                return this.zoomOutField;
            }
            set
            {
                this.zoomOutField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsUP
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsDown
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsLeft
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsRight
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsZoomIn
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsStandartControlsZoomOut
    {

        private string startEventField;

        private string stopEventField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartEvent
        {
            get
            {
                return this.startEventField;
            }
            set
            {
                this.startEventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StopEvent
        {
            get
            {
                return this.stopEventField;
            }
            set
            {
                this.stopEventField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraModelInfoControlsControl
    {

        private string nameField;

        private string urlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CamerasCameraStamp
    {

        private bool activeField;

        private int positionField;

        private bool showNameField;

        private bool showDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool active
        {
            get
            {
                return this.activeField;
            }
            set
            {
                this.activeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool showName
        {
            get
            {
                return this.showNameField;
            }
            set
            {
                this.showNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool showDate
        {
            get
            {
                return this.showDateField;
            }
            set
            {
                this.showDateField = value;
            }
        }
    }
}