using System.Windows;
using System.Windows.Forms;

namespace DICE.Modules.Views.CollectConfigViews
{
    public partial class MemoryInputMegaView
    {
		public MemoryInputMegaView()
        {
            InitializeComponent();
        }
		private void BrowseFileButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Raw Files|*.raw|Memory Dump Files|*.dmp|All Files|*.*"
			};

			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				FilePathTextBox.Text = openFileDialog.FileName;
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Please select a file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(FilePathTextBox.Text))
			{
				System.Windows.Forms.MessageBox.Show("Please select a file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				Close();
			}
		}
	}
}
