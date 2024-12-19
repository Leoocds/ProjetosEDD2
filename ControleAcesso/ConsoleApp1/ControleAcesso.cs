using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Ambiente> Ambientes { get; set; }

    public Usuario(int id, string nome)
    {
        Id = id;
        Nome = nome;
        Ambientes = new List<Ambiente>();
    }

    public bool ConcederPermissao(Ambiente ambiente)
    {
        if (!Ambientes.Contains(ambiente))
        {
            Ambientes.Add(ambiente);
            return true;
        }
        return false;
    }

    public bool RevogarPermissao(Ambiente ambiente)
    {
        if (Ambientes.Contains(ambiente))
        {
            Ambientes.Remove(ambiente);
            return true;
        }
        return false;
    }
}

public class Ambiente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public Queue<Log> Logs { get; set; }

    public Ambiente(int id, string nome)
    {
        Id = id;
        Nome = nome;
        Logs = new Queue<Log>(100);
    }

    public void RegistrarLog(Log log)
    {
        if (Logs.Count >= 100)
            Logs.Dequeue();
        Logs.Enqueue(log);
    }
}

public class Log
{
    public DateTime DtAcesso { get; set; }
    public Usuario Usuario { get; set; }
    public bool TipoAcesso { get; set; }

    public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
    {
        DtAcesso = dtAcesso;
        Usuario = usuario;
        TipoAcesso = tipoAcesso;
    }
}

public class Cadastro
{
    public List<Usuario> Usuarios { get; set; }
    public List<Ambiente> Ambientes { get; set; }

    public Cadastro()
    {
        Usuarios = new List<Usuario>();
        Ambientes = new List<Ambiente>();
    }

    public void AdicionarUsuario(Usuario usuario)
    {
        Usuarios.Add(usuario);
    }

    public bool RemoverUsuario(Usuario usuario)
    {
        if (!usuario.Ambientes.Any())
        {
            Usuarios.Remove(usuario);
            return true;
        }
        return false;
    }

    public Usuario PesquisarUsuario(string nome)
    {
        return Usuarios.FirstOrDefault(u => u.Nome == nome);
    }

    public void AdicionarAmbiente(Ambiente ambiente)
    {
        Ambientes.Add(ambiente);
    }

    public bool RemoverAmbiente(Ambiente ambiente)
    {
        Ambientes.Remove(ambiente);
        return true;
    }

    public Ambiente PesquisarAmbiente(string nome)
    {
        return Ambientes.FirstOrDefault(a => a.Nome == nome);
    }

