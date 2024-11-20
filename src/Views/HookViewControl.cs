using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Mono.Cecil;
using Oxide.Patcher.Hooks;
using Oxide.Patcher.Patching;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oxide.Patcher.Common;
using Oxide.Patcher.Common.TextHighlighting;

namespace Oxide.Patcher.Views
{
    public partial class HookViewControl : UserControl
    {
        /// <summary>
        /// Gets or sets the hook to use
        /// </summary>
        public Hook Hook { get; set; }

        /// <summary>
        /// Gets or sets the main patcher form
        /// </summary>
        public PatcherForm MainForm { get; set; }

        public CheckBox FlagCheck { get; set; }

        private TextEditorControl _msilBefore, _msilAfter, _codeBefore, _codeAfter;

        private MethodDefinition _methodDef;

        private bool _loaded;

        private HighlightGroup _msilHighlight;

        public HookViewControl()
        {
            InitializeComponent();
            FlagCheck = flagCheckBox;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _methodDef = MainForm.AssemblyLoader.GetMethod(Hook.AssemblyName, Hook.TypeName, Hook.Signature);

            //Hook Types
            for (int i = 0; i < Hook.HookTypes.Length; i++)
            {
                string typeName = Hook.HookTypes[i].GetCustomAttribute<HookType>().Name;

                hooktypedropdown.Items.Add(typeName);

                if (typeName == Hook.HookTypeName)
                {
                    hooktypedropdown.SelectedIndex = i;
                }
            }

            //Base Hook
            if (Hook.BaseHook != null)
            {
                SetBaseHookButtonText(Hook.BaseHookName);
                baseHookBtn.Enabled = true;
            }

            assemblytextbox.Text = Hook.AssemblyName;
            typenametextbox.Text = Hook.TypeName;

            methodnametextbox.Text = _methodDef != null ? Hook.Signature.ToString() : $"{Hook.Signature} (METHOD MISSING)";

            nametextbox.Text = Hook.Name;
            hooknametextbox.Text = Hook.HookName;
            hookdescriptiontextbox.Text = Hook.HookDescription;

            applybutton.Enabled = false;

            flagCheckBox.Checked = Hook.Flagged;

            clonebutton.Enabled = Hook.ChildHook == null;

            LoadSettings();

            await LoadCodeViews();

            _loaded = true;
        }

        #region -Loading-

        private void LoadSettings()
        {
            IHookSettingsControl settingsView = Hook.CreateSettingsView();
            if (settingsView == null)
            {
                Label label = new Label
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Text = "No settings.",
                    Dock = DockStyle.Fill
                };

                hooksettingstab.Controls.Add(label);
            }
            else
            {
                settingsView.Dock = DockStyle.Fill;
                settingsView.OnSettingsChanged += settingsview_OnSettingsChanged;
                hooksettingstab.Controls.Add((Control)settingsView);
            }
        }

        private async Task LoadCodeViews()
        {
            if (_methodDef == null)
            {
                beforetab.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    Text = "METHOD MISSING",
                    TextAlign = ContentAlignment.MiddleCenter
                });

