using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_.net6.Data;
using Prova_.net6.DTOs; // Importe o namespace onde estão os DTOs
using Prova_.net6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto_Avaliativo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public BancoController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Banco>> CreateBanco(Banco bancoDTO)
        {
            var banco = _mapper.Map<Banco>(bancoDTO);
            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<Banco>(banco));
        }

        //TODOS os registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banco>>> GetBancos()
        {
            var bancos = await _context.Bancos.ToListAsync();
            var bancosDTO = _mapper.Map<List<Banco>>(bancos);
            return Ok(bancosDTO);
        }

        //registro passando o Código do Banco
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Banco>> GetBanco(string codigo)
        {
            var banco = await _context.Bancos.FirstOrDefaultAsync(b => b.Codigo == codigo);
            if (banco == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Banco>(banco));
        }
    }
}