    public void Upload(string filePath = "dados.json")
    {
        var data = new
        {
            Usuarios = Usuarios.Select(u => new
            {
                u.Id,
                u.Nome,
                Ambientes = u.Ambientes.Select(a => a.Id).ToList()
            }).ToList(),
            Ambientes = Ambientes.Select(a => new
            {
                a.Id,
                a.Nome,
                Logs = a.Logs.Select(log => new
                {
                    DtAcesso = log.DtAcesso,
                    Usuario = log.Usuario.Id,
                    TipoAcesso = log.TipoAcesso
                }).ToList()
            }).ToList()
        };

        File.WriteAllText(filePath,JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
    }

    public void Download(string filePath = "dados.json")
    {
        if (File.Exists(filePath))
        {
            var data = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(filePath));

            var usuarioMap = new Dictionary<int, Usuario>();
            foreach (var u in data.Usuarios)
            {
                var usuario = new Usuario((int)u.Id, (string)u.Nome);
                usuarioMap[usuario.Id] = usuario;
                Usuarios.Add(usuario);
            }

            foreach (var a in data.Ambientes)
            {
                var ambiente = new Ambiente((int)a.Id, (string)a.Nome);
                foreach (var log in a.Logs)
                {
                    var usuario = usuarioMap[(int)log.Usuario];
                    ambiente.RegistrarLog(new Log((DateTime)log.DtAcesso, usuario, (bool)log.TipoAcesso));
                }
                Ambientes.Add(ambiente);
            }

            foreach (var u in data.Usuarios)
            {
                var usuario = usuarioMap[(int)u.Id];
                usuario.Ambientes.AddRange(((IEnumerable<int>)u.Ambientes).Select(id => Ambientes.First(a => a.Id == id)));
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Cadastro cadastro = new Cadastro();
        cadastro.Download();

        while (true)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuário");
            Console.WriteLine("5. Consultar usuário");
            Console.WriteLine("6. Excluir usuário");
            Console.WriteLine("7. Conceder permissão de acesso ao usuário");
            Console.WriteLine("8. Revogar permissão de acesso ao usuário");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs de acesso");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    cadastro.Upload();
                    return;

                case 1:
                    Console.Write("Digite o nome do ambiente: ");
                    string nomeAmbiente = Console.ReadLine();
                    cadastro.AdicionarAmbiente(new Ambiente(cadastro.Ambientes.Count + 1, nomeAmbiente));
                    break;

                case 2:
                    Console.Write("Digite o nome do ambiente: ");
                    var ambienteConsulta = cadastro.PesquisarAmbiente(Console.ReadLine());
                    if (ambienteConsulta != null)
                        Console.WriteLine($"Ambiente encontrado: {ambienteConsulta.Nome}");
                    else
                        Console.WriteLine("Ambiente não encontrado.");
                    break;

                case 3:
                    Console.Write("Digite o nome do ambiente: ");
                    string excluirAmbiente = Console.ReadLine();
                    var ambienteExcluir = cadastro.PesquisarAmbiente(excluirAmbiente);
                    
                    if (ambienteExcluir != null)
                    {
                        cadastro.RemoverAmbiente(ambienteExcluir);
                        Console.WriteLine("Ambiente excluido com sucesso.");
                    }

                    else
                    {
                        Console.WriteLine("Ambiente não foi excluido.");
                    }
                    break;

                case 4:
                    Console.Write("Digite o nome do usuário: ");
                    string nomeUsuario = Console.ReadLine();
                    cadastro.AdicionarUsuario(new Usuario(cadastro.Usuarios.Count + 1, nomeUsuario));
                    break;

                case 5:
                    Console.Write("Digite o nome do usuário: ");
                    var usuarioConsulta = cadastro.PesquisarUsuario(Console.ReadLine());
                    if (usuarioConsulta != null)
                        Console.WriteLine($"Usuário encontrado com sucesso: {usuarioConsulta.Nome}");
                    else
                        Console.WriteLine("Usuário não encontrado.");
                    break;

                case 6:
                    Console.WriteLine("Digite o nome do usuário: ");
                    string excluirUsuario = Console.ReadLine();
                    var usuarioExcluir = cadastro.PesquisarUsuario(excluirUsuario);

                    if (usuarioExcluir != null)
                    {
                        cadastro.RemoverUsuario(usuarioExcluir);
                        Console.WriteLine("Usuário excluido com sucesso.");
                    }

                    else
                    {
                        Console.WriteLine("Usuário não foi excluido.");
                    }
                    break;

                case 7: 
                    Console.Write("Digite o nome do usuário: ");
                    string nomeUsuarioPermissao = Console.ReadLine();
                    var usuarioPermissao = cadastro.PesquisarUsuario(nomeUsuarioPermissao);

                    Console.Write("Digite o nome do ambiente: ");
                    string nomeAmbientePermissao = Console.ReadLine();
                    var ambientePermissao = cadastro.PesquisarAmbiente(nomeAmbientePermissao);

                    if (usuarioPermissao != null && ambientePermissao != null)
                    {
                        if (usuarioPermissao.ConcederPermissao(ambientePermissao))
                            Console.WriteLine("Permissão concedida com sucesso.");
                        else
                            Console.WriteLine("O usuário já possui permissão para este ambiente.");
                    }
                    else
                    {
                        Console.WriteLine("Usuário ou ambiente não encontrados.");
                    }
                    break;

                case 8: 
                    Console.Write("Digite o nome do usuário: ");
                    string nomeUsuarioRevogar = Console.ReadLine();
                    var usuarioRevogar = cadastro.PesquisarUsuario(nomeUsuarioRevogar);

                    Console.Write("Digite o nome do ambiente: ");
                    string nomeAmbienteRevogar = Console.ReadLine();
                    var ambienteRevogar = cadastro.PesquisarAmbiente(nomeAmbienteRevogar);

                    if (usuarioRevogar != null && ambienteRevogar != null)
                    {
                        if (usuarioRevogar.RevogarPermissao(ambienteRevogar))
                            Console.WriteLine("Permissão revogada com sucesso.");
                        else
                            Console.WriteLine("O usuário não possui permissão para este ambiente.");
                    }
                    else
                    {
                        Console.WriteLine("Usuário ou ambiente não encontrados.");
                    }
                    break;

                case 9:
                    Console.Write("Digite o nome do ambiente: ");
                    var ambienteLog = cadastro.PesquisarAmbiente(Console.ReadLine());
                    Console.Write("Digite o nome do usuário: ");
                    var usuarioLog = cadastro.PesquisarUsuario(Console.ReadLine());
                    if (ambienteLog != null && usuarioLog != null)
                    {
                        bool acesso = usuarioLog.Ambientes.Contains(ambienteLog);
                        ambienteLog.RegistrarLog(new Log(DateTime.Now, usuarioLog, acesso));
                        Console.WriteLine(acesso ? "Acesso autorizado." : "Acesso negado.");
                    }
                    else
                        Console.WriteLine("Ambiente ou usuário não encontrados.");
                    break;

                case 10: 
                    Console.Write("Digite o nome do ambiente: ");
                    string nomeAmbienteLogs = Console.ReadLine();
                    var ambienteLogs = cadastro.PesquisarAmbiente(nomeAmbienteLogs);

                    if (ambienteLogs != null)
                    {
                        Console.WriteLine("Filtrar logs por:");
                        Console.WriteLine("1. Todos");
                        Console.WriteLine("2. Apenas autorizados");
                        Console.WriteLine("3. Apenas negados");

                        int filtro = int.Parse(Console.ReadLine());
                        var logs = ambienteLogs.Logs;

                        IEnumerable<Log> logsFiltrados = filtro switch
                        {
                            2 => logs.Where(log => log.TipoAcesso),
                            3 => logs.Where(log => !log.TipoAcesso),
                            _ => logs
                        };

                        foreach (var log in logsFiltrados)
                        {
                            Console.WriteLine($"{log.DtAcesso}: Usuário {log.Usuario.Nome} - {(log.TipoAcesso ? "Autorizado" : "Negado")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ambiente não encontrado.");
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
