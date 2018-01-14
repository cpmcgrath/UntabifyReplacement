using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Text.Editor;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace CMcG.UntabifyReplacement
{
    sealed class UntabifyCommandFilter : CommandFilter
    {
        IWpfTextView m_view;

        public UntabifyCommandFilter(IWpfTextView view)
        {
            m_view = view;
        }

        protected override Guid CommandGuid { get; } = new Guid("1496a755-94de-11d0-8c3f-00c04fc2aae2");

        protected override uint CommandId => 46;

        public override void Execute(uint nCmdID)
        {
            var snapshot = m_view.TextSnapshot;
            var lines    = GetLinesToAlign(snapshot).Where(x => x.GetText().Contains("\t")).ToArray();
            using (var edit = snapshot.TextBuffer.CreateEdit())
            {
                foreach (var line in lines)
                {
                    if (!edit.Replace(line.Start.Position, line.Length, ReplaceTabs(line.GetText())))
                        return;
                }

                edit.Apply();
            }
        }

        string ReplaceTabs(string value)
        {
            int tabSize = m_view.Options.GetOptionValue(DefaultOptions.TabSizeOptionId);
            
            var index = value.IndexOf('\t');
            while (index >= 0)
            {
                value = value.Remove(index, 1).Insert(index, string.Empty.PadLeft(tabSize - (index % tabSize)));
                index = value.IndexOf('\t');
            }

            return value;
        }

        IEnumerable<ITextSnapshotLine> GetLinesToAlign(ITextSnapshot snapshot)
        {
            int start = snapshot.GetLineNumberFromPosition(m_view.Selection.Start.Position);
            int end   = snapshot.GetLineNumberFromPosition(m_view.Selection.End.Position);

            if (start == end)
            {
                start = 0;
                end   = snapshot.LineCount -1;
            }

            return start.UpTo(end).Select(x => snapshot.GetLineFromLineNumber(x));
        }
    }
}