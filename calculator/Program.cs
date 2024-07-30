using System;
using System.Windows.Forms;

namespace calculator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());
        }
    }

    public class CalculatorForm : Form
    {
        private TextBox txtDisplay;
        private Button[] numButtons;
        private Button btnAdd, btnSubtract, btnMultiply, btnDivide, btnEquals, btnClear;
        private double _value = 0;
        private string _operation = "";
        private bool _operationPressed = false;

        public CalculatorForm()
        {
            this.Text = "KALKULATOR";
            this.Size = new System.Drawing.Size(300, 400);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            txtDisplay = new TextBox();
            txtDisplay.ReadOnly = true;
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            txtDisplay.Size = new System.Drawing.Size(260, 30);
            txtDisplay.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(txtDisplay);

            numButtons = new Button[10];
            for (int i = 0; i < numButtons.Length; i++)
            {
                numButtons[i] = new Button();
                numButtons[i].Text = i.ToString();
                numButtons[i].Size = new System.Drawing.Size(50, 50);
                numButtons[i].Click += new EventHandler(NumButton_Click);
                this.Controls.Add(numButtons[i]);
            }

            btnAdd = CreateOperatorButton("+", new System.Drawing.Point(200, 70));
            btnSubtract = CreateOperatorButton("-", new System.Drawing.Point(200, 130));
            btnMultiply = CreateOperatorButton("*", new System.Drawing.Point(200, 190));
            btnDivide = CreateOperatorButton("/", new System.Drawing.Point(200, 250));
            btnEquals = CreateOperatorButton("=", new System.Drawing.Point(140, 310));
            btnClear = CreateOperatorButton("C", new System.Drawing.Point(20, 310));

            PositionNumberButtons();
        }

        private Button CreateOperatorButton(string text, System.Drawing.Point location)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new System.Drawing.Size(50, 50);
            button.Location = location;
            button.Click += new EventHandler(OperatorButton_Click);
            this.Controls.Add(button);
            return button;
        }

        private void PositionNumberButtons()
        {
            int xPos = 20;
            int yPos = 70;

            for (int i = 1; i <= 9; i++)
            {
                numButtons[i].Location = new System.Drawing.Point(xPos, yPos);
                xPos += 60;
                if (i % 3 == 0)
                {
                    xPos = 20;
                    yPos += 60;
                }
            }
            numButtons[0].Location = new System.Drawing.Point(80, 250);
        }

        private void NumButton_Click(object sender, EventArgs e)
        {
            if ((txtDisplay.Text == "0") || (_operationPressed))
            {
                txtDisplay.Clear();
            }

            _operationPressed = false;
            Button b = (Button)sender;
            txtDisplay.Text += b.Text;
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "C")
            {
                txtDisplay.Text = "0";
                _value = 0;
                _operation = "";
                _operationPressed = false;
                return;
            }

            if (b.Text == "=")
            {
                PerformCalculation();
                _operationPressed = false;
                return;
            }

            _operation = b.Text;
            _value = Double.Parse(txtDisplay.Text);
            _operationPressed = true;
        }

        private void PerformCalculation()
        {
            switch (_operation)
            {
                case "+":
                    txtDisplay.Text = (_value + Double.Parse(txtDisplay.Text)).ToString();
                    break;

                case "-":
                    txtDisplay.Text = (_value - Double.Parse(txtDisplay.Text)).ToString();
                    break;

                case "*":
                    txtDisplay.Text = (_value * Double.Parse(txtDisplay.Text)).ToString();
                    break;

                case "/":
                    txtDisplay.Text = (_value / Double.Parse(txtDisplay.Text)).ToString();
                    break;

                default: break;
            }
        }
    }
}