                aftertab.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    Text = "METHOD MISSING",
                    TextAlign = ContentAlignment.MiddleCenter
                });

                _loaded = true;
                return;
            }

            ILWeaver weaver = new ILWeaver(_methodDef.Body) { Module = _methodDef.Module };

            Hook.PreparePatch(_methodDef, weaver);

            _msilBefore = new TextEditorControl { Dock = DockStyle.Fill, Text = weaver.ToString(), IsReadOnly = true };

            _codeBefore = new TextEditorControl
            {
                Dock = DockStyle.Fill,
                Text = await Decompiler.GetSourceCode(_methodDef, weaver),
                Document = { HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#") },
                IsReadOnly = true
            };

            Hook.ApplyPatch(_methodDef, weaver);

            string afterText = weaver.ToString();

            _msilAfter = new TextEditorControl { Dock = DockStyle.Fill, Text = afterText, IsReadOnly = true };
            _codeAfter = new TextEditorControl
            {
                Dock = DockStyle.Fill,
                Text = await Decompiler.GetSourceCode(_methodDef, weaver),
                Document = { HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#") },
                IsReadOnly = true
            };

            beforetab.Controls.Add(_msilBefore);
            aftertab.Controls.Add(_msilAfter);
            codebeforetab.Controls.Add(_codeBefore);
            codeaftertab.Controls.Add(_codeAfter);

            _msilHighlight = new HighlightGroup(_msilAfter);

            AddHighlight(afterText);
        }

        private void AddHighlight(string afterText)
        {
            int searchIndex = afterText.IndexOf($"\"{Hook.HookName}\"");
            if (searchIndex == -1)
            {
                return;
            }

            int startIndex = afterText.LastIndexOf("IL_", searchIndex);
            if (startIndex == -1)
            {
                return;
            }

            int endIndex = afterText.IndexOf("\n", startIndex);
            if (endIndex == -1)
            {
                return;
            }

            TextMarker marker = new TextMarker(startIndex, endIndex - startIndex, TextMarkerType.SolidBlock, Color.Yellow, Color.Black);

            _msilHighlight.AddMarker(marker);
        }

        #endregion

        #region -Actions-

        private void settingsview_OnSettingsChanged()
        {
            applybutton.Enabled = true;
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(MainForm, "Are you sure you want to remove this hook?", "Oxide Patcher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MainForm.RemoveHook(Hook);
            }
        }

        private void hooktypedropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded || hooktypedropdown.SelectedIndex < 0)
            {
                return;
            }

            Type hookType = Hook.HookTypes[hooktypedropdown.SelectedIndex];
            if (hookType == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show(MainForm, "Are you sure you want to change the type of this hook? Any hook settings will be lost.",
                "Oxide Patcher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MainForm.RemoveHook(Hook);

                Hook newHook = Activator.CreateInstance(hookType) as Hook;
                newHook.Name = Hook.Name;
                newHook.HookName = Hook.HookName;
                newHook.HookDescription = Hook.HookDescription;
                newHook.AssemblyName = Hook.AssemblyName;
                newHook.TypeName = Hook.TypeName;
                newHook.Signature = Hook.Signature;
                newHook.Flagged = Hook.Flagged;
                newHook.MSILHash = Hook.MSILHash;
                newHook.BaseHook = Hook.BaseHook;
                newHook.BaseHookName = Hook.BaseHookName;
                newHook.HookCategory = Hook.HookCategory;

                MainForm.AddHook(newHook);
                MainForm.GotoHook(newHook);
            }
        }

        private void baseHookBtn_Click(object sender, EventArgs e)
        {
            if (Hook.BaseHook != null)
                MainForm.GotoHook(Hook.BaseHook);
        }

        private void nametextbox_TextChanged(object sender, EventArgs e)
        {
            applybutton.Enabled = true;
        }

        private void hooknametextbox_TextChanged(object sender, EventArgs e)
        {
            applybutton.Enabled = true;
        }

        private void hookdescriptiontextbox_TextChanged(object sender, EventArgs e)
        {
            applybutton.Enabled = true;
        }

        private async void applybutton_Click(object sender, EventArgs e)
        {
            string oldName = Hook.Name;

            Hook.Name = nametextbox.Text;
            Hook.HookName = hooknametextbox.Text;
            if (hookdescriptiontextbox.Text != string.Empty)
            {
                Hook.HookDescription = hookdescriptiontextbox.Text;
            }

            MainForm.UpdateHook(Hook, oldName: oldName != nametextbox.Text ? oldName : null);

            if (_msilBefore != null && _msilAfter != null)
            {
                ILWeaver weaver = new ILWeaver(_methodDef.Body) { Module = _methodDef.Module };

                Hook.PreparePatch(_methodDef, weaver);
                _msilBefore.Text = weaver.ToString();
                _codeBefore.Text = await Decompiler.GetSourceCode(_methodDef, weaver);

                Hook.ApplyPatch(_methodDef, weaver);

                string afterText = weaver.ToString();

                _msilAfter.Text = afterText;
                _codeAfter.Text = await Decompiler.GetSourceCode(_methodDef, weaver);

                AddHighlight(afterText);
            }

            applybutton.Enabled = false;
        }

        private void clonebutton_Click(object sender, EventArgs e)
        {
            if (Hook.ChildHook != null)
            {
                MessageBox.Show($"You can only clone a hook once, use the last clone in the method ({GetLastCloneName(Hook)}) to create another clone",
                                "Cannot clone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Hook newHook = Activator.CreateInstance(Hook.GetType()) as Hook;
            newHook.Name = Hook.Name + "(Clone)";
            newHook.HookName = Hook.HookName + "(Clone)";
            newHook.HookDescription = Hook.HookDescription;
            newHook.AssemblyName = Hook.AssemblyName;
            newHook.TypeName = Hook.TypeName;
            newHook.Signature = Hook.Signature;
            newHook.Flagged = Hook.Flagged;
            newHook.MSILHash = Hook.MSILHash;
            newHook.BaseHook = Hook;

            Hook.ChildHook = newHook;

            MainForm.AddHook(newHook);
            MainForm.GotoHook(newHook);

            clonebutton.Enabled = false;
        }

        private void checkBoxFlag_CheckedChanged(object sender, EventArgs e)
        {
            Hook.Flagged = flagCheckBox.Checked;
            MainForm.UpdateHook(Hook);
            if (Hook.Flagged != flagCheckBox.Checked)
            {
                flagCheckBox.Checked = Hook.Flagged;
            }
        }

        private string GetLastCloneName(Hook hook)
        {
            Hook currentHook = hook;

            while (currentHook.ChildHook != null)
            {
                currentHook = currentHook.ChildHook;
            }

            return currentHook.Name;
        }

        public void SetBaseHookButtonText(string text = "") => baseHookBtn.Text = text;

        #endregion
    }
}
