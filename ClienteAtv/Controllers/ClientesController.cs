namespace ClienteAtv.Controllers
{
    //Anna Clara Oliveira Andrade 
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteServico _clienteService;

        public ClientesController()
        {
            _clienteService = new ClienteServico();
        }

        [HttpPost]
        public IActionResult CreateCliente([FromBody] Cliente cliente)
        {
            try
            {
                _clienteService.AddCliente(cliente);
                return CreatedAtAction(nameof(GetClienteByCPF), new { cpf = cliente.CPF }, cliente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllClientes()
        {
            var clientes = _clienteService.GetAllClientes();
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetClienteByCPF(string cpf)
        {
            var cliente = _clienteService.GetClienteByCPF(cpf);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPut("{cpf}")]
        public IActionResult UpdateCliente(string cpf, [FromBody] Cliente cliente)
        {
            try
            {
                _clienteService.UpdateCliente(cpf, cliente);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{cpf}")]
        public IActionResult DeleteCliente(string cpf)
        {
            try
            {
                _clienteService.DeleteCliente(cpf);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }

}
