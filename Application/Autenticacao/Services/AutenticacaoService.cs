using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Dto;
using Application.Autenticacao.Dto.Cliente;
using Domain.Autenticacao;
using Domain.Autenticacao.Enums;
using Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Autenticacao.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IAutenticacaoRepository _autenticacaoRepository;
        private readonly ConfiguracaoToken _settings;

        public AutenticacaoService(IAutenticacaoRepository autenticacaoRepository, IOptions<ConfiguracaoToken> options)
        {
            _autenticacaoRepository = autenticacaoRepository;
            _settings = options.Value;
        }

        public async Task<LogInUsuarioOutput> AutenticaUsuario(LogInUsuarioInput input)
        {
            var loginDto = new LoginUsuarioDto(input.NomeUsuario, EncryptPassword(input.Senha));

            var usuario = new AcessoUsuario(loginDto.NomeUsuario, loginDto.Senha);

            var autenticado = await _autenticacaoRepository.AutenticaUsuario(usuario);

            if (!string.IsNullOrEmpty(autenticado.NomeUsuario))
            {
                var token = GenerateToken(input.NomeUsuario, autenticado.Role.ToString(), autenticado.Id);
                return new LogInUsuarioOutput(input.NomeUsuario, token);
            }

            return new LogInUsuarioOutput(false, "Usuario ou senha invalidos");
        }

        public async Task<IdentificaOutput> AutenticaCliente(IdentificaInput input)
        {
            var identificaDto = new IdentificaDto(input.CPF, EncryptPassword(input.Senha));

            var usuario = new AcessoCliente(identificaDto.CPF, identificaDto.Senha);

            var autenticado = await _autenticacaoRepository.AutenticaCliente(usuario);

            if (!string.IsNullOrEmpty(autenticado.CPF))
            {
                var token = GenerateToken(autenticado.Nome, Roles.Cliente.ToString(), autenticado.Id);
                return new IdentificaOutput(autenticado.Nome, token);
            }

            return new IdentificaOutput(false, "Usuario ou senha invalidos");
        }

        public async Task<IdentificaOutput> CadastraCliente(CadastraClienteInput input)
        {
            var clienteDto = new CadastraClienteDto(EncryptPassword(input.Senha), input);

            var usuario = new AcessoCliente(clienteDto.CPF, clienteDto.Senha, clienteDto.Email, clienteDto.Nome);

            if (await _autenticacaoRepository.ClienteJaExiste(usuario))
            {
                return new IdentificaOutput(false, "CPF ou e-mail já cadastrados");
            }

            _autenticacaoRepository.CadastraCliente(usuario);

            await _autenticacaoRepository.UnitOfWork.Commit();

            var autenticado = await _autenticacaoRepository.AutenticaCliente(usuario);

            var token = GenerateToken(clienteDto.Nome, Roles.Cliente.ToString(), autenticado.Id);
            return new IdentificaOutput(clienteDto.Nome, token);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _autenticacaoRepository.Dispose();
        }


        private string GenerateToken(string name, string role, Guid idUsuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _settings.ClientSecret;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string EncryptPassword(string dataToEncrypt)
        {
            string encryptedData;
            var bytes = Encoding.UTF8.GetBytes($"{_settings.PreSalt}{dataToEncrypt}{_settings.PosSalt}");
            var hash = SHA512.HashData(bytes);
            encryptedData = GetStringFromHash(hash);

            return encryptedData;
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
