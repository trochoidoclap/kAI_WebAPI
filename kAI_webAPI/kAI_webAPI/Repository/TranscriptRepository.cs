using kAI_webAPI.Data;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Transcript;
using kAI_webAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace kAI_webAPI.Repository
{
    public class TranscriptRepository : ITranscriptRepository
    {
        private readonly ApplicationDBContext _context;

        public TranscriptRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Transcript?> AddTranscriptAsync(Transcript transcript)
        {
            await _context.Transcript.AddAsync(transcript);
            await _context.SaveChangesAsync();
            return transcript;
        }

        public async Task<List<Transcript>> GetTranscriptsByUserId(int userId)
        {
            return await _context.Transcript
                .Where(t => t.Id_user == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }
    }
}