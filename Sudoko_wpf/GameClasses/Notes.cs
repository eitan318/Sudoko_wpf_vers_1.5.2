using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Sudoko_wpf.publico;

namespace Sudoko_wpf.GameClasses
{
    public class Notes : UniformGrid
    {
        private List<TextBlock> notes;
        public Notes()
        {
            notes = new List<TextBlock>();
            Clear();
        }

        public void ManipulatNote(string noteText)
        {
            if (NoteExist(noteText))
            {
                RemoveNote(noteText);
            }
            else
            {
                AddNote(noteText);
            }

        }


        public void AddNote(string noteText)
        {
            Viewbox noteContainer = new Viewbox();
            noteContainer.HorizontalAlignment = HorizontalAlignment.Stretch; // Stretch horizontally
            noteContainer.VerticalAlignment = VerticalAlignment.Stretch;

            TextBlock note = NewNote(noteText);

            //Add the TextBlock to the ViewBox
            noteContainer.Child = note;

            notes.Add(note);
            Children.Add(noteContainer);

            MyUpdateLayout();
        }


        private TextBlock NewNote(string noteText)
        {
            return new TextBlock()
            {
                Text = noteText,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Foreground = BrushResources.TextFore
            };
        }

        public void Clear()
        {
            notes.Clear();
            Children.Clear();

            MyUpdateLayout();
        }

        public bool IsLastOne()
        {
            return notes.Count == 1;
        }

        public string LastOne()
        {
            return notes[0].Text;
        }

        public bool NoteExist(string noteText)
        {
            foreach (TextBlock note in notes)
            {
                if (note.Text == noteText)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveNote(string noteText)
        {
            for (int i = 0; i < notes.Count(); i++)
            {
                TextBlock note = notes[i];
                if (note.Text == noteText)
                {
                    Children.RemoveAt(i);
                    notes.Remove(note);
                    MyUpdateLayout();
                }
            }
        }

        private void MyUpdateLayout()
        {
            int noteCount = notes.Count;
            if (noteCount < 4)
            {
                UpdateLayout();
                return;
            }

            Columns = (int)Math.Ceiling(Math.Sqrt(noteCount));

            Rows = (int)Math.Ceiling(noteCount / (double)Columns);

            if (Columns * (Rows - 1) >= noteCount)
            {
                Rows--;
            }
        }
    }
}
