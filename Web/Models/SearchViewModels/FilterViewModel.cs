using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.ModelsDTO;

namespace PL.Models.SearchViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<GenreDTO> teams, int team, string position, string selectedAuthor)
        {
            teams.Insert(0, new GenreDTO { Name = "All", Id = 0 });
            Genres = new SelectList(teams, "Id", "Name", team);
            SelectedTeam = team;
            SelectedInput = position;
            SelectedAuthor = selectedAuthor;
        }
        public SelectList Genres { get; } // список клубов
        public int SelectedTeam { get; } // выбранный клуб
        public string SelectedInput { get; } // введенная позиция
        public string SelectedAuthor { get; } // введенная позиция
    }
}
