using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Oxide.Patcher
{
    public partial class NewProjectForm : Form
    {
        private bool _textBoxNameReady = false, _textBoxDirectoryReady = false, _textBoxFileReady = false;

        public NewProjectForm()
        {
            InitializeComponent();
            _textBoxNameReady = !string.IsNullOrWhiteSpace(nametextbox.Text);
            _textBoxDirectoryReady = !string.IsNullOrWhiteSpace(directorytextbox.Text);
            _textBoxFileReady = !string.IsNullOrWhiteSpace(filenametextbox.Text);
            ToggleCreateButton();
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            // PatcherForm owner = Owner as PatcherForm;
            // PatcherFormSettings settings = owner.CurrentSettings;

            // selectdirectorydialog.SelectedPath = settings.LastTargetDirectory;
            directorytextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //selectfilenamedialog.FileName = settings.LastProjectDirectory;
        }

        private void selectdirectorybutton_Click(object sender, EventArgs e)
        {
            DialogResult result = selectdirectorydialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (CheckTargetDirectory(selectdirectorydialog.SelectedPath))
                {
                    return;
                }

                //PatcherForm owner = Owner as PatcherForm;

                //PatcherFormSettings settings = owner.CurrentSettings;
                //settings.LastTargetDirectory = selectdirectorydialog.SelectedPath;
                //settings.Save();

                directorytextbox.Text = selectdirectorydialog.SelectedPath;
            }
        }

        private void selectfilenamebutton_Click(object sender, EventArgs e)
        {
            DialogResult result = selectfilenamedialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                //PatcherForm owner = Owner as PatcherForm;

                //PatcherFormSettings settings = owner.CurrentSettings;
                //settings.LastProjectDirectory = Path.GetDirectoryName(selectfilenamedialog.FileName);
                //settings.Save();

                filenametextbox.Text = selectfilenamedialog.FileName;
            }
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void createbutton_Click(object sender, EventArgs e)
        {
            // Verify
            if (!filenametextbox.Text.EndsWith(".opj"))
            {
                filenametextbox.Text += ".opj";
            }

            if (!Directory.Exists(directorytextbox.Text))
            {
                MessageBox.Show(this, $"The target directory does not exist!\n{directorytextbox.Text}", "Oxide Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CheckTargetDirectory(directorytextbox.Text))
            {
                return;
            }

            string fileDir = Path.GetDirectoryName(filenametextbox.Text);
            if (!Directory.Exists(fileDir))
            {
                MessageBox.Show(this, $"The project directory does not exist!\n{fileDir}", "Oxide Patcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create project
            Project newproj = new Project();
            newproj.Name = nametextbox.Text;
            newproj.TargetDirectory = directorytextbox.Text;
            newproj.Save(filenametextbox.Text);

            // Set parent form to load it
            PatcherForm owner = Owner as PatcherForm;
            owner.OpenProject(filenametextbox.Text);

            // Close
            Close();
        }

        private void nametextbox_TextChanged(object sender, EventArgs e)
        {
            _textBoxNameReady = !string.IsNullOrWhiteSpace(nametextbox.Text);
            ToggleCreateButton();
        }

        private void directorytextbox_TextChanged(object sender, EventArgs e)
        {
            _textBoxDirectoryReady = !string.IsNullOrWhiteSpace(directorytextbox.Text);
            ToggleCreateButton();
        }

        private void filenametextbox_TextChanged(object sender, EventArgs e)
        {
            _textBoxFileReady = !string.IsNullOrWhiteSpace(filenametextbox.Text);
            ToggleCreateButton();
        }

        // Toggle Create Button
        private void ToggleCreateButton() => createbutton.Enabled = _textBoxNameReady && _textBoxDirectoryReady && _textBoxFileReady;

        // Checking the target directory for libraries
        private bool CheckTargetDirectory(string path)
        {
            if (!Directory.EnumerateFiles(path).Any(x => x.EndsWith(".dll") || x.EndsWith(".exe")) &&
                MessageBox.Show(this, "The specified directory does not contain any dll files. Continue anyway?", "Oxide Patcher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return true;
            }
            return false;
        }
    }
}
