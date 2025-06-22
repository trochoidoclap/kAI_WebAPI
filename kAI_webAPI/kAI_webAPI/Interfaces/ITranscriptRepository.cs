using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Dtos.Transcript;
using kAI_webAPI.Models.Transcript;
namespace kAI_webAPI.Interfaces
{
    public interface ITranscriptRepository
    {
        Task<Transcript?> AddTranscriptAsync(Transcript transcript);
        Task<List<GetTranscriptsDto>?> GetTranscriptsByUserId(int id);
        Task<Remark?> AddRemarkAsync(Remark remark);
        Task<List<Remark?>> GetTranscriptRemarksById(int userId);
    }
}
