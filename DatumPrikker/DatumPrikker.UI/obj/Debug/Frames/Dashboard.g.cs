﻿

#pragma checksum "C:\Users\Developer\Documents\GitHub\SIG-Windows8\DatumPrikker\DatumPrikker.UI\Frames\Dashboard.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1383E0F6890F6404E2312A1F87B57DCF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatumPrikker.UI.Frames
{
    partial class Dashboard : global::DatumPrikker.UI.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 48 "..\..\Frames\Dashboard.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.AdressBookItems_SelectionChanged_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 32 "..\..\Frames\Dashboard.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoHome;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 33 "..\..\Frames\Dashboard.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.btnRequests_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 36 "..\..\Frames\Dashboard.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.btnAddressBook_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


