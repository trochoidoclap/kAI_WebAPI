using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Models.Transcript;
namespace kAI_webAPI.Interfaces
{
    public interface ITranscriptRepository
    {
        Task AddTranscriptAsync(Transcript transcript);
        Task<Transcript> GetTranscript(int id);
    }
}
