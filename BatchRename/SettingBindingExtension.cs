﻿using System.Windows.Data;

namespace BatchRename
{
    class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Source = BatchRename.Properties.Settings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
}
