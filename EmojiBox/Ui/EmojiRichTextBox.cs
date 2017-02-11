using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EmojiBox.Ui
{
	public class EmojiRichTextBox : RichTextBox
    {
        bool readingInput = false;
        string input = "";
        EmojiParser ep = new EmojiParser();
        TextPointer inputStart = null;

        public EmojiRichTextBox()
        {
            this.Loaded += EmojiRichTextBox_Loaded;
        }

        private void EmojiRichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.PreviewTextInput += EmojiRichTextBox_PreviewTextInput;
            this.PreviewKeyDown += EmojiRichTextBox_KeyDown;
        }

		private void EmojiRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                // Backspace

                if (readingInput)
                {
                    if (input.Length == 0)
                    {
                        readingInput = false;
                        //input = "";
                        Debug.Print("Stop reading");
                    }
                    else
                    {
                        input = input.Substring(0, input.Length - 1);
                        Debug.Print(input);
                    }
                }
            }
            else if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                // Space / Enter

                readingInput = false;
                input = "";
                Debug.Print("Stop reading");
            }
        }

        private void EmojiRichTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (readingInput)
            {
                if (e.Text == ":")
                {
                    readingInput = false;
                    Debug.Print("Stop reading");
                    e.Handled = InsertEmoji(input.ToLowerInvariant(), inputStart, CaretPosition);
                    input = "";

                }
                else if (e.Text == " ")
                {
                    readingInput = false;
                    input = "";
                    Debug.Print("Stop reading");
                }
                else
                {
                    input += e.Text;
                }

                Debug.Print(input);
            }
            else if (e.Text == ":")
            {
                readingInput = true;
				inputStart = CaretPosition;
                Debug.Print("Start reading");
            }
        }

        /// <summary>
        /// Inserts an emoji image within the rich text box, replacing the text between the start and end locations.
        /// </summary>
        /// <param name="emojiName">The name of the emoji to insert.</param>
        /// <param name="startLoc">The position at which text should start to be replaced from.</param>
        /// <param name="endLoc">The position at which text should end being replaced.</param>
        /// <returns>Returns true if successful.</returns>
        public bool InsertEmoji(string emojiName, TextPointer startLoc, TextPointer endLoc)
        {
            TextRange range = new TextRange(startLoc, endLoc);

            Debug.Write("Insert \"" + emojiName + "\" at loc " + (startLoc.DocumentStart.GetOffsetToPosition(startLoc)));

            BitmapImage bim = null;

            try
            {
                bim = ep.GetEmojiWithName(emojiName);
            }
            catch (ArgumentException)
            {
                return false;
            }

			// When the TextBox is completely empty, Parent is FlowDocument
            if (startLoc.Parent is Run || startLoc.Parent is FlowDocument)
            {
                dynamic r = startLoc.Parent as Run;
				if (r == null)
					r = startLoc.Parent as FlowDocument;

				// Re-arrange text
                Run runBefore = new Run(new TextRange(r.ContentStart, range.Start).Text);
                Run runAfter = new Run(new TextRange(range.End, r.ContentEnd).Text);

                // Create image item
                double size = r.FontSize;
                Image img = new Image();
                img.Source = bim;
                img.UseLayoutRounding = true;
                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);
                img.Height = size;
                img.Width = size;

                // Insert into text
                if (range.Start.Paragraph != null)
                {
                    Paragraph p = range.Start.Paragraph;

					// Entering into a completely empty TextBox, causes more runs to be created idk
					Run lastInline = p.Inlines.LastInline as Run;

					if (lastInline != null && lastInline.Text.EndsWith(emojiName))
						p.Inlines.Remove(lastInline);

					// Remove emoji text from new inline
					if (runBefore.Text.EndsWith(emojiName))
						runBefore.Text = runBefore.Text.Substring(0, runBefore.Text.Length - (emojiName.Length + 1));

					p.Inlines.Add(runBefore);
                    p.Inlines.Add(img);
                    p.Inlines.Add(runAfter);
				}
                else
                {
                    CaretPosition = runAfter.ContentEnd;
                    return false;
                }

                CaretPosition = runAfter.ContentEnd;

                return true;
            }

            return false;
        }

    }
}
