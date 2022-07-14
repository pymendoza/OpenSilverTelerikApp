using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Html;

namespace OpenSilverTelerikApp
{
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            var formatProvider = new HtmlFormatProvider();
            formatProvider.ImportSettings = new HtmlImportSettings();
            formatProvider.ImportSettings.UseDefaultStylesheetForFontProperties = true;
            var result = formatProvider.Import("<p style=\"margin-top: 0px;margin-bottom: 0px;line-height: 1;\"><span style=\"font-family: \'Verdana\';font-size: 11px;color: #606060;\">Repton Projects Limited is a Project Management consultancy based in Nottingham, England. </span></p><p style=\"margin-top: 0px;margin-bottom: 0px;line-height: 1;\"><span style=\"font-family: \'Verdana\';font-size: 11px;color: #606060;\">&nbsp;</span></p><p style=\"margin-top: 0px;margin-bottom: 0px;line-height: 1;\"><span style=\"font-family: \'Verdana\';font-size: 11px;color: #606060;\">Over recent years Repton Projects\' business focus has undergone significant changes to inculde the provision of a full Project Management toolset (Project on Demand). </span></p>");

            this.DataContext = this;
            this.InitializeComponent();
            //Label = "I am bound to the Label property";
            Label = "<p>This is a paragraph!</p>";
            extTb.RichText = "<p>This is a paragraph!</p>";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string label = string.Empty;
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                RaisePropertyChangedEvent("Label");
            }
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
