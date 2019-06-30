using System;
namespace SnakeGame
{
    public class MenuOption
    {
        public string Label { get; set; }
        public bool IsChecked { get; set; }
        public enum Action { Start, Scores, Exit };
        public Action MenuAction { get; set; }

        public MenuOption(string lbl)
        {
            this.Label = lbl;
            this.IsChecked = false;
        }
    }
}
