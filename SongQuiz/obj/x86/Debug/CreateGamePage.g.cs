﻿#pragma checksum "C:\Users\gxsha\OneDrive\documents\visual studio 2017\Projects\SongQuiz\SongQuiz\CreateGamePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "973FF0370F6DDA4453A226E3EDE35941"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SongQuiz
{
    partial class CreateGamePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
        };

        private class CreateGamePage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ICreateGamePage_Bindings
        {
            private global::SongQuiz.CreateGamePage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.Button obj6;
            private global::Windows.UI.Xaml.Controls.Button obj7;
            private global::Windows.UI.Xaml.Controls.Button obj8;

            public CreateGamePage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 6:
                        this.obj6 = (global::Windows.UI.Xaml.Controls.Button)target;
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += (global::System.Object param0, global::Windows.UI.Xaml.RoutedEventArgs param1) =>
                        {
                        this.dataRoot.SelectSongClick();
                        };
                        break;
                    case 7:
                        this.obj7 = (global::Windows.UI.Xaml.Controls.Button)target;
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += (global::System.Object param0, global::Windows.UI.Xaml.RoutedEventArgs param1) =>
                        {
                        this.dataRoot.CancelClick();
                        };
                        break;
                    case 8:
                        this.obj8 = (global::Windows.UI.Xaml.Controls.Button)target;
                        ((global::Windows.UI.Xaml.Controls.Button)target).Click += (global::System.Object param0, global::Windows.UI.Xaml.RoutedEventArgs param1) =>
                        {
                        this.dataRoot.NextClick();
                        };
                        break;
                    default:
                        break;
                }
            }

            // ICreateGamePage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // CreateGamePage_obj1_Bindings

            public void SetDataRoot(global::SongQuiz.CreateGamePage newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.generalSettingsGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 16 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.generalSettingsGrid).GotFocus += this.generalSettingsGrid_GotFocus;
                    #line 17 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.generalSettingsGrid).LostFocus += this.generalSettingsGrid_LostFocus;
                    #line default
                }
                break;
            case 3:
                {
                    this.songSettingsGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    #line 67 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.songSettingsGrid).GotFocus += this.songSettings_GotFocus;
                    #line 68 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Grid)this.songSettingsGrid).LostFocus += this.songSettings_LostFocus;
                    #line default
                }
                break;
            case 4:
                {
                    this.mediaElement = (global::Windows.UI.Xaml.Controls.MediaElement)(target);
                }
                break;
            case 5:
                {
                    this.songCounter = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.songPicker = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.Button element7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 8:
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 9:
                {
                    this.wrongAnswer3 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10:
                {
                    this.wrongAnswer2 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11:
                {
                    this.wrongAnswer1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12:
                {
                    this.rightAnswer = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13:
                {
                    this.timeSelector = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 115 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)this.timeSelector).ValueChanged += this.timeSelector_ValueChanged;
                    #line default
                }
                break;
            case 14:
                {
                    this.timeInterval = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    #line 62 "..\..\..\CreateGamePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Slider)this.timeInterval).ValueChanged += this.timeInterval_ValueChanged;
                    #line default
                }
                break;
            case 15:
                {
                    this.QuizName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    CreateGamePage_obj1_Bindings bindings = new CreateGamePage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}
