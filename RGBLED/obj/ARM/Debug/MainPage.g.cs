﻿#pragma checksum "C:\Users\Willie\Desktop\RGBLED\RGBLED\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7E38042A2ACBBE951E0C3BF02409A179"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RGBLED
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.LED1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.LED2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.LED3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4:
                {
                    this.LED4 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.LED5 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.textbox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 51 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.textbox).TextChanged += this.TextBox_TextChanged;
                    #line default
                }
                break;
            case 7:
                {
                    this.textbox1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 52 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.textbox1).TextChanged += this.TextBox_TextChanged_1;
                    #line default
                }
                break;
            case 8:
                {
                    this.textbox2 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 53 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.textbox2).TextChanged += this.TextBox_TextChanged_2;
                    #line default
                }
                break;
            case 9:
                {
                    this.textbox3 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 54 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.textbox3).TextChanged += this.TextBox_TextChanged_3;
                    #line default
                }
                break;
            case 10:
                {
                    this.textbox4 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 55 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.textbox4).TextChanged += this.textbox4_TextChanged;
                    #line default
                }
                break;
            case 11:
                {
                    this.textblock1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 56 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.textblock1).SelectionChanged += this.TextBlock_SelectionChanged;
                    #line default
                }
                break;
            case 12:
                {
                    this.textblock3 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 57 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.textblock3).SelectionChanged += this.TextBlock_SelectionChanged_1;
                    #line default
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
            return returnValue;
        }
    }
}

