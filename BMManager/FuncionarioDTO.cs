using System.Security.Claims;

namespace BMManager;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Equipa { get; set; }

    public FuncionarioDTO(int id, string name, string equipa)
    {
        Id = id;
        Name = name;
        Equipa = equipa;
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
