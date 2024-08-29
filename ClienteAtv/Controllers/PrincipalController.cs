using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;



public class ClienteServico
{
    private readonly string _filePath = "clientes.json";

    public ClienteServico()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Dispose();
            File.WriteAllText(_filePath, "[]");
        }
    }

    private List<Cliente> GetClientes()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
    }

    private void SaveClientes(List<Cliente> clientes)
    {
        var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void AddCliente(Cliente cliente)
    {
        ValidateCliente(cliente);
        var clientes = GetClientes();
        clientes.Add(cliente);
        SaveClientes(clientes);
    }

    public List<Cliente> GetAllClientes()
    {
        return GetClientes();
    }

    public Cliente GetClienteByCPF(string cpf)
    {
        var clientes = GetClientes();
        return clientes.FirstOrDefault(c => c.CPF == cpf);
    }

    public void UpdateCliente(string cpf, Cliente updatedCliente)
    {
        ValidateCliente(updatedCliente);
        var clientes = GetClientes();
        var index = clientes.FindIndex(c => c.CPF == cpf);

        if (index == -1)
        {
            throw new KeyNotFoundException("Cliente não encontrado.");
        }

        clientes[index] = updatedCliente;
        SaveClientes(clientes);
    }

    public void DeleteCliente(string cpf)
    {
        var clientes = GetClientes();
        var clienteToRemove = clientes.FirstOrDefault(c => c.CPF == cpf);

        if (clienteToRemove == null)
        {
            throw new KeyNotFoundException("Cliente não encontrado.");
        }

        clientes.Remove(clienteToRemove);
        SaveClientes(clientes);
    }

    private void ValidateCliente(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome) ||
            cliente.DataNascimento == default ||
            string.IsNullOrWhiteSpace(cliente.Sexo) ||
            string.IsNullOrWhiteSpace(cliente.RG) ||
            string.IsNullOrWhiteSpace(cliente.CPF) ||
            string.IsNullOrWhiteSpace(cliente.Endereco) ||
            string.IsNullOrWhiteSpace(cliente.Cidade) ||
            string.IsNullOrWhiteSpace(cliente.Estado) ||
            string.IsNullOrWhiteSpace(cliente.Telefone) ||
            string.IsNullOrWhiteSpace(cliente.Email))
        {
            throw new ArgumentException("Todos os campos são obrigatórios.");
        }

        if (!IsValidCPF(cliente.CPF))
        {
            throw new ArgumentException("CPF inválido.");
        }
    }

    private bool IsValidCPF(string cpf)
    {
        return true;
    }
}

