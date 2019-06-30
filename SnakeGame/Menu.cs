using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Menu
    {
        public string Title { get; set; }
        public List<MenuOption> options = new List<MenuOption>();

        public Menu()
        {
            this.Title = "Snake";
            MenuOption Start = new MenuOption("Jugar")
            {
                IsChecked = true,
                MenuAction = MenuOption.Action.Start
            };
            this.options.Add(Start);
            MenuOption Scores = new MenuOption("Ver Puntajes")
            {
                MenuAction = MenuOption.Action.Scores
            };
            this.options.Add(Scores);
            MenuOption Exit = new MenuOption("Salir")
            {
                MenuAction = MenuOption.Action.Exit
            };
            this.options.Add(Exit);
        }

        /// <summary>
        /// Selecciona el item anterior del menu
        /// </summary>
        public void SelectPrev()
        {
            int index = this.options.FindIndex(f => f.IsChecked == true);
            this.options[index].IsChecked = false;
            if(index == 0)
            {
                this.options[options.Count - 1].IsChecked = true;
            }
            else
            {
                this.options[index - 1].IsChecked = true;
            }
        }

        /// <summary>
        /// Selecciona el siguiente item del menu
        /// </summary>
        public void SelectNext()
        {
            int index = this.options.FindIndex(f => f.IsChecked == true);
            this.options[index].IsChecked = false;
            if (index == options.Count - 1)
            {
                this.options[0].IsChecked = true;
            }
            else
            {
                this.options[index + 1].IsChecked = true;
            }
        }
    }
}
