using MusicPortal.BLL.ModelsDTO;
using PL.Models.SearchViewModels;

namespace MusicPortal.Models.SearchViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<AudioDTO> Audios { get; }
        public IEnumerable<string> ImagePaths { get; set; }
        public Dictionary<int, List<GenreDTO>> Genres { get; }
        public PageViewModel PageViewModel { get; }

        public SortViewModel SortViewModel { get; }
        public IndexViewModel(IEnumerable<AudioDTO> audios, IEnumerable<string> imagePaths, PageViewModel viewModel, Dictionary<int, List<GenreDTO>> genres,
            SortViewModel sortViewModel)
        {
            Audios = audios;
            ImagePaths = imagePaths;
            PageViewModel = viewModel;
            Genres = genres;
            SortViewModel = sortViewModel;
        }
    }
}
