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

        public async Task<List<Transcript>> GetAllTranscriptsAsync()
        {
            return await _context.Transcripts.ToListAsync();
        }

        public async Task<Transcript?> GetTranscriptByIdAsync(int id)
        {
            return await _context.Transcripts
                                 .Where(t => t.Id_transcript == id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Transcript> GetTranscript(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Transcripts.FirstOrDefaultAsync(t => t.Id_transcript == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        // Fix for CS0535: Implementing the missing method from the interface
        public async Task AddTranscriptAsync(Transcript transcript)
        {
            await _context.Transcripts.AddAsync(transcript);
            await _context.SaveChangesAsync();
        }
    }
}