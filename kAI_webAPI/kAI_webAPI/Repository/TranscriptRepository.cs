using kAI_webAPI.Data;
using kAI_webAPI.Dtos.Transcript;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Transcript;
using kAI_webAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace kAI_webAPI.Repository
{
    public class TranscriptRepository : ITranscriptRepository
    {
        private readonly ApplicationDBContext _context;

        public TranscriptRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Remark?> AddRemarkAsync(Remark remark)
        {
            await _context.Remark.AddAsync(remark);
            await _context.SaveChangesAsync();
            return remark;
        }

        public async Task<Transcript?> AddTranscriptAsync(Transcript transcript)
        {
            await _context.Transcript.AddAsync(transcript);
            await _context.SaveChangesAsync();
            return transcript;
        }

        public async Task<List<GetTranscriptsDto>?> GetTranscriptsByUserId(int userId)
        {
            var transcripts = await _context.Transcript
                .Where(t => t.Id_user == userId)
                .OrderByDescending(t => t.Date)
                .Select(t => new GetTranscriptsDto
                {
                    Id_transcript = t.Id_transcript,
                    Date = t.Date,
                    Content = t.Content,
                    Rating = t.Rating
                })
                .ToListAsync();

            return transcripts.Count > 0 ? transcripts : null;
        }

        public async Task<List<Remark?>> GetTranscriptRemarksById(int id)
        {
            return await _context.Remark
                .Where(r => r.Id_transcript == id)
                .Select(r => (Remark?)r) // Ensure nullability matches the interface
                .ToListAsync();
        }
    }
}