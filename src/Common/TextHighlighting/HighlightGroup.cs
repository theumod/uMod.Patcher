using System;
using System.Collections.Generic;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace Oxide.Patcher.Common.TextHighlighting
{
    public class HighlightGroup : IDisposable
    {
        private readonly List<TextMarker> _markers = new List<TextMarker>();
        private readonly TextEditorControl _editorControl;
        private readonly IDocument _document;

        public HighlightGroup(TextEditorControl editorControl)
        {
            _editorControl = editorControl;
            _document = _editorControl.Document;
        }

        public void AddMarker(TextMarker marker)
        {
            _markers.Add(marker);
            _document.MarkerStrategy.AddMarker(marker);
        }

        private void ClearMarkers()
        {
            foreach (TextMarker m in _markers)
            {
                _document.MarkerStrategy.RemoveMarker(m);
            }

            _markers.Clear();
            _editorControl.Invoke(new Action(_editorControl.Refresh));
        }

        public void Dispose()
        {
            ClearMarkers();
            GC.SuppressFinalize(this);
        }

        ~HighlightGroup()
        {
            Dispose();
        }
    }
}
