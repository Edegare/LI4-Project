using System.Security.Claims;

namespace BMManager;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Equipa { get; set; }
    public bool Conta_Ativa {  get; set; }

    public FuncionarioDTO(int id, string name, string equipa, bool conta_ativa)
    {
        Id = id;
        Name = name;
        Equipa = equipa;
        Conta_Ativa = conta_ativa;
    }

    // Método para converter o DTO em uma lista de Claims
    public Claim[] ToClaims()
    {
        return
        [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Name, Name),
            new Claim(ClaimTypes.Role, Equipa),
        ];
    }
}
