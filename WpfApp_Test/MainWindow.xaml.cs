using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Reflection;

namespace WpfApp_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tabCount = 1;
        public MainWindow()
        {
            InitializeComponent();

            string filePath = "SqlHighlighting.xshd"; // Adjust this to your path

            using (FileStream fs = File.OpenRead(filePath))
            {
                // Check for BOM and skip if present
                if (fs.Length > 2)
                {
                    byte[] bom = new byte[3];
                    fs.Read(bom, 0, 3);
                    if (bom[0] != 0xEF || bom[1] != 0xBB || bom[2] != 0xBF)
                    {
                        fs.Position = 0; // No BOM, reset position
                    }
                }

                using (XmlTextReader reader = new XmlTextReader(fs))
                {
                    XshdSyntaxDefinition xshd = HighlightingLoader.LoadXshd(reader);
                    MyAvalonEdit.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void AddNewTab(string name = "", string condition = "", string smsText = "")
        {
            TabItem newTab = new TabItem();
            tabCount++;
            newTab.Header = $"V{tabCount}";

            Grid grid = CreateGridForTab();
            newTab.Content = grid;

            VariantsTabControl.Items.Add(newTab);
        }
        private Grid CreateGridForTab()
        {
            Grid grid = new Grid
            {
                Margin = new Thickness(0, 10, 0, 0)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // 'Název varianty' TextBlock
            TextBlock nameLabel = new TextBlock
            {
                Text = "Název varianty",
                Margin = new Thickness(10, 0, 5, 5)
            };
            Grid.SetRow(nameLabel, 0);
            Grid.SetColumn(nameLabel, 0);
            grid.Children.Add(nameLabel);

            // 'Název varianty' TextBox
            TextBox variantNameTextBox = new TextBox
            {
                Margin = new Thickness(0, 0, 10, 5),
                Width = 150
            };
            Grid.SetRow(variantNameTextBox, 0);
            Grid.SetColumn(variantNameTextBox, 1);
            grid.Children.Add(variantNameTextBox);

            // 'Podmínka varianty' TextBlock
            TextBlock conditionLabel = new TextBlock
            {
                Text = "Podmínka varianty",
                Margin = new Thickness(10, 0, 5, 5)
            };
            Grid.SetRow(conditionLabel, 0);
            Grid.SetColumn(conditionLabel, 2);
            grid.Children.Add(conditionLabel);

            // 'Podmínka varianty' TextBox
            TextBox variantConditionTextBox = new TextBox
            {
                Margin = new Thickness(10, 0, 10, 5),
                Width = 150
            };
            Grid.SetRow(variantConditionTextBox, 0);
            Grid.SetColumn(variantConditionTextBox, 3);
            grid.Children.Add(variantConditionTextBox);

            // 'Text SMS' Label
            TextBlock smsTextLabel = new TextBlock
            {
                Text = "Text SMS",
                Margin = new Thickness(10, 10, 10, 5)
            };
            Grid.SetRow(smsTextLabel, 1);
            Grid.SetColumn(smsTextLabel, 0);
            Grid.SetColumnSpan(smsTextLabel, 4);
            grid.Children.Add(smsTextLabel);

            // 'Text SMS' TextBox with vertical scrollbar
            TextBox smsTextTextBox = new TextBox
            {
                Margin = new Thickness(10, 10, 10, 5),
                Height = 200,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                TextWrapping = TextWrapping.Wrap
            };
            Grid.SetRow(smsTextTextBox, 2);
            Grid.SetColumn(smsTextTextBox, 0);
            Grid.SetColumnSpan(smsTextTextBox, 4);
            grid.Children.Add(smsTextTextBox);

            return grid;
        }


        private void AddVariantButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTab();
        }

        private void CopyVariantButton_Click(object sender, RoutedEventArgs e)
        {
            if (VariantsTabControl.SelectedItem is TabItem currentTab)
            {
                var content = currentTab.Content as Grid;
                if (content != null)
                {
                    var nameTextBox = content.Children[1] as TextBox;
                    var conditionTextBox = content.Children[3] as TextBox;
                    var smsTextBox = content.Children[5] as TextBox;
                    AddNewTab(nameTextBox.Text, conditionTextBox.Text, smsTextBox.Text);
                }
            }
        }

        private void RemoveVariantButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
