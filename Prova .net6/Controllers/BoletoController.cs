using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_.net6.Data;
using Prova_.net6.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization; // Importe o namespace do AutoMapper

namespace Prova_.net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoController : Controller
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper; 

        public BoletoController(ApiDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper; // Atribua o IMapper ao campo privado
        }

        [HttpGet]
        [Route("/boletos")]
        public async Task<ActionResult> GetBoleto()
        {
            var boletos = await _context.Boletos.ToListAsync();
            var boletosDto = _mapper.Map<List<Boleto>>(boletos);

            return Ok(boletosDto); // Retorne os DTOs mapeados
        }

        [HttpPost]
        public async Task<ActionResult> CreateBoleto(Boleto boleto)
        {
            await _context.Boletos.AddAsync(boleto);
            await _context.SaveChangesAsync();

            var boletoDto = _mapper.Map<Boleto>(boleto);

            return Ok(boletoDto); // Retorne o DTO mapeado
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Boleto>> GetBoleto(int id)
        {
            var boleto = await _context.Boletos
                .Include(b => b.Banco)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            // Verificar se a data atual é posterior à data de vencimento
            if (DateTime.Now > boleto.DataVencimento)
            {
                // Calcular o valor do boleto com juros
                decimal jurosTotal = (boleto.Valor * boleto.Banco.PercentualJuros) / 100;
                decimal valorTotal = boleto.Valor + jurosTotal;

                // Atualizar o valor do boleto com juros
                boleto.Valor = valorTotal;
            }

            var boletoDto = _mapper.Map<Boleto>(boleto); 

            return boletoDto;
        }

    }
}
