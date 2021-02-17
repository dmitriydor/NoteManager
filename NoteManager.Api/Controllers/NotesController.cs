using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteManager.Api.Contracts.Requests;
using NoteManager.Api.Contracts.Responses;
using NoteManager.Api.Models;
using NoteManager.Api.Services;

namespace NoteManager.Api.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<NoteResponse> GetNote([FromQuery] string id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            return _mapper.Map<NoteResponse>(note);
        }

        [HttpGet]
        public async Task<List<NoteResponse>> GetNotes([FromBody] string userId)
        {
            var notes = await _noteService.GetNotesByUserIdAsync(userId);
            return _mapper.Map<List<NoteResponse>>(notes);
        }

        [HttpPost("{id}")]
        public async Task<NoteResponse> UpsertNote([FromBody] NoteRequest note)
        {
            var storedNote = await _noteService.UpsertNoteAsync(_mapper.Map<Note>(note));
            return _mapper.Map<NoteResponse>(storedNote);
        }

        [HttpDelete("{id}")]
        public async Task DeleteNote([FromQuery] string id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            await _noteService.DeleteNotesAsync(note);
        }
    }
}