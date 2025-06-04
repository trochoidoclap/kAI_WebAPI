﻿using kAI_webAPI.Data;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Question;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kAI_webAPI.Repository
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ApplicationDBContext _context;

        public QuestionsRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            var questions = await _context.Questions.ToListAsync();
            return questions;
        }
    }
}
