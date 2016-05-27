using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EmojiBox;

namespace EmojiBoxSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ep = new EmojiParser();
        }

        EmojiParser ep;

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in ep.ListEmojisWithUnicodes())
            {
                TextBlock tb = new TextBlock();
                tb.Text = item;
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.VerticalAlignment = VerticalAlignment.Top;
                tb.Tag = item.Substring(0, item.IndexOf(' '));
                tb.Focusable = true;
                tb.KeyUp += Tb_KeyUp;
                tb.MouseUp += Tb_MouseUp;

                emojiList.Children.Add(tb);
            }
        }

        private void Tb_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                txtEmojiName.Text = (sender as TextBlock).Tag as string;
            }
            //throw new NotImplementedException();
        }

        private void Tb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Space)
            {
                txtEmojiName.Text = (sender as TextBlock).Tag as string;
            }
            //throw new NotImplementedException();
        }

        private void btnSearchName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emojiImage.Source = ep.GetEmojiWithName(txtEmojiName.Text);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("There is no Emoji by the name \"" + txtEmojiName.Text + "\".");
            }
        }
    }
}
