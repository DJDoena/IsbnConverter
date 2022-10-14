using System;
using System.IO;
using System.Windows.Forms;

namespace DoenaSoft.IsbnConverter
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnConvertToEanButtonClick(object sender, EventArgs e) => Convert(IsbnTextBox, EanTextBox, new Isbn());

        private void OnConvertToIsbnButtonClick(object sender, EventArgs e) => Convert(EanTextBox, IsbnTextBox, new BooklandEan());

        private static void Convert(TextBox source, TextBox target, IConvert converter)
        {
            target.Text = string.Empty;

            try
            {
                target.Text = converter.Convert(source.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCalculateIsbnChecksumButtonClick(object sender, EventArgs e) => CalculateCheckSum(IsbnTextBox, IsbnChecksumTextBox, new Isbn());

        private void OnCalculateEanChecksumButtonClick(object sender, EventArgs e) => CalculateCheckSum(EanTextBox, EanChecksumTextBox, new BooklandEan());

        private static void CalculateCheckSum(TextBox source, TextBox target, IChecksum calculator)
        {
            target.Text = string.Empty;

            try
            {
                target.Text = calculator.GetCheckSum(source.Text, true).ToString();
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCheckIsbnChecksumButtonClick(object sender, EventArgs e) => VerifyCheckSum(IsbnTextBox, new Isbn());

        private void OnCheckEanChecksumButtonClick(object sender, EventArgs e) => VerifyCheckSum(EanTextBox, new BooklandEan());

        private static void VerifyCheckSum(TextBox source, IChecksum verifier)
        {
            try
            {
                var isValid = verifier.IsValidCheckSum(source.Text);

                if (isValid)
                {
                    MessageBox.Show("Valid.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not valid!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}