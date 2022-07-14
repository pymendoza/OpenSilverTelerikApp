using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Model.Styles;

namespace OpenSilverTelerikApp.Controls
{
    public class ExtendedRadRichTextBlock : RadRichTextBox
    {
        private HtmlFormatProvider formatProvider;
        public ExtendedRadRichTextBlock() : base()
        {
            //this.DefaultStyleKey = typeof(ExtendedRadRichTextBlock);
            formatProvider = new HtmlFormatProvider();
            formatProvider.ImportSettings = new HtmlImportSettings();
            formatProvider.ImportSettings.UseDefaultStylesheetForFontProperties = true;
            this.DefaultStyleKey = typeof(ExtendedRadRichTextBlock);

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.IsSpellCheckingEnabled = false;

            this.DocumentInheritsDefaultStyleSettings = true;
            this.DefaultStyleSettings.ParagraphProperties.LineSpacing = 1;
            this.DefaultStyleSettings.ParagraphProperties.AutomaticSpacingAfter = false;
            this.DefaultStyleSettings.ParagraphProperties.AutomaticSpacingBefore = false;
            this.DefaultStyleSettings.ParagraphProperties.SpacingAfter = 0;
            this.DefaultStyleSettings.ParagraphProperties.SpacingBefore = 0;

            this.DefaultStyleSettings.SpanProperties.ForeColor = ((SolidColorBrush)this.Foreground).Color;
            this.DefaultStyleSettings.SpanProperties.FontFamily = this.FontFamily;
            this.DefaultStyleSettings.SpanProperties.FontSize = this.FontSize;
        }

        public string RichText
        {
            get { return (string)GetValue(RichTextProperty); }
            set { SetValue(RichTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty = DependencyProperty.Register("RichText", typeof(string), typeof(ExtendedRadRichTextBlock), new PropertyMetadata("", RichTextPropertyChangedCallback));
        private static void RichTextPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ExtendedRadRichTextBlock control = (ExtendedRadRichTextBlock)sender;

            control.Document.ParagraphDefaultSpacingAfter = 0;
            control.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                if (e.NewValue == null)
                    control.Document = control.formatProvider.Import(string.Empty);
                else
                    control.Document = control.formatProvider.Import((string)e.NewValue);

                StyleDefinition possibleWebStyle = control.Document.StyleRepository.FirstOrDefault(x => x.Name == RadDocumentDefaultStyles.NormalWebStyleName);
                if (possibleWebStyle != null)
                {
                    possibleWebStyle.ParagraphProperties.AutomaticSpacingBefore = false;
                    possibleWebStyle.ParagraphProperties.AutomaticSpacingAfter = false;
                    possibleWebStyle.ParagraphProperties.SpacingBefore = 0;
                    possibleWebStyle.ParagraphProperties.SpacingAfter = 0;
                    control.UpdateEditorLayout();
                }
            }));
        }
    }
}
