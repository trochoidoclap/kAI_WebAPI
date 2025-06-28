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
                .Where(t => t.id_user == userId)
                .OrderByDescending(t => t.date)
                .Select(t => new GetTranscriptsDto
                {
                    id_transcript = t.id_transcript,
                    date = t.date,
                    content = t.content,
                    rating = t.rating
                })
                .ToListAsync();

            return transcripts.Count > 0 ? transcripts : null;
        }

        public async Task<List<Remark>?> GetTranscriptRemarksById(int id)
        {
            var remarks = await _context.Remark
                .Where(r => r.Transcript.id_user == id)
                .OrderByDescending(r => r.id_remark)
                .ToListAsync();
            return remarks.Count > 0 ? remarks : null;
        }
    }
}