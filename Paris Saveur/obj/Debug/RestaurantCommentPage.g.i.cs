﻿

#pragma checksum "C:\Users\Zelong\Documents\Code\Windows Phone\Paris Saveur\Paris Saveur\RestaurantCommentPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B8BFDF86E88D607FD7B262D5E8E9B656"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paris_Saveur
{
    partial class RestaurantCommentPage : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.DataTemplate restaurantCommentListItemTemplate; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ListView restaurantCommentList; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button loadMoreButoon; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ProgressBar LoadingBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///RestaurantCommentPage.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            restaurantCommentListItemTemplate = (global::Windows.UI.Xaml.DataTemplate)this.FindName("restaurantCommentListItemTemplate");
            restaurantCommentList = (global::Windows.UI.Xaml.Controls.ListView)this.FindName("restaurantCommentList");
            loadMoreButoon = (global::Windows.UI.Xaml.Controls.Button)this.FindName("loadMoreButoon");
            LoadingBar = (global::Windows.UI.Xaml.Controls.ProgressBar)this.FindName("LoadingBar");
        }
    }
}



