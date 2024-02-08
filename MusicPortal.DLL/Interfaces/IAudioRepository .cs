using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
	public interface IAudioRepository : IRepository<Audio>
	{
		Task<List<Audio>> GetSongsByGenre(string genreName);
	}
}
