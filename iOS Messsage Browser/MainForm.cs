using System;
using System.Data;
using System.Windows.Forms;

namespace iOS_Messsage_Browser
{
    public partial class MainForm : Form
    {
        private DataTable Messages;
        private BindingSource bs = new BindingSource();
        private const string FileName = "3d0d7e5fb2ce288813306e4d4636395e047a3d28";

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonGetMesssages_Click(object sender, EventArgs e)
        {
            string FilePath = string.Empty;
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = string.Format(@"C:\Users\{0}\AppData\Roaming\Apple Computer\MobileSync\Backup\", Environment.UserName);
                openFile.Filter = string.Format("DB File|{0}", FileName);
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                if (openFile.FileName == string.Empty) return;
                FilePath = openFile.FileName;
            }
            DBTools.OpenDB(FilePath);

            Messages = DBTools.GetMessagesTable;
            bs.DataSource = Messages;
            dgData.DataSource = bs;

            foreach (var column in Messages.Columns)
            {
                System.Diagnostics.Debug.WriteLine("{0},", column);
            }

            DBTools.CloseDB();
        }

        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}