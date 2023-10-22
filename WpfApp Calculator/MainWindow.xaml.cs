using System;
using System.Collections.Generic;
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

namespace WpfApp_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder _commandHelper = new StringBuilder();
        private List<long> _numbers = new List<long>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OperationButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                
                string buttonName = button.Name.Substring("Btn".Length);

                if (buttonName != OperationNames.Remove && buttonName != OperationNames.EqualsTo)
                {
                    DisableAllOperationButtons();
                    BtnZero.IsEnabled = false;
                }                


                switch (buttonName)
                {
                    case OperationNames.Remove:
                        {
                            if (LblResult.Content.ToString().Length == 1)
                            {
                                DisableAllOperationButtons();
                                BtnZero.IsEnabled = false;
                            }

                            BackBtn_Click(sender, e);                            
                        }
                        break;

                    case OperationNames.Retry:
                        {
                            CEBtn_Click(sender, e);
                        }
                        break;

                    case OperationNames.EqualsTo:
                        {
                            EqualslBtn_Click(sender, e);
                        }
                        break;

                    case OperationNames.Add:
                        {                     
                            AddDataToLabel('+');
                        }
                        break;
                    case OperationNames.Subtract:
                        {
                            AddDataToLabel('-');
                        }
                        break;
                    case OperationNames.Multiply:
                        {
                            AddDataToLabel('*');
                        }
                        break;
                    case OperationNames.Divide:
                        {                         
                            AddDataToLabel('/');
                        }
                        break;
                }                

            }
        }


        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                BtnZero.IsEnabled = true;
                RemoveZeroFromLabel();
                EnableAllOperationButtons();
                AddDataToLabel(Convert.ToChar(button.Content));
            }
        }



        private void EnableAllOperationButtons()
        {
            BtnMultiply.IsEnabled = true;
            BtnDivide.IsEnabled = true;
            BtnAdd.IsEnabled = true;
            BtnSubtract.IsEnabled = true;
            BtnEquals.IsEnabled = true;
        }
        private void RemoveZeroFromLabel()
        {
            if (LblResult.Content.ToString() == "0")
                LblResult.Content = "";
        }

        private void AddDataToLabel(char data)
        {
            StringBuilder temp = new StringBuilder();

            if (data == '+' || data == '-' || data == '/' || data == '*')
                temp.Append(" ");

                temp.Append(data);
                         
            if (data == '+' || data == '-' || data == '/' || data == '*')
                temp.Append(" ");

            LblResult.Content += temp.ToString();

        }

        private void DisableAllOperationButtons()
        {
            BtnSubtract.IsEnabled = false;
            BtnAdd.IsEnabled = false;
            BtnMultiply.IsEnabled = false;
            BtnDivide.IsEnabled = false;
            BtnEquals.IsEnabled = false;
        }

        private void CEBtn_Click(object sender, EventArgs e)
        {
            string content = LblResult.Content.ToString();

            if (!(content.Length == 1 && content == "0"))
            {
                LblResult.Content = "0";
                DisableAllOperationButtons();
                BtnZero.IsEnabled = false;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            string content = LblResult.Content.ToString();

            if (content.Length == 1)
                LblResult.Content = "0";
            else
            {
                if (content[content.Length - 1] == ' ')
                {
                    LblResult.Content = content.Substring(0, content.Length - 3);
                    BtnZero.IsEnabled = true;
                    EnableAllOperationButtons();
                }
                else if (!(content.Length == 1 && content == "0"))
                {
                    LblResult.Content = content.Substring(0, content.Length - 1);

                    
                    char currentSymbol = content[content.Length - 1];
                    
                    if (currentSymbol == ' ')
                    {
                        BtnZero.IsEnabled = false;
                        DisableAllOperationButtons();
                    }


                }

                if (content.Length == 0)
                    LblResult.Content = "0";


            }

        }

        private void EqualslBtn_Click(object sender, RoutedEventArgs e)
        {
            BtnZero.IsEnabled = false;
            PickNumbersAndCommands();
            CalculatorHelper.CalculateDivideAndMultiply(ref _numbers, ref _commandHelper);
            string result = CalculatorHelper.CalculateSameCommands(_numbers, _commandHelper);
            _numbers.Clear();
            _commandHelper.Clear();
            LblResult.Content= result;

        }

        private void PickNumbersAndCommands()
        {
            StringBuilder resultHelper = new StringBuilder();
            resultHelper.Append(LblResult.Content).Append(", ");

            int i = 0;

            if (resultHelper[0] == '-')
                i = 1;

            for (; i < resultHelper.Length; i++)
            {
                if (resultHelper[i] == '+' || resultHelper[i] == '-' || resultHelper[i] == '/' || resultHelper[i] == '*')
                {
                    _commandHelper.Append(resultHelper[i]);
                    resultHelper[i] = ',';
                }
            }

            string temp = resultHelper.ToString();
            foreach (string str in temp.Split(','))
            {
                if (long.TryParse(str, out long number))
                    _numbers.Add(number);
            }

        }

    }       

}