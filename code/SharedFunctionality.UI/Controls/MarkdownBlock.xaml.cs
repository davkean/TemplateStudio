﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Templates.UI.Converters;
using Microsoft.Templates.UI.Styles;

namespace Microsoft.Templates.UI.Controls
{
    public partial class MarkdownBlock : UserControl
    {
        public MarkdownBlock()
        {
            this.Resources.MergedDictionaries.Add(AllStylesDictionary.GetMergeDictionary());

            var markdown = new Markdown
            {
                DocumentStyle = (Style)this.Resources["DocumentStyle"],
                Heading1Style = (Style)this.Resources["H1Style"],
                Heading2Style = (Style)this.Resources["H2Style"],
                Heading3Style = (Style)this.Resources["H3Style"],
                Heading4Style = (Style)this.Resources["H4Style"],
                LinkStyle = (Style)this.Resources["WtsHyperlink"],
                ImageStyle = (Style)this.Resources["ImageStyle"],
                SeparatorStyle = (Style)this.Resources["SeparatorStyle"],
                AssetPathRoot = Environment.CurrentDirectory,
            };
            this.Resources.Add(nameof(Markdown), markdown);

            var ttfdc = new TextToFlowDocumentConverter { Markdown = markdown };
            this.Resources.Add(nameof(TextToFlowDocumentConverter), ttfdc);

            InitializeComponent();

            CommandBindings.Add(new CommandBinding(NavigationCommands.GoToPage, (sender, e) => SafeNavigate(e.Parameter)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MarkdownBlock), new PropertyMetadata(null));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private void SafeNavigate(object parameter)
        {
            if (parameter is string uri && Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                Process.Start(uri);
            }
        }
    }
}
